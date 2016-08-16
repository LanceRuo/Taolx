
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Dynamic;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Taolx.Common.DataAccess
{
    public static class BatchExtensions
    {

        private static readonly string _selectRegex = @"SELECT\s*\r\n(?<ColumnValue>.+)?\s*AS\s*(?<ColumnAlias>\w+)\r\nFROM\s*(?<TableName>\w+\.\w+|\w+)\s*AS\s*(?<TableAlias>\w+)";

        /// <summary>
        /// 新增
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="entity"></param>
        /// <returns></returns>

        public static TEntity Add<TEntity>(this TaolxQueryable<TEntity> source, TEntity entity) where TEntity : class
        {

            ObjectQuery<TEntity> sourceQuery = source.ToObjectQuery();
            if (sourceQuery == null)
                throw new ArgumentException("The query must be of type ObjectQuery or DbQuery.", "source");
            ObjectContext objectContext = sourceQuery.Context;
            if (objectContext == null)
                throw new ArgumentException("The ObjectContext for the source query can not be null.", "source");
            EntityMap entityMap = sourceQuery.GetEntityMap<TEntity>();
            if (entityMap == null)
                throw new ArgumentException("Could not load the entity mapping information for the query ObjectSet.", "source");
            InternalAdd(source, objectContext, entityMap, new List<TEntity> { entity });
            return entity;
        }

        /// <summary>
        /// 增加多个
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="enities"></param>
        /// <returns></returns>
        public static IEnumerable<TEntity> AddRange<TEntity>(this TaolxQueryable<TEntity> source, IEnumerable<TEntity> enities) where TEntity : class
        {
            ObjectQuery<TEntity> sourceQuery = source.ToObjectQuery();
            if (sourceQuery == null)
                throw new ArgumentException("The query must be of type ObjectQuery or DbQuery.", "source");
            ObjectContext objectContext = sourceQuery.Context;
            if (objectContext == null)
                throw new ArgumentException("The ObjectContext for the source query can not be null.", "source");
            EntityMap entityMap = sourceQuery.GetEntityMap<TEntity>();
            if (entityMap == null)
                throw new ArgumentException("Could not load the entity mapping information for the query ObjectSet.", "source");
            InternalAdd(source, objectContext, entityMap, enities);
            return enities;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static int Delete<TEntity>(this TaolxQueryable<TEntity> source) where TEntity : class
        {
            ObjectQuery<TEntity> sourceQuery = source.ToObjectQuery();
            if (sourceQuery == null)
                throw new ArgumentException("The query must be of type ObjectQuery or DbQuery.", "source");

            ObjectContext objectContext = sourceQuery.Context;
            if (objectContext == null)
                throw new ArgumentException("The ObjectContext for the source query can not be null.", "source");

            EntityMap entityMap = sourceQuery.GetEntityMap<TEntity>();
            if (entityMap == null)
                throw new ArgumentException("Could not load the entity mapping information for the query ObjectSet.", "source");
            return InternalDelete(source, objectContext, entityMap, sourceQuery);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="updateExpression"></param>
        /// <returns></returns>
        public static int Update<TEntity>(this TaolxQueryable<TEntity> source, Expression<Func<TEntity, TEntity>> updateExpression)
          where TEntity : class
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (updateExpression == null)
                throw new ArgumentNullException("updateExpression");
            ObjectQuery<TEntity> sourceQuery = source.ToObjectQuery();
            if (sourceQuery == null)
                throw new ArgumentException("The query must be of type ObjectQuery or DbQuery.", "source");
            ObjectContext objectContext = sourceQuery.Context;
            if (objectContext == null)
                throw new ArgumentException("The ObjectContext for the query can not be null.", "source");
            EntityMap entityMap = sourceQuery.GetEntityMap<TEntity>();
            if (entityMap == null)
                throw new ArgumentException("Could not load the entity mapping information for the source.", "source");
            return InternalUpdate(source, objectContext, entityMap, sourceQuery, updateExpression);
        }

        #region private

        private static IEnumerable<TEntity> InternalAdd<TEntity>(TaolxQueryable<TEntity> source, ObjectContext objectContext, EntityMap entityMap, IEnumerable<TEntity> entities) where TEntity : class
        {
            DbConnection insertConnection = source.TaolxDbContext.WriteDbConnection;
            DbTransaction insertTransaction = source.TaolxDbContext.WriteDbTransaction;
            DbCommand insertCommand = null;
            bool ownConnection = false;
            bool ownTransaction = false;
            try
            {
                if (insertConnection.State != ConnectionState.Open)
                {
                    insertConnection.Open();
                    ownConnection = true;
                }
                if (insertTransaction == null)
                {
                    insertTransaction = insertConnection.BeginTransaction();
                    ownTransaction = true;
                }
                insertCommand = insertConnection.CreateCommand();
                insertCommand.Transaction = insertTransaction;
                if (objectContext.CommandTimeout.HasValue)
                    insertCommand.CommandTimeout = objectContext.CommandTimeout.Value;
                CheckIsIdentity<TEntity>(entityMap);
                source.TaolxDbContext.Log(string.Format("{0}:-------Insert sql start", source.TaolxDbContext.GetType().Name));
                foreach (var entity in entities)
                {
                    var sql = CreateInsertSql(entityMap, entity);
                    source.TaolxDbContext.Log(sql.Item1);
                    sql.Item2.ForEach(p => source.TaolxDbContext.Log(string.Format(" \t{0}:{1}   \t({2});", p.ParameterName, p.Value, p.DbType)));
                    source.TaolxDbContext.Log(string.Format("{0}:-------Insert sql end", source.TaolxDbContext.GetType().Name));
                    insertCommand.CommandType = CommandType.Text;
                    insertCommand.CommandText = sql.Item1;
                    insertCommand.Parameters.Clear();
                    insertCommand.Parameters.AddRange(sql.Item2.ToArray());
                    var result = insertCommand.ExecuteScalar();
                    foreach (var key in entityMap.KeyMaps)
                    {
                        if (key.IsStoreGeneratedIdentity)
                        {
                            var kp = entity.GetType().GetProperty(key.PropertyName);
                            var ty = kp.PropertyType;
                            var nResult = Convert.ChangeType(result, ty);
                            kp.SetValue(entity, nResult);
                            break;
                        }
                    }
                }
                if (ownTransaction)
                    insertTransaction.Commit();
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (insertCommand != null)
                    insertCommand.Dispose();
                if (insertTransaction != null && ownTransaction)
                    insertTransaction.Dispose();
                if (insertConnection != null && ownConnection)
                    insertConnection.Close();
            }
        }

        private static int InternalDelete<TEntity>(TaolxQueryable<TEntity> source, ObjectContext objectContext, EntityMap entityMap, ObjectQuery<TEntity> query) where TEntity : class
        {
            DbConnection deleteConnection = source.TaolxDbContext.WriteDbConnection;
            DbTransaction deleteTransaction = source.TaolxDbContext.WriteDbTransaction;
            DbCommand deleteCommand = null;
            bool ownConnection = false;
            bool ownTransaction = false;
            try
            {
                // get store connection and transaction
                //var store = GetStore(objectContext);
                //deleteConnection = store.Item1;
                //deleteTransaction = store.Item2;
                if (deleteConnection.State != ConnectionState.Open)
                {
                    deleteConnection.Open();
                    ownConnection = true;
                }
                if (deleteTransaction == null)
                {
                    deleteTransaction = deleteConnection.BeginTransaction();
                    ownTransaction = true;
                }
                deleteCommand = deleteConnection.CreateCommand();
                deleteCommand.Transaction = deleteTransaction;
                if (objectContext.CommandTimeout.HasValue)
                    deleteCommand.CommandTimeout = objectContext.CommandTimeout.Value;
                var innerSelect = GetSelectSql(query, entityMap, deleteCommand);
                var sqlBuilder = new StringBuilder(innerSelect.Length * 2);
                sqlBuilder.Append("DELETE j0");
                sqlBuilder.AppendLine();
                sqlBuilder.AppendFormat("FROM {0} AS j0 INNER JOIN (", entityMap.TableName);
                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine(innerSelect);
                sqlBuilder.Append(") AS j1 ON (");
                bool wroteKey = false;
                foreach (var keyMap in entityMap.KeyMaps)
                {
                    if (wroteKey)
                        sqlBuilder.Append(" AND ");
                    sqlBuilder.AppendFormat("j0.`{0}` = j1.`{0}`", keyMap.ColumnName);
                    wroteKey = true;
                }
                sqlBuilder.Append(")");
                deleteCommand.CommandText = sqlBuilder.ToString().Replace("[", "`").Replace("]", "`");
#if DEBUG
                source.TaolxDbContext.Log("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$-- LogStart----");
                source.TaolxDbContext.Log(deleteCommand.CommandText);
                foreach (DbParameter p in deleteCommand.Parameters)
                    source.TaolxDbContext.Log(string.Format("{0}:{1} ({2});", p.ParameterName, p.Value, p.DbType));
                source.TaolxDbContext.Log("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$-- LogEnd----");
#endif 
                int result = deleteCommand.ExecuteNonQuery();
                // only commit if created transaction
                if (ownTransaction)
                    deleteTransaction.Commit();

                return result;
            }
            finally
            {
                if (deleteCommand != null)
                    deleteCommand.Dispose();
                if (deleteTransaction != null && ownTransaction)
                    deleteTransaction.Dispose();
                if (deleteConnection != null && ownConnection)
                    deleteConnection.Close();
            }
        }

        private static int InternalUpdate<TEntity>(TaolxQueryable<TEntity> source, ObjectContext objectContext, EntityMap entityMap, ObjectQuery<TEntity> query, Expression<Func<TEntity, TEntity>> updateExpression, bool async = false)
            where TEntity : class
        {
            DbConnection updateConnection = source.TaolxDbContext.WriteDbConnection;
            DbTransaction updateTransaction = source.TaolxDbContext.WriteDbTransaction;
            DbCommand updateCommand = null;
            bool ownConnection = false;
            bool ownTransaction = false;
            try
            {
                // get store connection and transaction
                //var store = GetStore(objectContext);
                //updateConnection = store.Item1;
                //updateTransaction = store.Item2;
                if (updateConnection.State != ConnectionState.Open)
                {
                    updateConnection.Open();
                    ownConnection = true;
                }
                // use existing transaction or create new
                if (updateTransaction == null)
                {
                    updateTransaction = updateConnection.BeginTransaction();
                    ownTransaction = true;
                }
                updateCommand = updateConnection.CreateCommand();
                updateCommand.Transaction = updateTransaction;
                if (objectContext.CommandTimeout.HasValue)
                    updateCommand.CommandTimeout = objectContext.CommandTimeout.Value;
                var innerSelect = GetSelectSql(query, entityMap, updateCommand);
                var sqlBuilder = new StringBuilder(innerSelect.Length * 2);
                sqlBuilder.Append("UPDATE ");
                sqlBuilder.Append(entityMap.TableName);
                sqlBuilder.AppendFormat(" AS j0 INNER JOIN (", entityMap.TableName);
                sqlBuilder.AppendLine();
                sqlBuilder.AppendLine(innerSelect);
                sqlBuilder.Append(") AS j1 ON (");
                bool wroteKey = false;
                foreach (var keyMap in entityMap.KeyMaps)
                {
                    if (wroteKey)
                        sqlBuilder.Append(" AND ");
                    sqlBuilder.AppendFormat("j0.`{0}` = j1.`{0}`", keyMap.ColumnName);
                    wroteKey = true;
                }
                sqlBuilder.Append(")");
                sqlBuilder.AppendLine(" ");
                sqlBuilder.AppendLine(" SET ");
                var memberInitExpression = updateExpression.Body as MemberInitExpression;
                if (memberInitExpression == null)
                    throw new ArgumentException("The update expression must be of type MemberInitExpression.", "updateExpression");
                int nameCount = 0;
                bool wroteSet = false;
                foreach (MemberBinding binding in memberInitExpression.Bindings)
                {
                    if (wroteSet)
                        sqlBuilder.AppendLine(", ");
                    string propertyName = binding.Member.Name;
                    string columnName = entityMap.PropertyMaps
                        .Where(p => p.PropertyName == propertyName)
                        .Select(p => p.ColumnName)
                        .FirstOrDefault();
                    var memberAssignment = binding as MemberAssignment;
                    if (memberAssignment == null)
                        throw new ArgumentException("The update expression MemberBinding must only by type MemberAssignment.", "updateExpression");
                    Expression memberExpression = memberAssignment.Expression;
                    ParameterExpression parameterExpression = null;
                    memberExpression.Visit((ParameterExpression p) =>
                    {
                        if (p.Type == entityMap.EntityType)
                            parameterExpression = p;
                        return p;
                    });
                    if (parameterExpression == null)
                    {
                        object value;
                        if (memberExpression.NodeType == ExpressionType.Constant)
                        {
                            var constantExpression = memberExpression as ConstantExpression;
                            if (constantExpression == null)
                                throw new ArgumentException("The MemberAssignment expression is not a ConstantExpression.", "updateExpression");
                            value = constantExpression.Value;
                        }
                        else
                        {
                            LambdaExpression lambda = Expression.Lambda(memberExpression, null);
                            value = lambda.Compile().DynamicInvoke();
                        }
                        if (value != null)
                        {
                            string parameterName = "p__update__" + nameCount++;
                            var parameter = updateCommand.CreateParameter();
                            parameter.ParameterName = parameterName;
                            parameter.Value = value;
                            updateCommand.Parameters.Add(parameter);
                            sqlBuilder.AppendFormat("`{0}` = @{1}", columnName, parameterName);
                        }
                        else
                        {
                            sqlBuilder.AppendFormat("`{0}` = NULL", columnName);
                        }
                    }
                    else
                    {
                        // create clean objectset to build query from
                        var objectSet = objectContext.CreateObjectSet<TEntity>();

                        Type[] typeArguments = new[] { entityMap.EntityType, memberExpression.Type };

                        ConstantExpression constantExpression = Expression.Constant(objectSet);
                        LambdaExpression lambdaExpression = Expression.Lambda(memberExpression, parameterExpression);

                        MethodCallExpression selectExpression = Expression.Call(
                            typeof(Queryable),
                            "Select",
                            typeArguments,
                            constantExpression,
                            lambdaExpression);

                        // create query from expression
                        var selectQuery = objectSet.CreateQuery(selectExpression, entityMap.EntityType);
                        string sql = selectQuery.ToTraceString();

                        // parse select part of sql to use as update

                        Match match = Regex.Match(sql, _selectRegex);
                        if (!match.Success)
                            throw new ArgumentException("The MemberAssignment expression could not be processed.", "updateExpression");
                        string value = match.Groups["ColumnValue"].Value;
                        string alias = match.Groups["TableAlias"].Value;
                        value = value.Replace(alias + ".", "");
                        foreach (ObjectParameter objectParameter in selectQuery.Parameters)
                        {
                            string parameterName = "p__update__" + nameCount++;
                            var parameter = updateCommand.CreateParameter();
                            parameter.ParameterName = parameterName;
                            parameter.Value = objectParameter.Value;
                            updateCommand.Parameters.Add(parameter);
                            value = value.Replace(objectParameter.Name, parameterName);
                        }
                        sqlBuilder.AppendFormat("`{0}` = {1}", columnName, value);
                    }
                    wroteSet = true;
                }

                updateCommand.CommandText = sqlBuilder.ToString().Replace("[", "`").Replace("]", "`");
#if DEBUG
                source.TaolxDbContext.Log("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$-- LogStart----");
                source.TaolxDbContext.Log(updateCommand.CommandText);
                foreach (DbParameter p in updateCommand.Parameters)
                    source.TaolxDbContext.Log(string.Format("{0}:{1} ({2});", p.ParameterName, p.Value, p.DbType));
                source.TaolxDbContext.Log("$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$-- LogEnd----");
#endif
                int result = updateCommand.ExecuteNonQuery();
                // only commit if created transaction
                if (ownTransaction)
                    updateTransaction.Commit();
                return result;
            }
            finally
            {
                if (updateCommand != null)
                    updateCommand.Dispose();
                if (updateTransaction != null && ownTransaction)
                    updateTransaction.Dispose();
                if (updateConnection != null && ownConnection)
                    updateConnection.Close();
            }
        }

        private static Tuple<DbConnection, DbTransaction> GetStore(ObjectContext objectContext)
        {
            DbConnection dbConnection = objectContext.Connection;
            var entityConnection = dbConnection as EntityConnection;

            // by-pass entity connection
            if (entityConnection == null)
                return new Tuple<DbConnection, DbTransaction>(dbConnection, null);

            DbConnection connection = entityConnection.StoreConnection;

            // get internal transaction
            dynamic connectionProxy = new DynamicProxy(entityConnection);
            dynamic entityTransaction = connectionProxy.CurrentTransaction;
            if (entityTransaction == null)
                return new Tuple<DbConnection, DbTransaction>(connection, null);

            DbTransaction transaction = entityTransaction.StoreTransaction;
            return new Tuple<DbConnection, DbTransaction>(connection, transaction);
        }

        private static string GetSelectSql<TEntity>(ObjectQuery<TEntity> query, EntityMap entityMap, DbCommand command)
           where TEntity : class
        {
            // changing query to only select keys
            var selector = new StringBuilder(50);
            selector.Append("new(");
            foreach (var propertyMap in entityMap.KeyMaps)
            {
                if (selector.Length > 4)
                    selector.Append((", "));
                selector.Append(propertyMap.PropertyName);
            }
            selector.Append(")");

            var selectQuery = DynamicQueryable.Select(query, selector.ToString());
            var objectQuery = selectQuery as ObjectQuery;

            if (objectQuery == null)
                throw new ArgumentException("The query must be of type ObjectQuery.", "query");

            string innerJoinSql = objectQuery.ToTraceString();

            // create parameters
            foreach (var objectParameter in objectQuery.Parameters)
            {
                var parameter = command.CreateParameter();
                parameter.ParameterName = objectParameter.Name;
                parameter.Value = objectParameter.Value;
                command.Parameters.Add(parameter);
            }
            return innerJoinSql;
        }

        private static Tuple<string, List<MySqlParameter>> CreateInsertSql<TEntity>(EntityMap entityMap, TEntity entity, int outIndex = 0)
        {
            var allFileds = entityMap.PropertyMaps;
            var needAddKeys = allFileds.Where(o => entityMap.KeyMaps.All(k => o.ColumnName != k.ColumnName)).ToList();
            var sqlBuilder = new StringBuilder();
            sqlBuilder.AppendFormat("INSERT INTO {0} ({1}) VALUES(", entityMap.TableName, string.Join(",", needAddKeys.Select(p => string.Format("`{0}`", p.ColumnName))));
            var index = 0;
            List<MySqlParameter> sp = new List<MySqlParameter>();
            List<string> values = new List<string>();
            foreach (var columnMapping in needAddKeys)
            {
                var @value = entity.GetType().GetProperty(columnMapping.PropertyName).GetValue(entity);
                if (@value == null)
                {
                    values.Add("null");
                    continue;
                }
                else
                {
                    var pName = string.Format("@_{0}_{1}", outIndex, index, columnMapping.ColumnName);
                    var vType = @value.GetType();
                    MySqlParameter p = null;
                    if (vType.IsEnum)
                        p = new MySqlParameter(pName, (int)@value);
                    else
                        p = new MySqlParameter(pName, @value);
                    sp.Add(p);
                    values.Add(pName);
                    index++;
                }
            }
            sqlBuilder.AppendFormat("{0});", string.Join(",", values));
            var keyProperty = string.Empty;
            foreach (var key in entityMap.KeyMaps)
            {
                if (key.IsStoreGeneratedIdentity)
                {
                    sqlBuilder.Append("SELECT LAST_INSERT_ID();");
                    keyProperty = key.PropertyName;
                }
            }
            return new Tuple<string, List<MySqlParameter>>(sqlBuilder.ToString(), sp);
        }

        /// <summary>
        /// 是否自增长
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entityMap"></param>
        private static void CheckIsIdentity<TEntity>(EntityMap entityMap)
        {
            var type = typeof(TEntity);

            foreach (var key in entityMap.KeyMaps)
            {
                var p = type.GetProperty(key.PropertyName);
                var dg = (DatabaseGeneratedAttribute)p.GetCustomAttributes(typeof(DatabaseGeneratedAttribute), false).FirstOrDefault();
                if (dg == null || dg.DatabaseGeneratedOption == DatabaseGeneratedOption.Identity)
                    key.IsStoreGeneratedIdentity = true;
            }
        }

        #endregion
    }
}
