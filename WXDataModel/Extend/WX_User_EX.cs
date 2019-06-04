using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXDataModel.Extend
{
    public static class WX_User_EX
    {
        public static string GetFollowStatus(this WX_User user)
        {
            return user.UnSubscribeTime == null ? "已关注" : "已退订";
        }
        public static string GetSysUser(this WX_User user)
        {
            return user.SYS_User == null ? "未分配" : user.SYS_User.UserName;
        }

        public static List<string> GetOpenIdList(this List<WX_User> userList)
        {
            List<string> openIdList = new List<string>();
            foreach (var i in userList)
            {
                openIdList.Add(i.OpenID);
            }
            return openIdList;
        }

        public static string GetSummary(this int i)
        {
            string r = "";
            switch (i)
            {
                case 1:
                    r = "公众号搜索";
                    break;
                case 17:
                    r = "名片分享";
                    break;
                case 30:
                    r = "扫描二维码 ";
                    break;
                case 43:
                    r = "图文页右上角菜单";
                    break;
                case 51:
                    r = "支付后关注";
                    break;
                case 57:
                    r = "图文页内公众号名称";
                    break;
                case 75:
                    r = "公众号文章广告";
                    break;
                case 78:
                    r = "朋友圈广告";
                    break;
                default:
                    r = "其他合计";
                    break;
            }
            return r;
        }
    }
}
