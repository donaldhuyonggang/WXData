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
      
    }
}
