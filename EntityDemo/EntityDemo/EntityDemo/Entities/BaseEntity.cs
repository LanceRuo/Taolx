using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityDemo.Entities
{
    /// <summary>
    /// BaseEntity
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// 自增主键id
        /// </summary>    
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key()]
        public int Id { set; get; }

        private DateTime? _createTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime
        {
            set { _createTime = value; }
            get
            {
                if (!_createTime.HasValue)
                    _createTime = DateTime.Now;
                return _createTime.Value;
            }
        }

        private DateTime? _updateTime;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            set { _updateTime = value; }
            get
            {
                if (!_updateTime.HasValue)
                    _updateTime = DateTime.Now;
                return _updateTime.Value;
            }
        }
    }
}
