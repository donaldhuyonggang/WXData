//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace WXDataModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class WX_Menu
    {
        public WX_Menu()
        {
            this.WX_Menu1 = new HashSet<WX_Menu>();
        }
    
        public int MenuId { get; set; }
        public Nullable<int> ParentMenuId { get; set; }
        public string AppId { get; set; }
        public Nullable<int> TypeId { get; set; }
        public string MenuName { get; set; }
        public string MenuKey { get; set; }
        public string MenuUrl { get; set; }
        public Nullable<int> MenuVisable { get; set; }
        public Nullable<int> MenuSort { get; set; }
    
        public virtual WX_App WX_App { get; set; }
        public virtual ICollection<WX_Menu> WX_Menu1 { get; set; }
        public virtual WX_Menu WX_Menu2 { get; set; }
        public virtual WX_MenuType WX_MenuType { get; set; }
    }
}
