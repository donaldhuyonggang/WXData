﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class WXDataEntities : DbContext
    {
        public WXDataEntities()
            : base("name=WXDataEntities")
        {
        }
    
        public DbSet<SYS_Department> SYS_Department { get; set; }
        public DbSet<SYS_Function> SYS_Function { get; set; }
        public DbSet<SYS_Log> SYS_Log { get; set; }
        public DbSet<SYS_Role> SYS_Role { get; set; }
        public DbSet<SYS_Setting> SYS_Setting { get; set; }
        public DbSet<SYS_User> SYS_User { get; set; }
        public DbSet<WX_App> WX_App { get; set; }
        public DbSet<WX_CustomMsg> WX_CustomMsg { get; set; }
        public DbSet<WX_EventQueue> WX_EventQueue { get; set; }
        public DbSet<WX_Media> WX_Media { get; set; }
        public DbSet<WX_Menu> WX_Menu { get; set; }
        public DbSet<WX_QR> WX_QR { get; set; }
        public DbSet<WX_Queue> WX_Queue { get; set; }
        public DbSet<WX_User> WX_User { get; set; }
        public DbSet<WX_UserGroup> WX_UserGroup { get; set; }
        public DbSet<WX_UserTag> WX_UserTag { get; set; }
    }
}
