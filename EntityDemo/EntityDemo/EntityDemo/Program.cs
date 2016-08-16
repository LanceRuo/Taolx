using EntityDemo.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taolx.Common.DataAccess;
using EntityFramework.Extensions;
using System.Diagnostics;

namespace EntityDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<TempDbContext>(null);

            List<int> ssssssssss = new List<int>();
            ssssssssss.GroupBy(o => o).ToList().ForEach(item =>
            {

            });


            TempDbContext dbc = new TempDbContext();

            var rrr = dbc.Entity1.Where(o => o.Id < 1);



            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(rrr));

            Action a = () =>
            {

                using (TaolxDbContextDemo db = new TaolxDbContextDemo())
                {
                    var result = db.Entity1.Where(o => o.Id >= 1).Single();
                    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(result));
                }
                Console.WriteLine("-------------------\n");

                using (TaolxDbContextDemo2 db2 = new TaolxDbContextDemo2())
                {
                    var tt = db2.Entity1.Where(o => o.Id >= 0).FirstOrDefault();
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    db2.BeginTransaction();
                    for (var i = 0; i < 500; i++)
                    { 
                        tt.OrderInfoId = -100000;
                        db2.Entity1.Add(tt);
                    }
                    db2.Commit();
                    sw.Stop(); 
                    Console.WriteLine(sw.ElapsedMilliseconds/1000M);
                    Console.ReadLine();
                    db2.Entity1.Where(o => o.Id < 0);
                    var rrrr = db2.Entity1.Where(o => o.Id < 0).Delete();
                    var rrr222 = db2.Entity1.Where(o => o.Id < 0).Update(o => new TradeInfo { CreateTime = DateTime.MinValue });
                    var query = from s in db2.Entity1 select s;
                    query.OrderBy(o => o.Id).ThenBy(o => o.Id);
                    var gp = db2.Entity1.Where(o => o.Id > 300 && o.Id < 310).GroupBy(o => o.Id);
                    var gpl = gp.ToList(); 
                }
                Console.WriteLine("-------------------\n");
            };


            a();
            a();
            a();

            Console.ReadLine();

        }
    }
}
