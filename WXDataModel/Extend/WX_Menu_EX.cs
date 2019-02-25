using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WXDataModel.Extend
{
    public static class WX_Menu_EX
    {
        private static JObject GetJson(this WX_Menu menu, JArray info)
        {
            JObject staff = new JObject();
            if(menu.WX_Menu1.Count() > 0)   //返回子菜单合集
            {
                JArray sub_button = new JArray();
                foreach (var i in menu.WX_Menu1)
                {
                    JObject temp = new JObject();
                    if (i.TypeId == 1)
                    {
                        temp.Add(new JProperty("type", i.WX_MenuType.TypeName));
                        temp.Add(new JProperty("name", i.MenuName));
                        temp.Add(new JProperty("key", i.MenuKey));
                    }
                    else
                    {
                        temp.Add(new JProperty("type", i.WX_MenuType.TypeName));
                        temp.Add(new JProperty("name", i.MenuName));
                        temp.Add(new JProperty("url", i.MenuUrl));
                    }
                    sub_button.Add(temp);
                }
                staff.Add(new JProperty("name", menu.MenuName));
                staff.Add(new JProperty("sub_button", sub_button));
            }
            else
            {
                if(menu.TypeId == 1)
                {
                    staff.Add(new JProperty("type", menu.WX_MenuType.TypeName));
                    staff.Add(new JProperty("name", menu.MenuName));
                    staff.Add(new JProperty("key", menu.MenuKey));
                }
                else
                {
                    staff.Add(new JProperty("type", menu.WX_MenuType.TypeName));
                    staff.Add(new JProperty("name", menu.MenuName));
                    staff.Add(new JProperty("url", menu.MenuUrl));
                }
                
            }
            info.Add(staff);
            return staff;
        }


        public static JObject GetJson(this List<WX_Menu> menu)
        {
            JArray staff = new JArray();
            if (menu.Count() > 0)   //返回子菜单合集
            {
                for(var i = 0; i < menu.Count(); i++)
                {
                    GetJson(menu[i], staff);
                }
            }
            
            return new JObject(new JProperty("button", staff));
        }


    }
}
