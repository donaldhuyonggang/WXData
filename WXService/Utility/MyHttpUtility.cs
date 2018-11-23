using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WXService.Utility
{
    /// <summary>
    /// html 请求，处理工具类。
    /// </summary>
    public static  class MyHttpUtility
    {
        /// <summary>
        /// 读取请求对象的内容，只能读一次
        /// </summary>
        /// <param name="request">HttpRequest对象</param>
        /// <returns>string</returns>
        public static string ReadRequest(HttpRequestBase request)
        {
            string reqStr = string.Empty;
            using (Stream s = request.InputStream)
            {
                using (StreamReader reader = new StreamReader(s, Encoding.UTF8))
                {
                    reqStr = reader.ReadToEnd();
                }
            }
            return reqStr;
        }

        public static string SendGet(string url)
        {
            return "";
        }
        public static string SendPost(string url,string data)
        {
            return "";
        }
        public static string SendFile(string url,byte[] file)
        {
            return "";
        }
    }
}
