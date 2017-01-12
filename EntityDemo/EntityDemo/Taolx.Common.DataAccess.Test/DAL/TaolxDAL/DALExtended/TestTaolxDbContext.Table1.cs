using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Taolx.Common.DataAccess.Test.DAL.Views;

namespace Taolx.Common.DataAccess.Test.DAL.TaolxDAL
{

    public static class TestTaolxDbContextTable1
    {
        /// <summary>
        /// 根据JobId获取数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public static List<Table1> GetByJobId(this TaolxDbSet<Table1> source, int JobId)
        {
            const string sql = "select * from Table1 where id=@id";
            return source.SqlQuery(sql, JobId);
        }

        /// <summary>
        /// 根据JobId获取View数据
        /// </summary>
        /// <param name="source"></param>
        /// <param name="JobId"></param>
        /// <returns></returns>
        public static List<DemoView> GetViewByJobId(this TaolxDbSet<Table1> source, int JobId)
        {
            const string sql = "select * from Table1 where id=@id";
            return source.SqlQuery<DemoView>(sql, JobId);
        }
    }
}
