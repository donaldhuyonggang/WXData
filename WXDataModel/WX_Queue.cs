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
    
    public partial class WX_Queue
    {
        public string MsgId { get; set; }
        public string OpenID { get; set; }
        public string AppId { get; set; }
        public string MsgType { get; set; }
        public string XmlContent { get; set; }
        public Nullable<int> MsgState { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
    
        public virtual WX_App WX_App { get; set; }
        public virtual WX_User WX_User { get; set; }
    }
}
