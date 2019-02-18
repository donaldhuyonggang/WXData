﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXDataDAL.WX;
using WXDataModel;
using WXDataDAL.WX;
namespace WXDataBLL.WXUser
{
    public class WX_UserManager:BaseBLL<WX_User>
    {
        public  bool NewUpdate(WX_User user) {
            return new WX_UserService().NewUpdate(user);
        }
        public bool AddTag(WX_User user, int tagid)
        {
            return new WX_UserService().AddTag(user, tagid);
        }
        public bool RemoveTag(WX_User user, int tagid)
        {
            return new WX_UserService().RemoveTag(user, tagid);
        }
    }
}
