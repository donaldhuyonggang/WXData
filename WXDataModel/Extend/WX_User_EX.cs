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
    }
}
