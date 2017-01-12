using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Taolx.Common.DataAccess;

namespace Taolx.Common.DataAccess
{

    /// <summary>
    /// 淘旅行数据库上下文
    /// </summary>
    public class TaolxDbContext : IDisposable
    {

        /// <summary>
        /// writeDbConnection
        /// </summary>
        private DbConnection _writeDbConnection;

        /// <summary>
        /// 读连接
        /// </summary>
        internal DbConnection ReadDbConnection { private set; get; }

        /// <summary>
        /// 写入连接
        /// </summary>
        internal DbConnection WriteDbConnection
        {
            get
            {
                if (_writeDbConnection == null)
                    _writeDbConnection = CreateDbConnection(WriteConnectionString, DatabaseType);
                return _writeDbConnection;
            }
        }

        /// <summary>
        /// 只读dbContext
        /// </summary>
        internal InternalDbContext ReadDbContext { private set; get; }

        /// <summary>
        /// 事务
        /// </summary>
        internal DbTransaction WriteDbTransaction { private set; get; }

        /// <summary>
        ///  只读连接字符串
        /// </summary>
        public string ReadConnectionString { private set; get; }

        /// <summary>
        /// 写入连接字符串
        /// </summary>
        public string WriteConnectionString { private set; get; }

        /// <summary>
        /// DatabaseType
        /// </summary>
        public DatabaseType DatabaseType { set; get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="readConnectionString">只读连接字符串</param>
        /// <param name="writeConnectionString">写入连接字符串</param>
        /// <param name="databaseType">数据库类型,默认为MySql</param>
        public TaolxDbContext(string readConnectionString, string writeConnectionString, DatabaseType databaseType = DatabaseType.MySql)
        {
            ReadConnectionString = readConnectionString;
            WriteConnectionString = writeConnectionString;
            DatabaseType = databaseType;
            //创建dbContext
            CreateDbContext(readConnectionString, writeConnectionString, databaseType);
            //初始化dbSet
            InitDbSet();
        }

        /// <summary>
        /// 创建dbContext
        /// </summary>
        /// <param name="readConnectionString">只读连接字符串</param>
        /// <param name="writeConnectionString">写入连接字符串</param>
        /// <param name="databaseType">数据库类型,默认为MySql</param>
        private void CreateDbContext(string readConnectionString, string writeConnectionString, DatabaseType databaseType = DatabaseType.MySql)
        {
            ReadDbConnection = CreateDbConnection(readConnectionString, databaseType);
            ReadDbContext = InternalDbContext.Create(GetType(), GetAllEntityTypes, ReadDbConnection, true);
            ReadDbContext.Database.Log = Log;
        }

        /// <summary>
        /// 获取当前类中的entity类型
        /// </summary>
        /// <returns></returns>
        private List<Type> GetAllEntityTypes()
        {
            List<Type> result = new List<Type>();
            var properties = GetType().GetProperties();
            var typeName = typeof(ITaolxDbSet<>).Name;
            foreach (var property in properties)
            {
                var type = property.PropertyType;
                if (!type.GetInterfaces().Any(o => o.Name == typeName))
                    continue;
                var entityType = type.GenericTypeArguments[0];
                if (entityType.IsValueType)
                    throw new ArgumentException(string.Format("类型{0}不是有效的实体类型", entityType.FullName));
                result.Add(entityType);
            }
            return result.Distinct().ToList();
        }

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        private DbConnection CreateDbConnection(string connectionString, DatabaseType databaseType)
        {
            switch (databaseType)
            {
                case DatabaseType.MySql:
                    return new MySqlConnection(connectionString);
                case DatabaseType.SqlServer:
                    return new SqlConnection(connectionString);
                default:
                    return null;
            }
        }

        /// <summary>
        /// 初始化dbset
        /// </summary>
        private void InitDbSet()
        {
            var properties = GetType().GetProperties();
            var typeName = typeof(ITaolxDbSet<>).Name;
            foreach (var property in properties)
            {
                var type = property.PropertyType;
                if (!type.GetInterfaces().Any(o => o.Name == typeName))
                    continue;
                var entityType = type.GenericTypeArguments[0];
                var dbSet = TaolxDbSet.Create(entityType, this);
                property.SetValue(this, dbSet);
            }
        }

        #region public method

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <returns></returns>
        public DbTransaction BeginTransaction()
        {
            if (WriteDbConnection.State != ConnectionState.Open)
                WriteDbConnection.Open();
            return WriteDbTransaction = WriteDbConnection.BeginTransaction();
        }

        /// <summary>
        /// 开启事务
        /// </summary>
        /// <param name="isolationLevel">事务级别</param>
        /// <returns></returns>
        public DbTransaction BeginTransaction(IsolationLevel isolationLevel)
        {
            return WriteDbTransaction = WriteDbConnection.BeginTransaction(isolationLevel);
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        public void Commit()
        {
            if (WriteDbTransaction == null)
                throw new ArgumentNullException("事务对象为空,请检查是否开启事务");
            WriteDbTransaction.Commit();
            WriteDbTransaction = null;
        }

        /// <summary>
        /// 回滚事务
        /// </summary>
        public void Rollback()
        {
            if (WriteDbTransaction == null)
                throw new ArgumentNullException("事务对象为空,请检查是否开启事务");
            try
            {
                WriteDbTransaction.Rollback();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex);
            }
            WriteDbTransaction = null;
        }

        /// <summary>
        /// 输出sql日志
        /// </summary>
        /// <param name="log"></param>
        public virtual void Log(string log)
        {
            Debug.WriteLine(string.Format("{0}:{1}:{2}", GetType().FullName, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), log));
        }

        /// <summary>
        /// 销毁
        /// </summary>
        public void Dispose()
        {
            ReadDbContext.Dispose();
           if (WriteDbTransaction != null)
                Rollback();
            WriteDbConnection.Dispose();
        } 
        #endregion
    }
}
