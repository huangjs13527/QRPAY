using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;

namespace QRPAY
{
    public class HttpClientUtil
    {
        public static string HttpPost(string url, string param = null, string contentType = "application/x-www-form-urlencoded", Dictionary<string, string> dicHeadPara = null)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            if (dicHeadPara != null && dicHeadPara.Any())
            {
                foreach (string key in dicHeadPara.Keys)
                {
                    request.Headers.Add(key, dicHeadPara[key]);
                }
            }
            request.Method = "POST";
            request.ContentType = contentType;
            request.Accept = "*/*";
            request.Timeout = 5 * 60 * 1000;
            request.KeepAlive = false;
            request.AllowAutoRedirect = true;
            StreamWriter requestStream = null;
            WebResponse response = null;
            string responseStr = null;
            try
            {
                requestStream = new StreamWriter(request.GetRequestStream());
                requestStream.Write(param);
                requestStream.Close();
                response = request.GetResponse();
                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
               
            }
            finally
            {
                request = null;
                requestStream = null;
                response = null;
            }
            return responseStr;
        }
        public static string HttpGet(string url)
        {
            HttpWebRequest request;

            //如果是发送HTTPS请求  
           
                request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;
            WebResponse response = null;
            string responseStr = null;
          

            try
            {
                response = request.GetResponse();

                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return responseStr;
        }

    }
}