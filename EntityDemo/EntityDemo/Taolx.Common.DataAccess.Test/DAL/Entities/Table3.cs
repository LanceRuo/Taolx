using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Taolx.Common.DataAccess.Test.DAL
{
    /// <summary>
    /// 任务执行日志
    /// </summary>
    [Serializable]
    [Table("table3")]
    public class Table3
    {
        /// <summary>
        /// 自增主键id
        /// </summary>
        [Key]
        [Column("Id")]
        public int Id { set; get; }

        /// <summary>
        /// 任务id
        /// </summary>
        [Column("JobId")]
        public int JobId { set; get; }

        /// <summary>
        /// 开始执行时间
        /// </summary>
        [Column("StartTime")]
        public DateTime StartTime { set; get; }

        /// <summary>
        /// 执行结束时间
        /// </summary>
        [Column("EndTime")]
        public DateTime EndTime { set; get; }

        /// <summary>
        /// 执行时长,单位ms
        /// </summary>
        [Column("ExecutionTime")]
        public long ExecutionTime { set; get; }

        /// <summary>
        /// 执行后的返回的内容
        /// </summary>
        [Column("ExecutedCommnet")]
        public string ExecutedCommnet { set; get; }

        /// <summary>
        /// 执行结果
        /// </summary>
        [Column("ExecutedResult")]
        public int ExecutedResult { set; get; }

        /// <summary>
        /// 备注
        /// </summary>
        [Column("Remark")]
        public string Remark { set; get; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Column("CreateTime")]
        public DateTime CreateTime { set; get; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("UpdateTime")]
        public DateTime UpdateTime { set; get; }

    }
}
