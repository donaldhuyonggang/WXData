 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WXDataModel;

namespace WXDataDAL.SYS
{
    public class SYS_RoleService : BaseDAL<SYS_Role>
    {
        public override bool Update(SYS_Role info)
        {
            using (WXDataEntities db = new WXDataEntities())
            {
                db.Database.ExecuteSqlCommand("delete SYS_Right where RoleId="+info.RoleId);

                var info1 = db.SYS_Role.Find(info.RoleId);
                info1.RoleId = info.RoleId;
                info1.RoleName = info.RoleName;
                info1.SYS_Function = info.SYS_Function;

                return db.SaveChanges() > 0;
            }
                
        }
    }
}
