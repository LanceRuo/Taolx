using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taolx.Common.DataAccess
{
    /// <summary>
    /// 淘旅行数据库上下文
    /// </summary>
    public class TaolxDbContext
    {
        /// <summary>
        /// 只读dbContext
        /// </summary>
        internal DbContext ReadDbContext { private set; get; }

        /// <summary>
        /// 写入dbContext
        /// </summary>
        internal DbContext WriteDbContext { private set; get; }

        /// <summary>
        ///  只读连接字符串
        /// </summary>
        public string ReadConnectionString { private set; get; }

        /// <summary>
        /// 写入连接字符串
        /// </summary>
        public string WriteConnectionString { private set; get; }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="readConnectionString">只读连接字符串</param>
        /// <param name="writeConnectionString">写入连接字符串</param>
        /// <param name="databaseType">数据库类型,默认为MySql</param>
        public TaolxDbContext(string readConnectionString, string writeConnectionString, DatabaseType databaseType = DatabaseType.MySql)
        {
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
            var readDbConnection = CreateDbConnection(readConnectionString, databaseType);
            ReadDbContext = new DbContext(readDbConnection, true);
            if (readConnectionString == writeConnectionString)
                WriteDbContext = ReadDbContext;
            else
            {
                var writeDbConnection = CreateDbConnection(writeConnectionString, databaseType);
                WriteDbContext = new DbContext(writeDbConnection, true);
            }
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
            foreach (var property in properties)
            {
                if (property.PropertyType.FullName.IndexOf("TaolxDbSet") > 0)
                    property.SetValue(this, null);
            }
        }

        /// <summary>
        /// 输出sql日志
        /// </summary>
        /// <param name="log"></param>
        public virtual void Log(string log)
        {
            Debug.WriteLine(string.Format("{0}:{1}:{2}", GetType().FullName, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), log));
        }

    }
}
