using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EntityDemo.Entities;
using MySql.Data.MySqlClient;
using System.Diagnostics;

namespace EntityDemo
{
    class TempDbContext : DbContext
    {
        private readonly static string readConnectionString = "Server=10.1.26.10;Database=paymentdb;Uid=dev_ro;Pwd=server_RO@taolx.com;Port = 3306;";

        public TempDbContext() : base(new MySqlConnection(readConnectionString), true)
        {
            base.Database.Log = (message) =>
            {
                Debug.WriteLine(message);
            };
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.RegisterEntityType(typeof(MerInfo));
        }

        // public IDbSet<MerInfo> Entity1 { set; get; }
    }
}
