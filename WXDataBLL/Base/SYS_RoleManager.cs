using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXDataModel;
using WXService.Utility;

namespace WXDataBLL.Base
{
    public class SYS_RoleManager : BaseBLL<SYS_Role>
    {
        public bool EditRight(int roleId, List<int> funcList)
        {
            SYS_Role role = GetByPK(roleId);
            SYS_Role role1 = new SYS_Role();
            EntityUntility.CopyProperty(role, role1);
            role1.SYS_Function.Clear();
            foreach (var id in funcList)
            {
                SYS_Function func = new SYS_FunctionManager().GetByPK(id);
                role1.SYS_Function.Add(func);
            }

            return Update(role1);
        }
    }
}
