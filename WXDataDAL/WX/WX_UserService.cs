
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WXDataModel;

namespace WXDataDAL.WX
{
    public class WX_UserService: BaseDAL<WX_User>
    {
        public bool NewUpdate(WX_User user)
        {
            WXDataEntities db = new WXDataEntities();
            WX_User users = db.WX_User.Find(user.OpenID);
            users.UserName = user.UserName;
            users.Telphone = user.Telphone;
            user.Remark = users.Remark;
            return db.SaveChanges() > 0;
        }
        public bool AddTag(WX_User user, int tagid)
        {
            using (var db = new WXDataEntities())
            {
                var p_tagid = new SqlParameter("@tagid", tagid);
                var p_openid = new SqlParameter("@openid", user.OpenID);
                var p_appid = new SqlParameter("@appid", user.AppId);
                var result = db.Database.ExecuteSqlCommand(@"Insert into WX_UserTagRelation values(@tagid,@openid,@appid)",p_tagid,p_openid,p_appid);
                return result > 0;
            }
        }
        public bool RemoveTag(WX_User user, int tagid)
        {
            using (var db = new WXDataEntities())
            {
                var p_tagid = new SqlParameter("@tagid", tagid);
                var p_openid = new SqlParameter("@openid", user.OpenID);
                var result = db.Database.ExecuteSqlCommand(@"Delete from WX_UserTagRelation where TagId=@tagid and OpenId=@openid", p_tagid, p_openid);
                return result > 0;
            }
        }
    }
}
