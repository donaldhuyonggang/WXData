using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXDataDAL.WX;
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

        public WX_UserTag GetByPK(int tagid,string appid)
        {
            return new WX_UserTagService().GetByPK(tagid, appid);
        }
        //public override bool Update(WX_UserTag info)
        //{
        //    WX_UserTag user = base.GetByPK(info.TagId);
        //    user.TagName = info.TagName;

        //    return base.Update(user);
        //}
    }

}
