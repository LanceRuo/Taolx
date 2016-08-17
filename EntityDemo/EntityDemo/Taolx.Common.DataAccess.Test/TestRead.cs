using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taolx.Common.DataAccess.Test.DAL.TaolxDAL;
namespace Taolx.Common.DataAccess.Test
{
    [TestClass]
    public class TestRead
    {


        [TestMethod]
        public void TestAll()
        {
            using (var db = TestConfig.CreateTestTaolxDbContext())
            {
            }
            using (var db = TestConfig.CreateTestEntityFKDbContext())
            {
            }
            TestTaolxDataAccess_WhereById();
            TestEntityFKDbContext_WhereById();
        }

        [TestMethod]
        public void TestTaolxDataAccess_WhereById()
        {
            Func<int, Tuple<long, int>> action = (index) =>
              {
                  Stopwatch sw = new Stopwatch();
                  sw.Start();
                  var db = TestConfig.CreateTestTaolxDbContext();
                  var count = 0;
                  using (db)
                  {
                      db.Table1.GetByJobId(3);

                      var result = db.Table2.Where(o => o.Id > 100).ToList();
                      count = result.Count();

                      var rr = db.Table2.Where(o => o.Id > 10).Select(o => new { o.Id, o.JobId }).ToList();
                      var query = from t1 in db.Table2
                                  join t2 in db.Table2 on t1.Id equals t2.Id into temp
                                  from tt in temp.DefaultIfEmpty()
                                  select new
                                  {
                                      T1 = t1,
                                      T2 = tt
                                  } ;
                     // query.ToList()[0].T2;
                  }
                  sw.Stop();
                  return new Tuple<long, int>(sw.ElapsedMilliseconds, count);
              };
            Trace.WriteLine("TestTaolxDataAccess_WhereById:");
            Trace.WriteLine("Index \t----- \tMilliseconds \t----- \t Count");
            var times = 10;
            for (var index = 0; index < times; index++)
            {
                var ms = action(index);
                Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  \t----- \t {2}", index, ms.Item1, ms.Item2));
            }
            Trace.WriteLine(string.Empty);
        }

        [TestMethod]
        public void TestEntityFKDbContext_WhereById()
        {
            Func<int, Tuple<long, int>> action = (index) =>
             {
                 Stopwatch sw = new Stopwatch();
                 sw.Start();
                 var db = TestConfig.CreateTestEntityFKDbContext();
                 var count = 0;
                 using (db)
                 {
                     var result = db.Table3.Where(o => o.Id > 100).ToList();
                     count = result.Count;
                 }
                 sw.Stop();
                 return new Tuple<long, int>(sw.ElapsedMilliseconds, count);
             };
            Trace.WriteLine("TestEntityFKDbContext_WhereById:");
            Trace.WriteLine("Index \t----- \tMilliseconds");
            var times = 10;
            for (var index = 0; index < times; index++)
            {
                var ms = action(index);
                Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  \t----- \t {2}", index, ms.Item1, ms.Item2));
            }
            Trace.WriteLine(string.Empty);
        }

    }
}
