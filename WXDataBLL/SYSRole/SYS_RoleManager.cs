using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXDataDAL;
using WXDataModel;

namespace WXDataBLL.SYSRole
{
    public class SYS_RoleManager:BaseBLL<SYS_Role>
    { 
        public SYS_RoleManager() : base(new BaseDAL<SYS_Role>())
        {

        }
    }
}
