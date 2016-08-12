using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityDemo.Entities
{
    /// <summary>
    /// 交易信息表,trade_info
    /// </summary>
    [Table("trade_info")]
    public class TradeInfo : BaseEntity
    {
        /// <summary>
        /// 订单信息id
        /// </summary>
        public int OrderInfoId { set; get; }

        /// <summary>
        /// 外部订单号,
        /// 银联特殊性:退货时, 退货的订单号是要新生产的,不是支付的订单号
        /// </summary>
        public string OutOrderId { set; get; }

        /// <summary>
        /// 支付方式:
        /// 支付宝:支付宝交易号;
        /// 微信:微信交易号
        /// 银联:支付时支付成功后的traceNo(跟踪号); 
        /// </summary>
        public int PayType { set; get; }

        /// <summary>
        /// 支付设备
        /// </summary>
        public int DeviceType { set; get; }

        /// <summary>
        /// 第三方交易号
        /// 支付宝:支付宝交易号;
        /// 微信:微信交易号
        /// 银联:支付时支付成功后的queryId; 
        /// </summary>
        public string TradeNo { set; get; }

        /// <summary>
        /// 第三方交易流水:
        /// 支付宝:支付时为"",退款时为退款批次号;---
        /// 微信:支付时为"",退款时为退款批次号;---
        /// 银联:支付时支付成功后的跟踪号(queryID),退货时为退货的queryID
        /// </summary>
        public string TradeSerialNo { set; get; }

        /// <summary>
        /// 交易金额,单位元,两位小数
        /// </summary>
        public decimal TradeAmount { set; get; }

        /// <summary>
        /// 交易类型
        /// </summary>
        public int TradeType { set; get; }

        private string _outTradeBatchNo;

        /// <summary>
        /// 外部交易批次号,
        /// 当支付时,该交易批次为支付交易流水号,该流水号会返回给商户; 
        /// 当退款时,商户传入的批次号.
        /// </summary>
        public string OutTradeBatchNo
        {
            set { _outTradeBatchNo = value; }
            get
            {
                return _outTradeBatchNo == null ? (_outTradeBatchNo = string.Empty) : _outTradeBatchNo;
            }
        }

        /// <summary>
        /// 交易结果
        /// </summary>
        public int TradeResultType { set; get; }

        private string _resultCode;

        /// <summary>
        /// 交易结果code
        /// </summary>
        public string ResultCode
        {
            set { _resultCode = value; }
            get
            {
                return _resultCode == null ? (_resultCode = string.Empty) : _resultCode;
            }
        }

        private string _resultMessage;
        /// <summary>
        /// 交易结果message
        /// </summary>
        public string ResultMessage
        {
            set { _resultMessage = value; }
            get
            {
                return _resultMessage == null ? (_resultMessage = string.Empty) : _resultMessage;
            }
        }
    }
}
