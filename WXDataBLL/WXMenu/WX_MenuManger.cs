using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WXDataBLL.WXMenu
{
    /// <summary>
    /// 微信菜单管理-李政
    /// </summary>
    public class WX_MenuManger : BaseBLL<WXDataModel.WX_Menu>
    {
        /// <summary>
        /// 根据id逻辑删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delect_id(int id)
        {
            return new WXDataDAL.WXMenu.WXMenuService().Delect_id(id);
        }

        /// <summary>
        /// 新建子级元素判断二级菜单是否超过五个
        /// </summary>
        /// <returns></returns>
        public bool Add_zi_pd(int id) {
            return new WXDataDAL.WXMenu.WXMenuService().Add_zi_pd(id);
        }

        /// <summary>
        /// 新增一级菜单  判断当前公众号的一级菜单是否超过3个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Add_yiji_pd(string AppId) {
            return new WXDataDAL.WXMenu.WXMenuService().Add_yiji_pd(AppId);
        }
    }
}
