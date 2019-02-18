 
using System;
using System.Collections.Generic;
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
    }
}
