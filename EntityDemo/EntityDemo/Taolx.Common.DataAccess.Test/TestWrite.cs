using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;
using Taolx.Common.DataAccess.Test.DAL;
using System.Collections.Generic;

namespace Taolx.Common.DataAccess.Test
{
    [TestClass]
    public class TestWrite
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
            TestTaolxDataAccess_Add();
            TestEntityFKDbContext_Add();
            TestTaolxDataAccess_Update();
            TestEntityFKDbContext_Update();
            TestTaolxDataAccess_Delete();
            TestEntityFKDbContext_Delete();
        }


        [TestMethod]
        public void TestAll_Add()
        {
            using (var db = TestConfig.CreateTestTaolxDbContext())
            {
            }
            using (var db = TestConfig.CreateTestEntityFKDbContext())
            {
            }
            TestTaolxDataAccess_Add();
            TestEntityFKDbContext_Add();
        }


        [TestMethod]
        public void TestAll_Update()
        {
            using (var db = TestConfig.CreateTestTaolxDbContext())
            {
            }
            using (var db = TestConfig.CreateTestEntityFKDbContext())
            {
            }
            TestTaolxDataAccess_Update();
            TestEntityFKDbContext_Update();
        }

        [TestMethod]
        public void TestAll_Delete()
        {
            using (var db = TestConfig.CreateTestTaolxDbContext())
            {
            }
            using (var db = TestConfig.CreateTestEntityFKDbContext())
            {
            }
            TestTaolxDataAccess_Delete();
            TestEntityFKDbContext_Delete();
        }


        [TestMethod]
        public void TestTaolxDataAccess_Add()
        {
            var db = TestConfig.CreateTestTaolxDbContext();
            Stopwatch sw = null;
            db = TestConfig.CreateTestTaolxDbContext();
            var entity = db.Table1.First();
            Func<Table2> f = () =>
            {
                var addEntity = new Table2()
                {
                    CreateTime = entity.CreateTime,
                    EndTime = entity.EndTime,
                    ExecutedCommnet = entity.ExecutedCommnet,
                    ExecutedResult = entity.ExecutedResult,
                    ExecutionTime = entity.ExecutionTime,
                    JobId = entity.JobId,
                    Remark = entity.Remark,
                    StartTime = entity.StartTime,
                    UpdateTime = entity.UpdateTime
                };
                return addEntity;
            };
            db.Dispose();
            Func<int, Tuple<long, int>> fun1 = (index) =>
            {
                sw = new Stopwatch();
                sw.Start();
                db = TestConfig.CreateTestTaolxDbContext();
                var rows = 100;
                using (db)
                {
                    db.BeginTransaction();
                    var list = new List<Table2>();
                    for (var rowIndex = 0; rowIndex < rows; rowIndex++)
                        list.Add(f());
                    db.Table2.AddRange(list);
                    db.Commit();
                }
                sw.Stop();
                return new Tuple<long, int>(sw.ElapsedMilliseconds, rows);
            };

            Func<int, Tuple<long, int>> fun2 = (index) =>
            {
                sw = new Stopwatch();
                sw.Start();
                db = TestConfig.CreateTestTaolxDbContext();
                var rows = 100;
                using (db)
                {
                    db.BeginTransaction();
                    var list = new List<Table3>();
                    for (var rowIndex = 0; rowIndex < rows; rowIndex++)
                        db.Table2.Add(f());
                    db.Commit();
                }
                sw.Stop();
                return new Tuple<long, int>(sw.ElapsedMilliseconds, rows);
            };

            Trace.WriteLine("TestTaolxDataAccess_Add:");
            Trace.WriteLine("Index \t----- \tMilliseconds \t----- \t Count");
            var times = 10;
            for (var index = 0; index < times; index++)
            {
                var ms = fun1(index);
                Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  \t----- \t {2}", index, ms.Item1, ms.Item2));
            }
            times = 20;
            for (var index = 10; index < times; index++)
            {
                var ms = fun2(index);
                Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  \t----- \t {2}", index, ms.Item1, ms.Item2));
            }
            Trace.WriteLine(string.Empty);
        }

        [TestMethod]
        public void TestTaolxDataAccess_Update()
        {
            var db = TestConfig.CreateTestTaolxDbContext();
            Stopwatch sw = null;
            db = TestConfig.CreateTestTaolxDbContext();
            var entity = db.Table2.First();
            db = TestConfig.CreateTestTaolxDbContext();
            db.Dispose();
            Func<int, Tuple<long, int>> action = (index) =>
             {
                 sw = new Stopwatch();
                 sw.Start();
                 db = TestConfig.CreateTestTaolxDbContext();
                 var updateRows = 0;
                 using (db)
                 {
                     db.BeginTransaction();
                     entity.CreateTime = entity.CreateTime.AddDays(1);
                     for (var rindex = 0; rindex < 100; rindex++)
                         updateRows += db.Table2.Where(o => o.Id == entity.Id).Update(o => new Table2
                         {
                             CreateTime = entity.CreateTime,
                             EndTime = entity.EndTime,
                             ExecutedCommnet = entity.ExecutedCommnet,
                             ExecutedResult = entity.ExecutedResult,
                             ExecutionTime = entity.ExecutionTime,
                             JobId = entity.JobId,
                             Remark = entity.Remark,
                             StartTime = entity.StartTime,
                             UpdateTime = entity.UpdateTime
                         });
                     db.Commit();
                     sw.Stop();
                     return new Tuple<long, int>(sw.ElapsedMilliseconds, updateRows);
                 }
             };
            Trace.WriteLine("TestTaolxDataAccess_Update:");
            Trace.WriteLine("Index \t----- \tMilliseconds \t----- \tUpdateRows");
            var times = 10;
            for (var index = 0; index < times; index++)
            {
                var ms = action(index);
                Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  \t-----\t{2}", index, ms.Item1, ms.Item2));
            }
            Trace.WriteLine(string.Empty);
        }

        [TestMethod]
        public void TestTaolxDataAccess_Delete()
        {
            Trace.WriteLine("TestTaolxDataAccess_Delete:");
            Trace.WriteLine("Index \t----- \tMilliseconds \t----- \tDeleteRows");
            var index = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var db = TestConfig.CreateTestTaolxDbContext();
            var rows = db.Table2.Where(o => o.Id <= 0).Delete();
            db.Dispose();
            sw.Stop();
            Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  \t-----\t{2}", index, sw.ElapsedMilliseconds, rows));
            index++;
            sw = new Stopwatch();
            sw.Start();
            db = TestConfig.CreateTestTaolxDbContext();
            rows = db.Table2.Where(o => o.Id >= 0).Delete();
            db.Dispose();
            sw.Stop();
            Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  \t-----\t{2}", index, sw.ElapsedMilliseconds, rows));
        }

        [TestMethod]
        public void TestEntityFKDbContext_Add()
        {
            var db = TestConfig.CreateTestEntityFKDbContext();
            Stopwatch sw = null;
            db = TestConfig.CreateTestEntityFKDbContext();
            var entity = db.Table1.First();
            Func<Table3> f = () =>
            {
                var addEntity = new Table3()
                {
                    CreateTime = entity.CreateTime,
                    EndTime = entity.EndTime,
                    ExecutedCommnet = entity.ExecutedCommnet,
                    ExecutedResult = entity.ExecutedResult,
                    ExecutionTime = entity.ExecutionTime,
                    JobId = entity.JobId,
                    Remark = entity.Remark,
                    StartTime = entity.StartTime,
                    UpdateTime = entity.UpdateTime
                };
                return addEntity;
            };
            db.Dispose();
            Func<int, Tuple<long, int>> fun1 = (index) =>
            {
                sw = new Stopwatch();
                sw.Start();
                db = TestConfig.CreateTestEntityFKDbContext();
                var rows = 100;
                using (db)
                {
                    var tran = db.Database.BeginTransaction();
                    var list = new List<Table3>();
                    for (var rowIndex = 0; rowIndex < rows; rowIndex++)
                        list.Add(f());
                    db.Table3.AddRange(list);
                    db.SaveChanges();
                    tran.Commit();
                }
                sw.Stop();
                return new Tuple<long, int>(sw.ElapsedMilliseconds, rows);
            };

            Func<int, Tuple<long, int>> fun2 = (index) =>
            {
                sw = new Stopwatch();
                sw.Start();
                db = TestConfig.CreateTestEntityFKDbContext();
                var rows = 100;
                using (db)
                {
                    var tran = db.Database.BeginTransaction();
                    var list = new List<Table3>();
                    for (var rowIndex = 0; rowIndex < rows; rowIndex++)
                        db.Table3.Add(f());
                    db.SaveChanges();
                    tran.Commit();
                }
                sw.Stop();
                return new Tuple<long, int>(sw.ElapsedMilliseconds, rows);
            };

            Trace.WriteLine("TestEntityFKDbContext_Add:");
            Trace.WriteLine("Index \t----- \tMilliseconds \t----- \t Count");
            var times = 10;
            for (var index = 0; index < times; index++)
            {
                var ms = fun1(index);
                Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  \t----- \t {2}", index, ms.Item1, ms.Item2));
            }
            times = 20;
            for (var index = 10; index < times; index++)
            {
                var ms = fun2(index);
                Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  \t----- \t {2}", index, ms.Item1, ms.Item2));
            }
            Trace.WriteLine(string.Empty);
        }

        [TestMethod]
        public void TestEntityFKDbContext_Update()
        {
            var db = TestConfig.CreateTestEntityFKDbContext();
            Stopwatch sw = null;
            db = TestConfig.CreateTestEntityFKDbContext();
            var entity = db.Table3.First();
            db.Dispose();
            Func<int, Tuple<long, int>> action = (index) =>
            {
                sw = new Stopwatch();
                sw.Start();
                db = TestConfig.CreateTestEntityFKDbContext();
                var updateRows = 0;
                using (db)
                {
                    var trn = db.Database.BeginTransaction();
                    for (var rindex = 0; rindex < 100; rindex++)
                    {
                        entity.CreateTime.AddDays(1);
                        db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                        updateRows += db.SaveChanges();
                    }
                    trn.Commit();
                    sw.Stop();
                    return new Tuple<long, int>(sw.ElapsedMilliseconds, updateRows);
                }
            };
            Trace.WriteLine("TestEntityFKDbContext_Update:");
            Trace.WriteLine("Index \t----- \tMilliseconds \t-----\t UpdateRows ");
            var times = 10;
            for (var index = 0; index < times; index++)
            {
                var ms = action(index);
                Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  \t-----\t{2}", index, ms.Item1, ms.Item2));
            }
            Trace.WriteLine(string.Empty);
        }

        [TestMethod]
        public void TestEntityFKDbContext_Delete()
        {
            Trace.WriteLine("TestEntityFKDbContext_Delete:");
            Trace.WriteLine("Index \t----- \tMilliseconds \t----- \tDeleteRows");
            var index = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            var db = TestConfig.CreateTestEntityFKDbContext();
            var rows = db.Table3.AsNoTracking().ToList();
            db.Dispose();
            sw.Stop();
            Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  \t-----\t{2}", index, sw.ElapsedMilliseconds, rows.Count));
            index++;
            sw = new Stopwatch();
            sw.Start();
            db = TestConfig.CreateTestEntityFKDbContext();
            var tran = db.Database.BeginTransaction();
            rows.ForEach(entity =>
            {
                db.Entry(entity).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            });
            db.SaveChanges();
            tran.Commit();
            db.Dispose();
            sw.Stop();
            Trace.WriteLine(string.Format("{0}  \t----- \t {1} \t ms  \t-----\t{2}", index, sw.ElapsedMilliseconds, rows.Count));
        }
    }
}
