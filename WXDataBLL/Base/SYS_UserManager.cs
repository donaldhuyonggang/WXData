 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WXDataDAL;
using WXDataModel;

namespace WXDataBLL.Base
{
    public class SYS_UserManager : BaseBLL<SYS_User>
    {
        public SYS_User Login(SYS_User info)
        {
            return  new BaseDAL<SYS_User>()
                .Where(x => x.LoginId == info.LoginId && x.Password == info.Password).FirstOrDefault();
        }
    }
}
