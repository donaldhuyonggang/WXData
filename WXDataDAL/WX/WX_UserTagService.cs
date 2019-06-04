
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using WXDataModel;

namespace WXDataDAL.WX
{
    public class WX_UserTagService: BaseDAL<WX_UserTag>
    {
        public WX_UserTag GetByPK(int tagid, string appid)
        {
            return base.Where(t => t.TagId.Equals(tagid) && t.AppId.Equals(appid)).First();
        }

        public bool Delete(int tagid, string appid)
        {
            using (var db = new WXDataEntities())
            {
                var p_tagid = new SqlParameter("@tagid", tagid);
                var result = db.Database.ExecuteSqlCommand(@"Delete from WX_UserTagRelation where TagId=@tagid", p_tagid);
                result = db.SaveChanges();
            }     
            return base.Delete(new object[] { tagid, appid });
        }
    }
}
