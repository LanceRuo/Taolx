using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EntityDemo.Entities;

namespace EntityDemo
{
    class TempDbContext : DbContext
    {

        public IDbSet<MerInfo> Entity1 { set; get; }
    }
}
