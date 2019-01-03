using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXDataModel.Extend
{
    public static class SYS_Function_EX
    {
        public static List<SYS_Function> GetChildByRole(this SYS_Function func,int roleId)
        {
            using (WXDataEntities db = new WXDataEntities())
            {
                SYS_Role role = db.SYS_Role.Find(roleId);
                List<SYS_Function> list = new List<SYS_Function>();
                if (role.RoleSign.Equals("SYS_ADMIN"))
                {
                    list = func.SYS_Function1.ToList();
                }else
                {
                    list = func.SYS_Function1.Where(s => s.SYS_Role.Where(r => r.RoleId == roleId).Count()>0).ToList();
                }
                return list;
            }
        }

        public static bool HasFunction(this List<SYS_Function> funcList,SYS_Function func)
        {
            foreach (var i in funcList)
            {
                if (i.FunctionID.Equals(func.FunctionID))
                {
                    return true;
                }
            }
            return false;
        }
        
    }
}
