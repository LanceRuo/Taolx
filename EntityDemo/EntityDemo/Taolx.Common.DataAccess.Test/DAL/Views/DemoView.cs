using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taolx.Common.DataAccess.Test.DAL.Views
{
    public class DemoView
    {
        /// <summary>
        /// 自增主键id
        /// </summary> 
        public int Id { set; get; }

        /// <summary>
        /// 任务id
        /// </summary> 
        public int JobId { set; get; }

        /// <summary>
        /// 开始执行时间
        /// </summary> 
        public DateTime StartTime { set; get; }

        /// <summary>
        /// 执行结束时间
        /// </summary> 
        public DateTime EndTime { set; get; }

        /// <summary>
        /// 执行时长,单位ms
        /// </summary> 
        public long ExecutionTime { set; get; }

        /// <summary>
        /// 执行后的返回的内容
        /// </summary> 
        public string ExecutedCommnet { set; get; }

        /// <summary>
        /// 执行结果
        /// </summary> 
        public int ExecutedResult { set; get; }

        /// <summary>
        /// 备注
        /// </summary> 
        public string Remark { set; get; }

        /// <summary>
        /// 开始时间
        /// </summary> 
        public DateTime CreateTime { set; get; }

        /// <summary>
        /// 更新时间
        /// </summary> 
        public DateTime UpdateTime { set; get; }
    }
}
