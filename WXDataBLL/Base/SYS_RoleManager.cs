using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXDataDAL;
using WXDataDAL.SYS;
using WXDataModel;
using WXService.Utility;

namespace WXDataBLL.Base
{
    public class SYS_RoleManager : BaseBLL<SYS_Role>
    {

        public SYS_RoleManager() : base(new BaseDAL<SYS_Role>())
        {

        }
        public bool EditRight(SYS_Role info, List<int> funcList)
        {
            SYS_Role role = new SYS_Role();
            EntityUntility.CopyProperty(info, role);
             
            foreach (var id in funcList)
            {
                SYS_Function func = new SYS_Function();
                func.FunctionID = id;
                role.SYS_Function.Add(func);
            }

            return new SYS_RoleService().Update(role);
        }
    }
}
