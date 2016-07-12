using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhantomJSDemo.Models
{
    public class Deal
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        [JsonProperty("productId")]
        public string ProductId { set; get; }

        /// <summary>
        /// 商品名称
        /// </summary>
        [JsonProperty("productName")]
        public string ProductName { set; get; }

        /// <summary>
        /// 起价
        /// </summary>
        [JsonProperty("startingPrice")]
        public int StartingPrice { set; get; }

        /// <summary>
        /// 出发城市
        /// </summary>

        [JsonProperty("departure")]
        public string Departure { set; get; }

        /// <summary>
        /// 目的地城市
        /// </summary>
        [JsonProperty("arrivalCity")]
        public string ArrivalCity { set; get; }

        /// <summary>
        /// 目的地国家
        /// </summary>
        [JsonProperty("arrivalCountry")]
        public string ArrivalCountry { set; get; }

        /// <summary>
        /// 供应商
        /// </summary>
        [JsonProperty("supplier")]
        public string Supplier { set; get; }

        /// <summary>
        /// 访问量
        /// </summary>
        [JsonProperty("pv")]
        public int PV { set; get; }

        /// <summary>
        /// 已售数量
        /// </summary>
        [JsonProperty("soldCount")]
        public int SoldCount { set; get; }

        /// <summary>
        /// 航司
        /// </summary>
        [JsonProperty("air")]
        public string Air { set; get; }

        /// <summary>
        /// 收藏次数
        /// </summary>
        [JsonProperty("collect")]
        public int Collect { set; get; }

    }
}
