using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taolx.Common.DataAccess.Test.DAL.EntityFKDAL;
using Taolx.Common.DataAccess.Test.DAL.TaolxDAL;

namespace Taolx.Common.DataAccess.Test
{
    public class TestConfig
    {
        /// <summary>
        /// 写数据库连接字符串
        /// </summary>
        public readonly static string ReadConnectionString = "Server=10.1.21.48;Database=testtaolx;Uid=root;Pwd=sasa;Port = 3306;";

        /// <summary>
        /// 写数据库连接字符串
        /// </summary>
        public readonly static string WriteConnectionString = "Server=10.1.21.48;Database=testtaolx;Uid=root;Pwd=sasa;Port = 3306;";


        public static TestTaolxDbContext CreateTestTaolxDbContext()
        {
            TestTaolxDbContext db2 = new TestTaolxDbContext();
            return db2;
        }

        public static TestEntityFKDbContext CreateTestEntityFKDbContext()
        {
            TestEntityFKDbContext db2 = new TestEntityFKDbContext();
            return db2;
        }
    }
}
