using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WXDataModel;

namespace WXDataDAL.WXMenu
{
    public class WXMenuService
    {
        /// <summary>
        /// 根据id逻辑删除菜单
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delect_id(int id)
        {
            WXDataEntities db = new WXDataEntities();
            var shi = db.WX_Menu.Find(id);
            shi.MenuVisable = 1;
            return db.SaveChanges() > 0;
        }

        /// <summary>
        /// 新建子级元素判断二级菜单是否超过五个
        /// </summary>
        /// <returns></returns>
        public bool Add_zi_pd(int id)
        {
            WXDataEntities db = new WXDataEntities();
            int count = (db.WX_Menu.Where(g => g.ParentMenuId == id && g.MenuVisable == 0).ToList()).Count;
            if (count < 5)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 新增一级菜单  判断当前公众号的一级菜单是否超过5个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Add_yiji_pd(string AppId)
        {
            WXDataEntities db = new WXDataEntities();
            int count = (db.WX_Menu.Where(g => g.ParentMenuId == null && g.AppId == AppId && g.MenuVisable == 0).ToList()).Count;
            if (count < 3)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
