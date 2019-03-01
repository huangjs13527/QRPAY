using ECardPass.Project.Infrastructure.XML;
using Newtonsoft.Json;
using QRPAY.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace QRPAY.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// QR支付
        /// </summary>
        /// <param name="RETURNTYPE">
        /// 2时,建行直接返回商户展示二维码图片的网页
        /// 3时，分2步请求地址QRURL
        /// </param>
        /// <returns></returns>
        [Route("pay/ccb/payment")]
        public ActionResult Payment(int RETURNTYPE = 3)
        {
            QrDemo qr = new QrDemo();
            var res = qr.PAY(RETURNTYPE);
            if (RETURNTYPE == 2)
            {
                this.Response.Write(res);
                this.Response.End();
                return Content("");
            }
            return this.Redirect(res);
        }

        /// <summary>
        /// 支付回调
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [Route("pay/ccb/notice")]
        public ActionResult Notice(PayNoticeData model)
        {
            LogHelper.Write("进入pay/ccb/notice");
          
            HttpContext.Request.InputStream.Position = 0;
            byte[] bytes = new byte[HttpContext.Request.InputStream.Length];
            HttpContext.Request.InputStream.Read(bytes, 0, bytes.Length);
            string inputstr = Encoding.GetEncoding("utf-8").GetString(bytes);
            LogHelper.Write("进入pay/ccb/notice PayNoticeData：" + inputstr);
            if (model != null && model.SUCCESS.Equals("Y", StringComparison.OrdinalIgnoreCase))
            {
                //回调成功，todo
                LogHelper.Write("进入pay/ccb/notice SUCCESS：Y");
            }
            return Content(inputstr);
        }
        /// <summary>
        /// 同步跳转
        /// </summary>
        /// <returns></returns>
        [Route("pay/ccb/callback")]
        public ActionResult CallBack()
        {
            LogHelper.Write("进入pay/ccb/CallBack");
            return Content("恭喜，支付成功！");
        }

        /// <summary>
        /// 退款
        /// </summary>
        /// <param name="RETURNTYPE"></param>
        /// <returns></returns>
        [Route("pay/ccb/refund")]
        public ActionResult Refund(string OrderId)
        {
            QrDemo qr = new QrDemo();
            var res = qr.Refund(OrderId);
            return Content(res);
        }

        /// <summary>
        /// 退款回调通知
        /// </summary>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [Route("pay/ccb/refundnotice")]
        public ActionResult RefundNotice()
        {
            return Content("SUCCESS");
        }

        public void Test()
        {
            var tx_info = new RefundRequest_TX_INFO()
            {
                MONEY = "0.01",
                ORDER = "105000148164313190228144018226"
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
            string returstr = HttpClientUtil.HttpPost("http://localhost:2582/pay/ccb/notice", request, "text/xml");


            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("ORDERID", "123123");
            dic.Add("POSID", "456456");
            string data = string.Empty;
            foreach (string key in dic.Keys)
            {
                data += key + "=" + dic[key] + "&";
            }
            string payStr = HttpClientUtil.HttpPost("http://localhost:2582/pay/ccb/notice", data.Trim('&')); //提交网银网关地址
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}