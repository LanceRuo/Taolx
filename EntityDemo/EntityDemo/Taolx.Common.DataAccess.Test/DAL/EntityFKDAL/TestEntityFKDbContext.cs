using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taolx.Common.DataAccess.Test.DAL.EntityFKDAL
{
    public class TestEntityFKDbContext : DbContext
    {


        /// <summary>
        /// 写数据库连接字符串
        /// </summary>
        private readonly static string writeConnectionString = TestConfig.WriteConnectionString;

        /// <summary>
        /// Table1
        /// </summary>
        public DbSet<Table1> Table1 { set; get; }

        /// <summary>
        /// Table2
        /// </summary>
        public DbSet<Table2> Table2 { set; get; }

        /// <summary>
        /// Table3
        /// </summary>
        public DbSet<Table3> Table3 { set; get; }

        public TestEntityFKDbContext() : base(new MySqlConnection(writeConnectionString), true)
        {
            this.Database.Log = (log) =>
            {
                //Debug.WriteLine(string.Format("{0}:{1}:{2}", GetType().FullName, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), log));
            };
        }


    }
}
