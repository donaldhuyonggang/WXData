using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WXDataBLL;
using WXDataBLL.Base;
using WXDataModel;
using WXDataUI.Filter;
using WXDataUI.Models;

namespace WXDataUI.Areas.Base.Controllers
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [RightFilter(Message = "角色管理")]
    public class RoleController : Controller
    {
        // GET: Base/Role
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetRole()
        {

            SYS_User user = (Session["SYSUSER"] as SYS_User);
            var list = new SYS_RoleManager().Where(s => (s.AppId == user.WX_App.AppId) || (string.IsNullOrEmpty(s.AppId)));
            var json = list.Select(s => new
            {
                s.RoleId,
                s.RoleSign,
                s.RoleName,
                Type = (s.AppId == null ? "公共" : s.WX_App.AppName)
            });
            
            
            return Json(json, JsonRequestBehavior.AllowGet);
        }




        [HttpGet]
        public ActionResult AddRole()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddRole(SYS_Role role)
        {
            if (role.AppId.Equals("-1"))
            {
                role.AppId = null;
            }
            return Json(new SYS_RoleManager().Add(role),JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult UpdateRole(int id)
        {
            ViewBag.role = new SYS_RoleManager().GetByPK(id);
            return PartialView();
        }

        [HttpPost]
        public ActionResult UpdateRole(SYS_Role role)
        {
            if (role.AppId.Equals("-1"))
            {
                role.AppId = null;
            }
            return Json(new SYS_RoleManager().Update(role), JsonRequestBehavior.AllowGet);
        }

        
        [HttpPost]
        public ActionResult DeleteRole(int id)
        {
            SYS_RoleManager rm = new SYS_RoleManager();
            SYS_Role role = rm.GetByPK(id);
            int result = 0;

            if (role.SYS_User.Count > 0)
            {
                result = -1;//该角色下还有管理员
            }else if (role.SYS_Function.Count > 0) {
                if(!rm.ClearRight(role.RoleId))
                {
                    result = -2;    //清除权限时错误
                }
            }

            if(result == 0){
                if (rm.Delete(role.RoleId))
                {
                    result = 1;
                }
            }
            return Json(result,JsonRequestBehavior.AllowGet);
        }








        [HttpGet]
        public ActionResult EditRight(int roleId)
        {
            ViewBag.roleId = roleId;
            return PartialView();
        }
        [HttpPost]
        public ActionResult EditRight(int roleId, string json)
        {
            List<int> list = new List<int>();
            var c = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(json);
            foreach (var item in c)
            {
                int id = item.id;
                list.Add(id);
            }
            var result = new SYS_RoleManager().EditRight(new SYS_RoleManager().GetByPK(roleId), list);
            Controller_EX.BindSession(Session);
            //JObject jo = JObject.Parse(json);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 获取树形菜单json
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult GetFunction(int roleId)
        {
            List<SYS_Function> FuncList = new SYS_FunctionManager().Where(x => x.ParentID == null);
            List<object> json = new List<object>();
            foreach (var item in FuncList)
            {
                var info = new
                {
                    id = item.FunctionID,
                    text = item.FunctionName,
                    selectable = true, //标记节点是否可以选择。false表示节点应该作为扩展标题，不会触发选择事件。  string
                    //icon = "glyphicon glyphicon-play-circle", //节点上显示的图标，支持bootstrap的图标  string
                    //selectedIcon = "glyphicon glyphicon-ok "//节点被选中时显示的图标       string
                    //nodes = new List<object>()
                };



                //var list = new SYS_FunctionManager().Where(x => x.ParentID == item.FunctionID);

                json.Add(DG(item.FunctionID, info, new SYS_RoleManager().GetByPK(roleId)));
            }


            return Json(json, JsonRequestBehavior.AllowGet);
        }



        private dynamic DG(int functionId, dynamic info, SYS_Role role)
        {
            SYS_FunctionManager bll = new SYS_FunctionManager();
            var list = bll.Where(x => x.ParentID == functionId);
            if (list.Count > 0)
            {

                dynamic newInfo = new
                {
                    id = functionId,
                    info.text,
                    nodes = new List<object>(),
                    state = GetState(role, functionId)
                };

                foreach (var item in list)
                {
                    var sub = new
                    {
                        id = item.FunctionID,
                        text = item.FunctionName,
                        icon = "glyphicon glyphicon-unchecked", //节点上显示的图标，支持bootstrap的图标  string
                        selectedIcon = "glyphicon glyphicon-check ",//节点被选中时显示的图标       string
                        state = GetState(role, item.FunctionID)
                        //nodes = new List<object>()
                    };

                    newInfo.nodes.Add(DG(item.FunctionID, sub, role));

                }

                return newInfo;
            }
            return info;

        }


        private dynamic GetState(SYS_Role role, int funcId)
        {
            if (role.RoleSign.Equals("SYS_ADMIN") && new SYS_FunctionManager().GetByPK(funcId).ParentID != null)
            {
                return new
                { //描述节点的初始状态    Object
                  /*disabled: true,*/ //是否禁用节点
                    expanded = false, //是否展开节点
                    selected = true //是否选中节点
                };
            }
            if (role.SYS_Function.Where(s => s.FunctionID == funcId).ToList().Count > 0)
            {
                return new
                { //描述节点的初始状态    Object
                  /*disabled: true,*/ //是否禁用节点
                    expanded = false, //是否展开节点
                    selected = true //是否选中节点
                };
            }
            return new
            { //描述节点的初始状态    Object
              /*disabled: true,*/ //是否禁用节点
                expanded = false, //是否展开节点
                selected = false //是否选中节点
            };

        }

    }
}