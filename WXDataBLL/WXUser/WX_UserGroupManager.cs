using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXDataModel;

namespace WXDataBLL.WXUser
{
    public class WX_UserGroupManager:BaseBLL<WX_UserGroup>
    {
        public List<WX_UserGroup> GetGroupList(string appId)
        {
            List<WX_UserGroup> list = base.GetAll().Where(t => t.AppId == null || t.AppId.Equals(appId)).ToList();
            return list;
        }
    }
}
