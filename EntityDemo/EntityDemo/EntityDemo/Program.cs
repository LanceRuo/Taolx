﻿using EntityDemo.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taolx.Common.DataAccess;
namespace EntityDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<TempDbContext>(null);

            TempDbContext dbc = new TempDbContext();

            var rrr = dbc.Entity1.Where(o => o.Id >= 1).Single();
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
                    db2.Entity1.Where(o => o.Id < 0).Delete();
                      
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
