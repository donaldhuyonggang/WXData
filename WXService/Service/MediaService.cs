﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXService.Utility;

namespace WXService.Service
{
    public class MediaService : BaseService
    {
        private const string MEDIA_ADD_NEWS = "https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={0}";
        private const string MEDIA_ADD_MATERIAL = "https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}&type={1}";
        private const string MEDIA_GET = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}";
        private string FileName { get; set; }
        public MediaService(string appId, string appSecert) 
            : base(appId, appSecert)
        {
        }


        private string GetCommand()
        {
            return string.Format(" -F media=@\"{0}\" \"https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={1}&type=images\""
                , this.FileName
                , this.Get_Access_Token()
                );
        }
        /// <summary>
        /// 运行cmd命令
        /// 不显示命令窗口
        /// </summary>
        /// <param name="cmdExe">指定应用程序的完整路径</param>
        /// <param name="cmdStr">执行命令行参数</param>
        public static string RunCmd2(string cmdStr)
        {
            try
            {
                using (Process myPro = new Process())
                {
                    //指定绝对路径
                    myPro.StartInfo.FileName = "D:/curl-7.64.0-win64-mingw/bin/curl.exe";
                    //使用环境变量路径
                    string enPath = Environment.GetEnvironmentVariable("CURL_HOME");
                    //LogOperate.Write("当前命令的环境变量：" + enPath);
                    //myPro.StartInfo.FileName = enPath + @"\curl.exe";
                    myPro.StartInfo.UseShellExecute = false;
                    myPro.StartInfo.RedirectStandardInput = true;
                    myPro.StartInfo.RedirectStandardOutput = true;
                    myPro.StartInfo.RedirectStandardError = true;
                    myPro.StartInfo.CreateNoWindow = true;
                    myPro.StartInfo.Arguments = cmdStr; //指定参数
                    myPro.Start();
                    myPro.StandardInput.AutoFlush = true;

                    //获取cmd窗口的输出信息
                    string output = myPro.StandardOutput.ReadToEnd();

                    myPro.WaitForExit();
                    myPro.Close();

                    return output;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        #region 远程调用
        /// <summary>
        /// 上传图片返回图片id
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public JObject PostImage(string filename)
        {
            try
            {
                //D:\Y2\5.项目\WXData\WXDataUI\
                this.FileName = AppDomain.CurrentDomain.BaseDirectory + "Upload/" + filename;
                string command = GetCommand();
                //执行命令获取mediaid
                string backdata = RunCmd2(command);
                //LogOperate.Write("backdata:" + backdata);

                JObject obj = JObject.Parse(backdata);
                JToken media_id = obj["media_id"];
                if (media_id == null)
                    throw new Exception($"获取media_id失败，" + backdata);
                return obj;
            }
            catch (Exception ex)
            {
                //LogOperate.Write("上传素材出错：" + ex.Message + "|" + ex.StackTrace);
            }
            return null;
        }
        #endregion


        public string Get(string type,int offset = 0,int count = 20)
        {
            string url = string.Format(MEDIA_GET, this.Get_Access_Token());
            var json = new
            {
                type = type,
                offset = offset,
                count = count,
            };
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(json));
            return respJson;
        }


    }
}
