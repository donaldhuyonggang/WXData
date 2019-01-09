using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXDataModel;

namespace WXDataBLL.WXUser
{
    public class WX_UserTagManager: BaseBLL<WX_UserTag>
    {
        public List<WX_UserTag> GetTagList(string appId)
        {
            List<WX_UserTag> list = base.GetAll().Where(t => t.AppId == null || t.AppId.Equals(appId)).ToList();
            return list;
        }
    }

}
