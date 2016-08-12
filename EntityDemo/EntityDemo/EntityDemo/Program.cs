using EntityDemo.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taolx.Common.DataAccess;
using EntityFramework.Extensions;
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
                    var t = db2.Entity1.Where(o => o.Id >= 0).FirstOrDefault();
                    db2.Entity1.Where(o => o.Id < 0);
                    var rrrr = db2.Entity1.Where(o => o.Id < 0).Delete();
                    var rrr222 = db2.Entity1.Where(o => o.Id < 0).Update(o => new TradeInfo { Id = -1 });
                    var query = from s in db2.Entity1 select t;
                    query.OrderBy(o => o.Id).ThenBy(o => o.Id);
                    var gp = db2.Entity1.Where(o => o.Id > 300 && o.Id < 310).GroupBy(o => o.Id);
                    var gpl = gp.ToList();
                    Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(t));
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
