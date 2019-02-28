using java.security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace QRPAY
{
    public class MD5
    {
        public static String md5Str(String str)
        {
            if (str == null) return "";
            return md5Str(str, 0);
        }

        public static string GetMD5Str(string toCryString)
        {
            MD5CryptoServiceProvider hashmd5;
            hashmd5 = new MD5CryptoServiceProvider();
            return BitConverter.ToString(hashmd5.ComputeHash(Encoding.Default.GetBytes(toCryString))).Replace("-", "").ToLower();//asp是小写,把所有字符变小写
        }


        /**
         * 计算消息摘要。
         * @param data 计算摘要的数据。
         * @param offset 数据偏移地址。
         * @param length 数据长度。
         * @return 摘要结果。(16字节)
         */
        public static String md5Str(String str, int offset)
        {
            try
            {
                MessageDigest md5 = MessageDigest.getInstance("MD5");
                byte[] b = strToToHexByte(str);
                md5.update(b, offset, b.Length);
                return byteArrayToHexString(md5.digest());
            }
            catch (NoSuchAlgorithmException ex)
            {
                ex.printStackTrace();
                return null;
            }
           
        }

        /**
         *
         * @param b byte[]
         * @return String
         */
        public static String byteArrayToHexString(byte[] b)
        {
            String result = "";
            for (int i = 0; i < b.Length; i++)
            {
                result += byteToHexString(b[i]);
            }
            return result;
        }

        private static String[] hexDigits =
            {
        "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b",
        "c", "d", "e", "f"};

        /**
         * 将字节转换为对应的16进制明文
         * @param b byte
         * @return String
         */
        public static String byteToHexString(byte b)
        {
            int n = b;
            if (n < 0)
            {
                n = 256 + n;
            }
            int d1 = n / 16;
            int d2 = n % 16;
            return hexDigits[d1] + hexDigits[d2];
        }

        private static byte[] strToToHexByte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}