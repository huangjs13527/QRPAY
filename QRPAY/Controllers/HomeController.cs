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