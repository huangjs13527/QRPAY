using java.lang;
using java.util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft;
using Newtonsoft.Json;
using QRPAY.Models;
using ECardPass.Project.Infrastructure.XML;

namespace QRPAY
{
    public class QrDemo
    {

        /// <summary>
        /// 聚合扫码只能上送2或3
        /// </summary>
        /// <param name="RETURNTYPE"></param>
        public string PAY(int RETURNTYPE = 3)
        {

            string ORDERID = QRModel.CreateOrderId();
            string PAYMENT = "0.01";
            string CURCODE = "01";
            string TXCODE = "530550";
            string REMARK1 = "";
            string REMARK2 = "";
            string TIMEOUT = "";


            //MAC字段参与摘要运算的字符串及其顺序
            StringBuilder tmp = new StringBuilder();
            tmp.append("MERCHANTID=");
            tmp.append(QRModel.MERCHANTID);
            tmp.append("&POSID=");
            tmp.append(QRModel.POSID);
            tmp.append("&BRANCHID=");
            tmp.append(QRModel.BRANCHID);
            tmp.append("&ORDERID=");
            tmp.append(ORDERID);
            tmp.append("&PAYMENT=");
            tmp.append(PAYMENT);
            tmp.append("&CURCODE=");
            tmp.append(CURCODE);
            tmp.append("&TXCODE=");
            tmp.append(TXCODE);
            tmp.append("&REMARK1=");
            tmp.append(REMARK1);
            tmp.append("&REMARK2=");
            tmp.append(REMARK2);
            tmp.append("&RETURNTYPE=");
            tmp.append(RETURNTYPE);
            tmp.append("&TIMEOUT=");
            tmp.append(TIMEOUT);
            tmp.append("&PUB=");
            tmp.append(QRModel.PUB32TR2);

            //提交的参数
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CCB_IBSVersion", "V6");
            dic.Add("MERCHANTID", QRModel.MERCHANTID);
            dic.Add("POSID", QRModel.POSID);
            dic.Add("BRANCHID", QRModel.BRANCHID);
            dic.Add("ORDERID", ORDERID);
            dic.Add("PAYMENT", PAYMENT);
            dic.Add("CURCODE", CURCODE);
            dic.Add("TXCODE", TXCODE);
            dic.Add("REMARK1", REMARK1);
            dic.Add("REMARK2", REMARK2);
            dic.Add("RETURNTYPE", RETURNTYPE.ToString());
            dic.Add("TIMEOUT", TIMEOUT);
            dic.Add("MAC", MD5.GetMD5Str(tmp.ToString()));
            string data = string.Empty;
            foreach (string key in dic.Keys)
            {
                data += key + "=" + dic[key] + "&";
            }
            string payStr = HttpClientUtil.HttpPost(QRModel.BankURL, data.Trim('&')); //提交网银网关地址
            //2 直接返回带二维码的网页
            if (RETURNTYPE == 2) return payStr;

            var payData = JsonConvert.DeserializeObject<PayResData>(payStr);
            var qrurlStr = HttpClientUtil.HttpGet(payData.PAYURL);
            var qrurlData = JsonConvert.DeserializeObject<PayResData>(qrurlStr);
            qrurlData.QRURL = HttpUtility.UrlDecode(qrurlData.QRURL);//浏览器直接打开地址
            return qrurlData.QRURL;
        }


        /// <summary>
        /// 退款申请
        /// </summary>
        public string Refund(string OrderId)
        {
            var tx_info = new RefundRequest_TX_INFO()
            {
                MONEY = "0.01",
                ORDER = OrderId
            };
            var refundRequest = new RefundRequest()
            {
                REQUEST_SN = QRModel.CreateREQUEST_SN(),
                CUST_ID = QRModel.MERCHANTID,
                USER_ID = "105000148164313-001",
                PASSWORD = "jlkj2019",
                LANGUAGE = "CN",
                TX_CODE = "5W1004",
                SIGNCERT = "1111",
                SIGN_INFO = "1111",
                TX_INFO = tx_info
            };
            string request = XmlHelper.SerializeToXmlStr(refundRequest, false);
            string returstr = HttpClientUtil.HttpPost("https://192.168.30.168:9010", request, "text/xml");
            //退款返回数据
            //var RETURN_CODE = XmlHelper.GetNodeValue(returstr, XmlHelper.XmlType.String, "RETURN_CODE");
            // if (RETURN_CODE.RETURN_CODE.Equals("000000"))
            //{
            //    //交易成功todo
            //}
            //var refundResponse = XmlHelper.XmlDeserialize<RefundResponse>(returstr);
            //if (refundResponse.RETURN_CODE.Equals("000000"))
            //{
            //    //交易成功todo
            //}
            return returstr;
        }
    }

}
