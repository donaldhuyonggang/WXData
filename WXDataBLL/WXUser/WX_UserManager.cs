using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXDataModel;
using WXDataDAL.WX;
namespace WXDataBLL.WXUser
{
    public class WX_UserManager:BaseBLL<WX_User>
    {
        public  bool NewUpdate(WX_User user) {
            return new WX_UserService().NewUpdate(user);
        }
    }
}
