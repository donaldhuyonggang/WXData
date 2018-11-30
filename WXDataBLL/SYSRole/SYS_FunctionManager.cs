using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXDataBLL.SYSUser;
using WXDataModel;

namespace WXDataBLL.SYSRole
{
    public class SYS_FunctionManager:BaseBLL<SYS_Function>
    {
        public dynamic GetFunction(int userId)
        {
            SYS_Role role = new SYS_RoleManager().GetByPK(new SYS_UserManager().GetByPK(userId).RoleId);
            List<SYS_Function> funcList = role.SYS_Function.Where(f => f.ParentID == null).ToList();
            List<object> json = new List<object>();
            foreach (var item in funcList)
            {
                var list = new
                {
                    item.FunctionID,
                    item.FunctionName,
                    item.FunctionType,
                    item.FunctionUrl,
                    item.ImageUrl,
                    children = new List<dynamic>()
                };

                DG(item.FunctionID, list);

                json.Add(list);
            }
            return json;
        }

        private void DG(int functionId, dynamic info)
        {

            var list = new SYS_FunctionManager().Where(x => x.ParentID == functionId);

            foreach (var item in list)
            {
                var sub = new
                {
                    item.FunctionID,
                    item.FunctionName,
                    item.FunctionType,
                    item.FunctionUrl,
                    item.ImageUrl,
                    children = new List<dynamic>()
                };

                DG(item.FunctionID, sub);

                info.children.Add(sub);

            }

        }
    }


}
