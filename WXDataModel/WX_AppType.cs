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
    
    public partial class WX_AppType
    {
        public WX_AppType()
        {
            this.WX_App = new HashSet<WX_App>();
        }
    
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    
        public virtual ICollection<WX_App> WX_App { get; set; }
    }
}
