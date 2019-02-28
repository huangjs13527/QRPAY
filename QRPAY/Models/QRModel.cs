using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace QRPAY.Models
{
    public class QRModel
    {
        /// <summary>
        /// 商户代码
        /// </summary>
        public static string MERCHANTID = "105000148164313";
        /// <summary>
        /// 商户柜台代码
        /// </summary>
        public static string POSID = "032944805";
        /// <summary>
        /// 分行代码
        /// </summary>
        public static string BRANCHID = "360000000";
        /// <summary>
        /// PUB字段为对应柜台的公钥后30位
        /// </summary>
        public static string PUB32TR2 = "6efbe92dcd1a7f1bc665f9eb020111";
        /// <summary>
        /// 网银的网关地址
        /// </summary>
        public static string BankURL = "https://ibsbjstar.ccb.com.cn/CCBIS/ccbMain";
        //public string bankURL = "http://124.127.94.61:8001/CCBIS/ccbMain";

        /// <summary>
        /// 由商户提供，最长30位。
        /// 需按以下规则生成订单号：商户代码(15位)+自定义字符串(不超过15位)。字符串可包含数字、字母、下划线。
        /// 商户需保证订单号唯一。
        /// </summary>
        /// <returns></returns>
        public static string CreateOrderId()
        {
            return MERCHANTID + DateTime.Now.ToString("yyMMddHHmmssfff");
        }

        /// <summary>
        /// 生成请求序列码，16位数字
        /// </summary>
        /// <returns></returns>
        public static string CreateREQUEST_SN()
        {
            return DateTime.Now.ToString("yyMMddHHmmssfff");
        }
    }

    /// <summary>
    /// 支付回调数据
    /// </summary>
    public class PayNoticeData
    {

        /// <summary>
        /// POSID	商户柜台代码
        /// </summary>
        public string POSID { get; set; }

        /// <summary>
        ///BRANCHID	分行代码
        /// </summary>
        public string BRANCHID { get; set; }
        /// <summary>
        /// ORDERID	定单号
        /// </summary>
        public string ORDERID { get; set; }
        /// <summary>
        /// PAYMENT	付款金额
        /// </summary>
        public decimal PAYMENT { get; set; }
        /// <summary>
        /// CURCODE	币种
        /// </summary>
        public string CURCODE { get; set; }
        /// <summary>
        ///REMARK1	备注一
        /// </summary>
        public string REMARK1 { get; set; }

        /// <summary>
        ///REMARK2	备注二
        /// </summary>
        public string REMARK2 { get; set; }
        /// <summary>
        /// ACC_TYPE	账户类型
        /// </summary>
        public string ACC_TYPE { get; set; }
        /// <summary>
        /// SUCCESS	成功标志
        /// </summary>
        public string SUCCESS { get; set; }
        /// <summary>
        /// TYPE	接口类型
        /// </summary>
        public string TYPE { get; set; }
        /// <summary>
        /// REFERER	Referer信息
        /// </summary>
        public string REFERER { get; set; }
        /// <summary>
        /// CLIENTIP	客户端IP
        /// </summary>
        public string CLIENTIP { get; set; }
        /// <summary>
        /// ACCDATE	系统记账日期
        /// </summary>
        public string ACCDATE { get; set; }
        /// <summary>
        /// USRMSG	支付账户信息
        /// </summary>
        public string USRMSG { get; set; }
        /// <summary>
        ///USRINFO	客户加密信息
        /// </summary>
        public string USRINFO { get; set; }
        /// <summary>
        ///PAYTYPE	支付方式
        /// </summary>
        public string PAYTYPE { get; set; }
        /// <summary>
        ///SIGN	数字签名
        /// </summary>
        public string SIGN { get; set; }
      
    }

    /// <summary>
    /// 返回数据
    /// </summary>
    public class PayResData
    {
        public bool SUCCESS { get; set; }
        public string PAYURL { get; set; }
        public string QRURL { get; set; }
    }

    [XmlType("TX")]
    public class RefundRequest
    {

        /// <summary>
        ///  请求序列码，16位数字
        /// </summary>
        public string REQUEST_SN { get; set; }

        /// <summary>
        ///   商户号
        /// </summary>
        public string CUST_ID { get; set; }
        /// <summary>
        ///     <USER_ID>操作员号</USER_ID>
        /// </summary>
        public string USER_ID { get; set; }
        /// <summary>
        ///      <PASSWORD>密码</PASSWORD>
        /// </summary>
        public string PASSWORD { get; set; }
        /// <summary>
        ///      <TX_CODE>5W1004</TX_CODE>
        /// </summary>
        public string TX_CODE { get; set; }
        /// <summary>
        ///       <LANGUAGE>CN</LANGUAGE>
        /// </summary>
        public string LANGUAGE { get; set; }

        public RefundRequest_TX_INFO TX_INFO { get; set; }

        /// <summary>
        ///   <SIGN_INFO>签名信息</SIGN_INFO>
        /// </summary>
        public string SIGN_INFO { get; set; }

        /// <summary>
        ///    <SIGNCERT>签名CA信息</SIGNCERT>
        /// </summary>
        public string SIGNCERT { get; set; }
    }

    [XmlType("TX_INFO")]
    public class RefundRequest_TX_INFO
    {
        /// <summary>
        ///        <MONEY>退款金额</MONEY>
        /// </summary>
        public string MONEY { get; set; }
        /// <summary>
        ///        <ORDER>订单号码 </ORDER>
        /// </summary>
        public string ORDER { get; set; }
    }


    [XmlType("TX")]
    public class RefundResponse
    {

        /// <summary>
        ///  请求序列码，16位数字
        /// </summary>
        public string REQUEST_SN { get; set; }

        /// <summary>
        ///   商户号
        /// </summary>
        public string CUST_ID { get; set; }


        /// <summary>
        ///      <TX_CODE>5W1004</TX_CODE>
        /// </summary>
        public string TX_CODE { get; set; }
        /// <summary>
        ///       <LANGUAGE>CN</LANGUAGE>
        /// </summary>
        public string LANGUAGE { get; set; }
        /// <summary>
        ///       <RETURN_CODE>返回码</RETURN_CODE>
        /// </summary>
        public string RETURN_CODE { get; set; }
        /// <summary>
        ///       <RETURN_MSG>返回码说明</RETURN_MSG>
        /// </summary>
        public string RETURN_MSG { get; set; }

        public RefundResponse_TX_INFO TX_INFO { get; set; }

    }


    [XmlType("TX_INFO")]
    public class RefundResponse_TX_INFO
    {
        /// <summary>
        ///          <ORDER_NUM>订单号</ORDER_NUM>
        /// </summary>
        public string ORDER_NUM { get; set; }
        /// <summary>
        ///            <PAY_AMOUNT>支付金额</PAY_AMOUNT>
        /// </summary>
        public string PAY_AMOUNT { get; set; }

        /// <summary>
        ///          <AMOUNT>退款金额</AMOUNT>
        /// </summary>
        public string AMOUNT { get; set; }
        /// <summary>
        ///      <REM1>备注1</REM1>
        /// </summary>
        public string REM1 { get; set; }
        /// <summary>
        ///          <REM2>备注2</REM2>
        /// </summary>
        public string REM2 { get; set; }
    }

}