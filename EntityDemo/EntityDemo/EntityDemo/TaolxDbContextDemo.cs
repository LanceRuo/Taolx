using EntityDemo.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taolx.Common.DataAccess;
using EntityFramework.Extensions;
namespace EntityDemo
{
    public class TaolxDbContextDemo : TaolxDbContext
    {

        private readonly static string readConnectionString = "Server=10.1.26.10;Database=paymentdb;Uid=dev_ro;Pwd=server_RO@taolx.com;Port = 3306;";

        private readonly static string writeConnectionString = "Server=10.1.26.10;Database=paymentdb;Uid=dev;Pwd=server1@taolx.com;Port = 3306;";


        public TaolxDbSet<MerInfo> Entity1 { set; get; }



        public TaolxDbContextDemo() : base(readConnectionString, writeConnectionString)
        {
            Entity1 = new TaolxDbSet<MerInfo>();
            //Entity1.TaolxDbContext = this;

            Entity1.Select(o => o).Delete();
        }
         
    }
}
