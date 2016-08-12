using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDemo.Entities
{
    /// <summary>
    /// 商户信息, mer_info
    /// </summary>
    [Table("mer_info")]
    public class MerInfo : BaseEntity
    {

        /// <summary>
        /// 商户id
        /// </summary>
        public string MerId { set; get; }

        /// <summary>
        /// 商户名称
        /// </summary>
        public string MerName { set; get; }

        /// <summary>
        /// 签名Key
        /// </summary>
        public string SignKey { set; get; }

    }
}
