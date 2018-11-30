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
    
    public partial class SYS_User
    {
        public SYS_User()
        {
            this.SYS_Department = new HashSet<SYS_Department>();
            this.WX_CustomMsg = new HashSet<WX_CustomMsg>();
            this.WX_QR = new HashSet<WX_QR>();
            this.WX_User = new HashSet<WX_User>();
            this.WX_UserGroup = new HashSet<WX_UserGroup>();
        }
    
        public int UserId { get; set; }
        public string AppId { get; set; }
        public Nullable<int> RoleId { get; set; }
        public string LoginId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Telphone { get; set; }
        public string Email { get; set; }
        public string WXId { get; set; }
        public string UserState { get; set; }
        public string HeadImageUrl { get; set; }
        public string UserSex { get; set; }
    
        public virtual ICollection<SYS_Department> SYS_Department { get; set; }
        public virtual SYS_Role SYS_Role { get; set; }
        public virtual WX_App WX_App { get; set; }
        public virtual ICollection<WX_CustomMsg> WX_CustomMsg { get; set; }
        public virtual ICollection<WX_QR> WX_QR { get; set; }
        public virtual ICollection<WX_User> WX_User { get; set; }
        public virtual ICollection<WX_UserGroup> WX_UserGroup { get; set; }
    }
}
