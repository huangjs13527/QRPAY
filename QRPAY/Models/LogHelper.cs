using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;

namespace QRPAY
{
    public class LogHelper
    {
        private static string logDirectory = System.Web.HttpContext.Current.Server.MapPath("~/Log");
        private static object _locker = new object();
        private static StreamWriter writer;
        private static FileStream fileStream = null;

        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="message"></param>
        public static void Write(string message)
        {
            var dt = DateTime.Now;
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            var filename = dt.ToString("yyyy-MM-dd") + ".txt";
            var logFile = Path.Combine(logDirectory, filename);
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(logFile);
            if (!fileInfo.Exists)
            {
                fileStream = fileInfo.Create();
                writer = new StreamWriter(fileStream);
            }
            else
            {
                fileStream = fileInfo.Open(FileMode.Append, FileAccess.Write);
                writer = new StreamWriter(fileStream);
            }

            writer.WriteLine(dt.ToString("yyyy-MM-dd HH:mm:ss"));
            writer.WriteLine(message);
            writer.WriteLine();

            //释放资源
            writer.Dispose();
            fileStream.Dispose();

        }

        private static void WriteUseThread(object state)
        {
            lock (_locker)
            {
                var message = state as string;
                Write(message);
            }
        }

        /// <summary>
        /// 写入日志（异步操作）
        /// </summary>
        /// <param name="message"></param>
        public static void WriteAsync(string message)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(WriteUseThread), message);
        }
    }
}
