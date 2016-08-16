using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taolx.Common.DataAccess.Test.DAL.TaolxDAL
{
    public class TestTaolxDbContext : TaolxDbContext
    {
        /// <summary>
        /// 读数据库连接字符串
        /// </summary>
        private readonly static string readConnectionString = TestConfig.ReadConnectionString;

        /// <summary>
        /// 写数据库连接字符串
        /// </summary>
        private readonly static string writeConnectionString = TestConfig.WriteConnectionString;

        /// <summary>
        /// Table1
        /// </summary>
        public TaolxDbSet<Table1> Table1 { set; get; }

        /// <summary>
        /// Table2
        /// </summary>
        public TaolxDbSet<Table2> Table2 { set; get; }

        /// <summary>
        /// Table3
        /// </summary>
        public TaolxDbSet<Table3> Table3 { set; get; }


        /// <summary>
        /// 构造方法
        /// </summary>
        public TestTaolxDbContext() : base(readConnectionString, writeConnectionString)
        {
        }

        /// <summary>
        /// 输出sql执行日志
        /// </summary>
        /// <param name="log"></param>
        public override void Log(string log)
        {
            //Debug.WriteLine(string.Format("{0}:{1}:{2}", GetType().FullName, DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), log));
        }
    }
}
