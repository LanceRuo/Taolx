using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taolx.Common.DataAccess;

namespace Taolx.Common.DataAccess
{


    /// <summary>
    /// InternalDbContext
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class InternalDbContext<T> : InternalDbContext where T : TaolxDbContext
    {
        public InternalDbContext(Func<List<Type>> getModelType, DbConnection existingConnection, bool contextOwnsConnection) : base(getModelType, existingConnection, contextOwnsConnection)
        {

        }
    }
}
