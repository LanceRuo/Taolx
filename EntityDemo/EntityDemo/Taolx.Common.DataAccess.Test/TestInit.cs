using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taolx.Common.DataAccess.Test.DAL;
using System.Diagnostics;
using Taolx.Common.DataAccess.Test.DAL.EntityFKDAL;
using Taolx.Common.DataAccess.Test.DAL.TaolxDAL;
using MySql.Data.MySqlClient;

namespace Taolx.Common.DataAccess.Test
{
    [TestClass]
    public class TestInit
    {
        [TestMethod]
        public void TestAll()
        {
            TestInitEntityFk();
            TestInitTaolxDataAccess();
            TestInitAdo();
        }

        /// <summary>
        /// 测试TaolxDataAccess初始化
        /// </summary>
        [TestMethod]
        public void TestInitTaolxDataAccess()
        {
            Func<int, long> fun = (index) =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                using (TestTaolxDbContext db = new TestTaolxDbContext())
                {
                }
                sw.Stop();
                return sw.ElapsedMilliseconds;
            };
            Trace.WriteLine("TestInitTaolxDataAccess:");
            Trace.WriteLine("Index \t----- \tMilliseconds \t-----");
            var times = 10;
            for (var index = 0; index < times; index++)
            {
                var ms = fun(index);
                Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  ", index, ms));
            }
            Trace.WriteLine(string.Empty);
        }

        /// <summary>
        /// 测试EntityFk初始化
        /// </summary>
        [TestMethod]
        public void TestInitEntityFk()
        {
            Func<int, long> fun = (index) =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                using (TestEntityFKDbContext db = new TestEntityFKDbContext())
                {
                }
                sw.Stop();
                return sw.ElapsedMilliseconds;
            };
            Trace.WriteLine("TestInitEntityFk:");
            Trace.WriteLine("Index \t----- \tMilliseconds \t-----");
            var times = 10;
            for (var index = 0; index < times; index++)
            {
                var ms = fun(index);
                Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  ", index, ms));
            }
            Trace.WriteLine(string.Empty);
        }

        /// <summary>
        /// 测试Ado初始化
        /// </summary>
        [TestMethod]
        public void TestInitAdo()
        {

            Func<int, long> fun = (index) =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                using (MySqlConnection dbConn = new MySqlConnection(TestConfig.WriteConnectionString))
                {
                }
                sw.Stop();
                return sw.ElapsedMilliseconds;
            };
            Trace.WriteLine("TestInitAdo:");
            Trace.WriteLine("Index \t----- \tMilliseconds \t-----");
            var times = 10;
            for (var index = 0; index < times; index++)
            {
                var ms = fun(index);
                Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  ", index, ms));
            }
            Trace.WriteLine(string.Empty);
        }
    }
}
