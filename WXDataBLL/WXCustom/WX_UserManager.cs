using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXDataModel;
namespace WXDataBLL.WXCustom
{
    public class WX_UserManager:BaseBLL<WX_User>
    {
        
        public override bool Update(WX_User info)
        {
            WXDataEntities db = new WXDataEntities();
            WX_User user = db.WX_User.Find(info.OpenID);
            user.UserId = info.UserId;
            return db.SaveChanges() > 0;
        }
    }
}
