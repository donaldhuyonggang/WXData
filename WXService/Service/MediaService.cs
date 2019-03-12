using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXDataModel;
using WXService.Utility;

namespace WXService.Service
{
    public class MediaService : BaseService
    {
        private const string MEDIA_ADD_NEWS = "https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={0}";
        private const string MEDIA_ADD_MATERIAL = "https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}&type={1}";
        private const string MEDIA_GET = "https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}";
        private const string MEDIA_DELETE_MATERIAL = "https://api.weixin.qq.com/cgi-bin/material/del_material?access_token={0}";

        private const string MEDIA_UPLOAD_TEMP = "https://api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}";
        private const string MEDIA_GET_TEMP = "https://api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}";
        private string FileName { get; set; }
        public MediaService(string appId, string appSecert) 
            : base(appId, appSecert)
        {
        }

        /// <summary>
        /// 获取curl上传文件命令
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetCommand(string url)
        {
            return string.Format(" -F media=@\"{0}\" \""+ url +"\""
                , this.FileName
                , this.Get_Access_Token()
                );
        }
        /// <summary>
        /// 获取curl上传文件命令
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetTempCommand(string url)
        {
            return string.Format(" -I -G  \"" + url + "\""
                );
        }

        /// <summary>
        /// 运行cmd命令
        /// 不显示命令窗口
        /// </summary>
        /// <param name="cmdExe">指定应用程序的完整路径</param>
        /// <param name="cmdStr">执行命令行参数</param>
        public static string RunCmd(string cmdStr)
        {
            try
            {
                using (Process myPro = new Process())
                {
                    //指定绝对路径
                    //myPro.StartInfo.FileName = "D:/curl-7.64.0-win64-mingw/bin/curl.exe";
                    //使用环境变量路径
                    string enPath = Environment.GetEnvironmentVariable("CURL_HOME");
                    //LogOperate.Write("当前命令的环境变量：" + enPath);
                    myPro.StartInfo.FileName = enPath + @"\curl.exe";
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


        #region 上传永久素材
        /// <summary>
        /// 上传永久素材
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public JObject PostImage(string filename)
        {
            try
            {
                //D:\Y2\5.项目\WXData\WXDataUI\
                this.FileName = AppDomain.CurrentDomain.BaseDirectory + "Upload/" + filename;
                string command = GetCommand(string.Format(MEDIA_ADD_MATERIAL,this.Get_Access_Token(),"image"));
                //执行命令获取mediaid
                string backdata = RunCmd(command);
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

        #region 上传临时素材
        /// <summary>
        /// 上传临时素材
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public JObject UploadTemp(string filename,string filetype)
        {
            try
            {
                //D:\Y2\5.项目\WXData\WXDataUI\
                this.FileName = AppDomain.CurrentDomain.BaseDirectory + "Upload/" + filename;
                string command = GetCommand(string.Format(MEDIA_UPLOAD_TEMP, this.Get_Access_Token(), filetype));
                //执行命令获取mediaid
                string backdata = RunCmd(command);
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


        /// <summary>
        /// 获取永久素材
        /// </summary>
        /// <param name="type"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
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
        /// <summary>
        /// 根据mediaid删除永久素材
        /// </summary>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public string Delete(string mediaId)
        {
            string url = string.Format(MEDIA_DELETE_MATERIAL, this.Get_Access_Token());
            var json = new
            {
                media_id = mediaId
            };
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(json));
            return respJson;
        }
        /// <summary>
        /// 添加图文消息
        /// </summary>
        /// <param name="news"></param>
        /// <returns></returns>
        public string AddNews(WX_Media_News news)
        {
            var json = new
            {
                articles = new List<Object>()
                {
                    new 
                    {
                        title = news.title,
                        thumb_media_id = news.thumb_media_id,
                        author = news.author,
                        digest = news.digest,
                        show_cover_pic = news.show_cover_pic,
                        content = news.content,
                        content_source_url = news.content_source_url,
                        need_open_comment = news.need_open_comment,
                        only_fans_can_comment = news.only_fans_can_comment,
                    }
                }
            };
            string url = string.Format(MEDIA_ADD_NEWS, this.Get_Access_Token());
            string respJson = MyHttpUtility.SendPost(url, JsonConvert.SerializeObject(json));
            return respJson;
        }

        /// <summary>
        /// 获取临时素材url
        /// </summary>
        /// <param name="mediaid"></param>
        /// <returns></returns>
        public string GetTempUrl(string mediaid)
        {

            string url = string.Format(MEDIA_GET_TEMP, this.Get_Access_Token(), mediaid);
            string command = GetTempCommand(url);
            string respJson = RunCmd(command);

            //string url = string.Format("http://file.api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}"
            //    ,this.Get_Access_Token(),
            //    mediaid);
            //var json = new
            //{
            //    access_token = this.Get_Access_Token(),
            //    media_id = mediaid
            //};
            //string respJson = MyHttpUtility.SendGet(url);
            return respJson;
        }
    }
}
