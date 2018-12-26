using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXDataModel.Extend
{
    public static class SYS_Role_EX
    {
        public static List<SYS_Function> GetMainFunction(this SYS_Role role)
        {
            List<SYS_Function> list = new List<SYS_Function>();
            if (role.RoleSign.Equals("SYS_ADMIN"))
            {
                list = new WXDataEntities().SYS_Function.Where(s => s.ParentID == null).ToList();
                return list;
            }
            foreach (var i in role.SYS_Function.Where(s=>s.ParentID != null))
            {
                if (!list.HasFunction(i.SYS_Function2) && i.SYS_Function2.ParentID == null)
                {
                    list.Add(i.SYS_Function2);
                }
            }
            return list;
        }
    }
}
