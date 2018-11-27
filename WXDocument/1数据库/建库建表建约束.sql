/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2018-11-27 14:07:25                          */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SYS_Department') and o.name = 'FK_SYS_DEPA_REFERENCE_SYS_DEPA')
alter table SYS_Department
   drop constraint FK_SYS_DEPA_REFERENCE_SYS_DEPA
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SYS_Department') and o.name = 'FK_SYS_DEPA_REFERENCE_WX_APP')
alter table SYS_Department
   drop constraint FK_SYS_DEPA_REFERENCE_WX_APP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SYS_Department') and o.name = 'FK_SYS_DEPA_REFERENCE_SYS_USER')
alter table SYS_Department
   drop constraint FK_SYS_DEPA_REFERENCE_SYS_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SYS_Function') and o.name = 'FK_SYS_FUNC_REFERENCE_SYS_FUNC')
alter table SYS_Function
   drop constraint FK_SYS_FUNC_REFERENCE_SYS_FUNC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SYS_Right') and o.name = 'FK_SYS_RIGH_REFERENCE_SYS_ROLE')
alter table SYS_Right
   drop constraint FK_SYS_RIGH_REFERENCE_SYS_ROLE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SYS_Right') and o.name = 'FK_SYS_RIGH_REFERENCE_SYS_FUNC')
alter table SYS_Right
   drop constraint FK_SYS_RIGH_REFERENCE_SYS_FUNC
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SYS_Role') and o.name = 'FK_SYS_ROLE_REFERENCE_WX_APP')
alter table SYS_Role
   drop constraint FK_SYS_ROLE_REFERENCE_WX_APP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SYS_Setting') and o.name = 'FK_SYS_SETT_REFERENCE_WX_APP')
alter table SYS_Setting
   drop constraint FK_SYS_SETT_REFERENCE_WX_APP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SYS_User') and o.name = 'FK_SYS_USER_REFERENCE_SYS_ROLE')
alter table SYS_User
   drop constraint FK_SYS_USER_REFERENCE_SYS_ROLE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('SYS_User') and o.name = 'FK_SYS_USER_REFERENCE_WX_APP')
alter table SYS_User
   drop constraint FK_SYS_USER_REFERENCE_WX_APP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WX_CustomMsg') and o.name = 'FK_WX_CUSTO_REFERENCE_WX_USER')
alter table WX_CustomMsg
   drop constraint FK_WX_CUSTO_REFERENCE_WX_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WX_CustomMsg') and o.name = 'FK_WX_CUSTO_REFERENCE_SYS_USER')
alter table WX_CustomMsg
   drop constraint FK_WX_CUSTO_REFERENCE_SYS_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WX_CustomMsg') and o.name = 'FK_WX_CUSTO_REFERENCE_WX_APP')
alter table WX_CustomMsg
   drop constraint FK_WX_CUSTO_REFERENCE_WX_APP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WX_Media') and o.name = 'FK_WX_MEDIA_REFERENCE_WX_APP')
alter table WX_Media
   drop constraint FK_WX_MEDIA_REFERENCE_WX_APP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WX_Menu') and o.name = 'FK_WX_MENU_REFERENCE_WX_APP')
alter table WX_Menu
   drop constraint FK_WX_MENU_REFERENCE_WX_APP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WX_Menu') and o.name = 'FK_WX_MENU_REFERENCE_WX_MENU')
alter table WX_Menu
   drop constraint FK_WX_MENU_REFERENCE_WX_MENU
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WX_Queue') and o.name = 'FK_WX_QUEUE_REFERENCE_WX_USER')
alter table WX_Queue
   drop constraint FK_WX_QUEUE_REFERENCE_WX_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WX_Queue') and o.name = 'FK_WX_QUEUE_REFERENCE_WX_APP')
alter table WX_Queue
   drop constraint FK_WX_QUEUE_REFERENCE_WX_APP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WX_User') and o.name = 'FK_WX_USER_REFERENCE_WX_APP')
alter table WX_User
   drop constraint FK_WX_USER_REFERENCE_WX_APP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WX_User') and o.name = 'FK_WX_USER_REFERENCE_SYS_USER')
alter table WX_User
   drop constraint FK_WX_USER_REFERENCE_SYS_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WX_User') and o.name = 'FK_WX_USER_REFERENCE_WX_USERG')
alter table WX_User
   drop constraint FK_WX_USER_REFERENCE_WX_USERG
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WX_User') and o.name = 'FK_WX_USER_REFERENCE_WX_USERT')
alter table WX_User
   drop constraint FK_WX_USER_REFERENCE_WX_USERT
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WX_UserGroup') and o.name = 'FK_WX_USERG_REFERENCE_WX_APP')
alter table WX_UserGroup
   drop constraint FK_WX_USERG_REFERENCE_WX_APP
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('WX_UserTag') and o.name = 'FK_WX_USERT_REFERENCE_WX_APP')
alter table WX_UserTag
   drop constraint FK_WX_USERT_REFERENCE_WX_APP
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SYS_Department')
            and   type = 'U')
   drop table SYS_Department
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SYS_Function')
            and   type = 'U')
   drop table SYS_Function
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SYS_Log')
            and   type = 'U')
   drop table SYS_Log
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SYS_Right')
            and   type = 'U')
   drop table SYS_Right
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SYS_Role')
            and   type = 'U')
   drop table SYS_Role
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SYS_Setting')
            and   type = 'U')
   drop table SYS_Setting
go

if exists (select 1
            from  sysobjects
           where  id = object_id('SYS_User')
            and   type = 'U')
   drop table SYS_User
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WX_App')
            and   type = 'U')
   drop table WX_App
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WX_CustomMsg')
            and   type = 'U')
   drop table WX_CustomMsg
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WX_EventQueue')
            and   type = 'U')
   drop table WX_EventQueue
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WX_Media')
            and   type = 'U')
   drop table WX_Media
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WX_Menu')
            and   type = 'U')
   drop table WX_Menu
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WX_Queue')
            and   type = 'U')
   drop table WX_Queue
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WX_User')
            and   type = 'U')
   drop table WX_User
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WX_UserGroup')
            and   type = 'U')
   drop table WX_UserGroup
go

if exists (select 1
            from  sysobjects
           where  id = object_id('WX_UserTag')
            and   type = 'U')
   drop table WX_UserTag
go

/*==============================================================*/
/* Table: SYS_Department                                        */
/*==============================================================*/
create table SYS_Department (
   DepartmentId         int                  identity,
   DepartmentName       nvarchar(50)         null,
   ParentId             int                  null,
   AppId                varchar(50)          null,
   UserId               int                  null,
   constraint PK_SYS_DEPARTMENT primary key (DepartmentId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '部门',
   'user', @CurrentUser, 'table', 'SYS_Department'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '部门编号',
   'user', @CurrentUser, 'table', 'SYS_Department', 'column', 'DepartmentId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '部门名称',
   'user', @CurrentUser, 'table', 'SYS_Department', 'column', 'DepartmentName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '父部门',
   'user', @CurrentUser, 'table', 'SYS_Department', 'column', 'ParentId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号ID',
   'user', @CurrentUser, 'table', 'SYS_Department', 'column', 'AppId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '系统用户编号',
   'user', @CurrentUser, 'table', 'SYS_Department', 'column', 'UserId'
go

/*==============================================================*/
/* Table: SYS_Function                                          */
/*==============================================================*/
create table SYS_Function (
   FunctionID           int                  identity,
   ParentID             int                  null,
   FunctionName         nvarchar(50)         null,
   ImageUrl             nvarchar(255)        null,
   FunctionUrl          nvarchar(255)        null,
   FunctionType         nvarchar(50)         null,
   Remark               nvarchar(255)        null,
   constraint PK_SYS_FUNCTION primary key (FunctionID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '功能表',
   'user', @CurrentUser, 'table', 'SYS_Function'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '功能ID',
   'user', @CurrentUser, 'table', 'SYS_Function', 'column', 'FunctionID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '父功能ID',
   'user', @CurrentUser, 'table', 'SYS_Function', 'column', 'ParentID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '功能名称',
   'user', @CurrentUser, 'table', 'SYS_Function', 'column', 'FunctionName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '图片URL',
   'user', @CurrentUser, 'table', 'SYS_Function', 'column', 'ImageUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   'URL',
   'user', @CurrentUser, 'table', 'SYS_Function', 'column', 'FunctionUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '功能类型(系统，模块，页面，按钮)',
   'user', @CurrentUser, 'table', 'SYS_Function', 'column', 'FunctionType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'SYS_Function', 'column', 'Remark'
go

/*==============================================================*/
/* Table: SYS_Log                                               */
/*==============================================================*/
create table SYS_Log (
   LogId                int                  not null,
   constraint PK_SYS_LOG primary key (LogId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '系统日志',
   'user', @CurrentUser, 'table', 'SYS_Log'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '日志编号',
   'user', @CurrentUser, 'table', 'SYS_Log', 'column', 'LogId'
go

/*==============================================================*/
/* Table: SYS_Right                                             */
/*==============================================================*/
create table SYS_Right (
   RoleId               int                  not null,
   FunctionID           int                  not null,
   constraint PK_SYS_RIGHT primary key (RoleId, FunctionID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '权限表',
   'user', @CurrentUser, 'table', 'SYS_Right'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色编号',
   'user', @CurrentUser, 'table', 'SYS_Right', 'column', 'RoleId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '功能ID',
   'user', @CurrentUser, 'table', 'SYS_Right', 'column', 'FunctionID'
go

/*==============================================================*/
/* Table: SYS_Role                                              */
/*==============================================================*/
create table SYS_Role (
   RoleId               int                  identity,
   AppId                varchar(50)          null,
   RoleSign             varchar(50)          null,
   RoleName             nvarchar(50)         null,
   constraint PK_SYS_ROLE primary key (RoleId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户角色',
   'user', @CurrentUser, 'table', 'SYS_Role'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色编号',
   'user', @CurrentUser, 'table', 'SYS_Role', 'column', 'RoleId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号ID',
   'user', @CurrentUser, 'table', 'SYS_Role', 'column', 'AppId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色标识',
   'user', @CurrentUser, 'table', 'SYS_Role', 'column', 'RoleSign'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色名称',
   'user', @CurrentUser, 'table', 'SYS_Role', 'column', 'RoleName'
go

/*==============================================================*/
/* Table: SYS_Setting                                           */
/*==============================================================*/
create table SYS_Setting (
   SettingId            int                  not null,
   AppId                varchar(50)          null,
   SettingKey           nvarchar(50)         null,
   SettingValue         nvarchar(50)         null,
   constraint PK_SYS_SETTING primary key (SettingId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '系统设置表',
   'user', @CurrentUser, 'table', 'SYS_Setting'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设置编号',
   'user', @CurrentUser, 'table', 'SYS_Setting', 'column', 'SettingId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号ID',
   'user', @CurrentUser, 'table', 'SYS_Setting', 'column', 'AppId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设置key',
   'user', @CurrentUser, 'table', 'SYS_Setting', 'column', 'SettingKey'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '设置Value',
   'user', @CurrentUser, 'table', 'SYS_Setting', 'column', 'SettingValue'
go

/*==============================================================*/
/* Table: SYS_User                                              */
/*==============================================================*/
create table SYS_User (
   UserId               int                  identity,
   AppId                varchar(50)          null,
   RoleId               int                  null,
   LoginId              varchar(20)          null,
   Password             varchar(50)          null,
   UserName             nvarchar(20)         null,
   Telphone             varchar(15)          null,
   Email                nvarchar(20)         null,
   WXId                 varchar(50)          null,
   UserState            nvarchar(10)         null,
   HeadImageUrl         VARCHAR(max)         null,
   UserSex              nchar(1)             null,
   constraint PK_SYS_USER primary key (UserId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '系统用户',
   'user', @CurrentUser, 'table', 'SYS_User'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '系统用户编号',
   'user', @CurrentUser, 'table', 'SYS_User', 'column', 'UserId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号ID',
   'user', @CurrentUser, 'table', 'SYS_User', 'column', 'AppId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '角色编号',
   'user', @CurrentUser, 'table', 'SYS_User', 'column', 'RoleId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '账号',
   'user', @CurrentUser, 'table', 'SYS_User', 'column', 'LoginId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '密码',
   'user', @CurrentUser, 'table', 'SYS_User', 'column', 'Password'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '姓名',
   'user', @CurrentUser, 'table', 'SYS_User', 'column', 'UserName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电话',
   'user', @CurrentUser, 'table', 'SYS_User', 'column', 'Telphone'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '邮箱',
   'user', @CurrentUser, 'table', 'SYS_User', 'column', 'Email'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '微信号',
   'user', @CurrentUser, 'table', 'SYS_User', 'column', 'WXId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户状态',
   'user', @CurrentUser, 'table', 'SYS_User', 'column', 'UserState'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户头像',
   'user', @CurrentUser, 'table', 'SYS_User', 'column', 'HeadImageUrl'
go

/*==============================================================*/
/* Table: WX_App                                                */
/*==============================================================*/
create table WX_App (
   AppId                varchar(50)          not null,
   AppName              nvarchar(50)         null,
   AppSecret            varchar(50)          null,
   WXId                 varchar(50)          null,
   AppType              nvarchar(10)         null,
   AppState             nvarchar(10)         null,
   CompanyName          nvarchar(50)         null,
   Remark               nvarchar(255)        null,
   constraint PK_WX_APP primary key (AppId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号',
   'user', @CurrentUser, 'table', 'WX_App'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号ID',
   'user', @CurrentUser, 'table', 'WX_App', 'column', 'AppId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号名称',
   'user', @CurrentUser, 'table', 'WX_App', 'column', 'AppName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号密码',
   'user', @CurrentUser, 'table', 'WX_App', 'column', 'AppSecret'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '微信号',
   'user', @CurrentUser, 'table', 'WX_App', 'column', 'WXId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号类型',
   'user', @CurrentUser, 'table', 'WX_App', 'column', 'AppType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号状态',
   'user', @CurrentUser, 'table', 'WX_App', 'column', 'AppState'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公司名称',
   'user', @CurrentUser, 'table', 'WX_App', 'column', 'CompanyName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'WX_App', 'column', 'Remark'
go

/*==============================================================*/
/* Table: WX_CustomMsg                                          */
/*==============================================================*/
create table WX_CustomMsg (
   MsgId                varchar(50)          not null,
   OpenID               VARCHAR(50)          null,
   UserId               int                  null,
   AppId                varchar(50)          null,
   CreateTime           datetime             null,
   Content              nvarchar(max)        null,
   MsgSource            nvarchar(5)          null,
   MediaId              varchar(50)          null,
   MsgType              varchar(20)          null,
   XmlContent           nvarchar(max)        null,
   constraint PK_WX_CUSTOMMSG primary key (MsgId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '客服信息表',
   'user', @CurrentUser, 'table', 'WX_CustomMsg'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '消息ID',
   'user', @CurrentUser, 'table', 'WX_CustomMsg', 'column', 'MsgId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户标识',
   'user', @CurrentUser, 'table', 'WX_CustomMsg', 'column', 'OpenID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '系统用户编号',
   'user', @CurrentUser, 'table', 'WX_CustomMsg', 'column', 'UserId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号ID',
   'user', @CurrentUser, 'table', 'WX_CustomMsg', 'column', 'AppId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发送时间',
   'user', @CurrentUser, 'table', 'WX_CustomMsg', 'column', 'CreateTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '发送内容',
   'user', @CurrentUser, 'table', 'WX_CustomMsg', 'column', 'Content'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '消息类型(客服，粉丝)',
   'user', @CurrentUser, 'table', 'WX_CustomMsg', 'column', 'MsgSource'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '媒体ID',
   'user', @CurrentUser, 'table', 'WX_CustomMsg', 'column', 'MediaId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '消息类型',
   'user', @CurrentUser, 'table', 'WX_CustomMsg', 'column', 'MsgType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '消息内容',
   'user', @CurrentUser, 'table', 'WX_CustomMsg', 'column', 'XmlContent'
go

/*==============================================================*/
/* Table: WX_EventQueue                                         */
/*==============================================================*/
create table WX_EventQueue (
   EventId              int                  identity,
   OpenID               VARCHAR(50)          not null,
   AppId                varchar(50)          null,
   CreateTime           datetime             null,
   MsgType              varchar(50)          null,
   Event                varchar(50)          null,
   XmlContent           nvarchar(max)        null,
   MsgState             int                  null,
   constraint PK_WX_EVENTQUEUE primary key (EventId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户标识',
   'user', @CurrentUser, 'table', 'WX_EventQueue', 'column', 'OpenID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号ID',
   'user', @CurrentUser, 'table', 'WX_EventQueue', 'column', 'AppId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '消息内容',
   'user', @CurrentUser, 'table', 'WX_EventQueue', 'column', 'XmlContent'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '消息状态',
   'user', @CurrentUser, 'table', 'WX_EventQueue', 'column', 'MsgState'
go

/*==============================================================*/
/* Table: WX_Media                                              */
/*==============================================================*/
create table WX_Media (
   MyMediaId            int                  identity,
   AppId                varchar(50)          null,
   MediaId              varchar(50)          null,
   MediaName            varchar(50)          null,
   MediaType            varchar(50)          null,
   MediaContent         nvarchar(max)        null,
   MediaState           int                  null,
   UploadTime           datetime             null,
   constraint PK_WX_MEDIA primary key (MyMediaId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '素材',
   'user', @CurrentUser, 'table', 'WX_Media'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '素材ID',
   'user', @CurrentUser, 'table', 'WX_Media', 'column', 'MyMediaId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号ID',
   'user', @CurrentUser, 'table', 'WX_Media', 'column', 'AppId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '微信素材ID',
   'user', @CurrentUser, 'table', 'WX_Media', 'column', 'MediaId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '素材名称',
   'user', @CurrentUser, 'table', 'WX_Media', 'column', 'MediaName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '媒体类型',
   'user', @CurrentUser, 'table', 'WX_Media', 'column', 'MediaType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '媒体内容',
   'user', @CurrentUser, 'table', 'WX_Media', 'column', 'MediaContent'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '状态',
   'user', @CurrentUser, 'table', 'WX_Media', 'column', 'MediaState'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '上传时间',
   'user', @CurrentUser, 'table', 'WX_Media', 'column', 'UploadTime'
go

/*==============================================================*/
/* Table: WX_Menu                                               */
/*==============================================================*/
create table WX_Menu (
   MenuId               int                  identity,
   ParentMenuId         int                  null,
   AppId                varchar(50)          null,
   MenuName             nvarchar(50)         null,
   MenuType             int                  null,
   MenuKey              int                  null,
   MenuUrl              nvarchar(255)        null,
   MenuVisable          int                  null,
   MenuSort             int                  null,
   constraint PK_WX_MENU primary key (MenuId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '微信菜单',
   'user', @CurrentUser, 'table', 'WX_Menu'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '菜单ID',
   'user', @CurrentUser, 'table', 'WX_Menu', 'column', 'MenuId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '父菜单ID',
   'user', @CurrentUser, 'table', 'WX_Menu', 'column', 'ParentMenuId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号ID',
   'user', @CurrentUser, 'table', 'WX_Menu', 'column', 'AppId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '菜单名称',
   'user', @CurrentUser, 'table', 'WX_Menu', 'column', 'MenuName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '菜单类型',
   'user', @CurrentUser, 'table', 'WX_Menu', 'column', 'MenuType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '菜单KEY',
   'user', @CurrentUser, 'table', 'WX_Menu', 'column', 'MenuKey'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '菜单URL',
   'user', @CurrentUser, 'table', 'WX_Menu', 'column', 'MenuUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '是否可见',
   'user', @CurrentUser, 'table', 'WX_Menu', 'column', 'MenuVisable'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排序号',
   'user', @CurrentUser, 'table', 'WX_Menu', 'column', 'MenuSort'
go

/*==============================================================*/
/* Table: WX_Queue                                              */
/*==============================================================*/
create table WX_Queue (
   MsgId                varchar(50)          not null,
   OpenID               varchar(50)          null,
   AppId                varchar(50)          null,
   MsgType              varchar(20)          null,
   XmlContent           nvarchar(max)        null,
   MsgState             int                  null,
   CreateTime           datetime             null,
   constraint PK_WX_QUEUE primary key (MsgId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '消息队列',
   'user', @CurrentUser, 'table', 'WX_Queue'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '消息编号',
   'user', @CurrentUser, 'table', 'WX_Queue', 'column', 'MsgId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户标识',
   'user', @CurrentUser, 'table', 'WX_Queue', 'column', 'OpenID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号ID',
   'user', @CurrentUser, 'table', 'WX_Queue', 'column', 'AppId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '消息类型',
   'user', @CurrentUser, 'table', 'WX_Queue', 'column', 'MsgType'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '消息内容',
   'user', @CurrentUser, 'table', 'WX_Queue', 'column', 'XmlContent'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '消息状态',
   'user', @CurrentUser, 'table', 'WX_Queue', 'column', 'MsgState'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '创建时间',
   'user', @CurrentUser, 'table', 'WX_Queue', 'column', 'CreateTime'
go

/*==============================================================*/
/* Table: WX_User                                               */
/*==============================================================*/
create table WX_User (
   OpenID               VARCHAR(50)          not null,
   TageId               int                  null,
   GrooupId             int                  null,
   AppId                varchar(50)          null,
   UserId               int                  null,
   UserNick             VARCHAR(50)          null,
   UserName             VARCHAR(50)          null,
   HeadImageUrl         VARCHAR(max)         null,
   UserSex              nchar(1)             null,
   Language             nVARCHAR(50)         null,
   City                 nVARCHAR(50)         null,
   Province             nVARCHAR(50)         null,
   Country              nVARCHAR(50)         null,
   SubscribeTime        datetime             null,
   UnSubscribeTime      datetime             null,
   Telphone             VARCHAR(15)          null,
   Emial                VARCHAR(50)          null,
   Address              VARCHAR(255)         null,
   ZipCode              char(6)              null,
   Remark               VARCHAR(255)         null,
   constraint PK_WX_USER primary key (OpenID)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订阅用户表',
   'user', @CurrentUser, 'table', 'WX_User'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户标识',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'OpenID'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户标签ID',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'TageId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户组编号',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'GrooupId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号ID',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'AppId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '系统用户编号',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'UserId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户昵称',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'UserNick'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户名称',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'UserName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户头像',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'HeadImageUrl'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '性别',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'UserSex'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户语言',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'Language'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '所在城市',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'City'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '所在省份',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'Province'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '所在国家',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'Country'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '关注时间',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'SubscribeTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '退订时间',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'UnSubscribeTime'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '电话',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'Telphone'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '邮箱',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'Emial'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '地址',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'Address'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '邮编',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'ZipCode'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '备注',
   'user', @CurrentUser, 'table', 'WX_User', 'column', 'Remark'
go

/*==============================================================*/
/* Table: WX_UserGroup                                          */
/*==============================================================*/
create table WX_UserGroup (
   GroupId              int                  not null,
   AppId                varchar(50)          null,
   GroupName            nvarchar(50)         null,
   GroupSort            int                  null,
   constraint PK_WX_USERGROUP primary key (GroupId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订阅用户组',
   'user', @CurrentUser, 'table', 'WX_UserGroup'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户组编号',
   'user', @CurrentUser, 'table', 'WX_UserGroup', 'column', 'GroupId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号ID',
   'user', @CurrentUser, 'table', 'WX_UserGroup', 'column', 'AppId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户组名称',
   'user', @CurrentUser, 'table', 'WX_UserGroup', 'column', 'GroupName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '排序号',
   'user', @CurrentUser, 'table', 'WX_UserGroup', 'column', 'GroupSort'
go

/*==============================================================*/
/* Table: WX_UserTag                                            */
/*==============================================================*/
create table WX_UserTag (
   TageId               int                  identity,
   AppId                varchar(50)          null,
   TagName              nvarchar(50)         null,
   UserCount            int                  null,
   constraint PK_WX_USERTAG primary key (TageId)
)
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '订阅用户标签',
   'user', @CurrentUser, 'table', 'WX_UserTag'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户标签ID',
   'user', @CurrentUser, 'table', 'WX_UserTag', 'column', 'TageId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '公众号ID',
   'user', @CurrentUser, 'table', 'WX_UserTag', 'column', 'AppId'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户标签名称',
   'user', @CurrentUser, 'table', 'WX_UserTag', 'column', 'TagName'
go

declare @CurrentUser sysname
select @CurrentUser = user_name()
execute sp_addextendedproperty 'MS_Description', 
   '用户数',
   'user', @CurrentUser, 'table', 'WX_UserTag', 'column', 'UserCount'
go

alter table SYS_Department
   add constraint FK_SYS_DEPA_REFERENCE_SYS_DEPA foreign key (ParentId)
      references SYS_Department (DepartmentId)
go

alter table SYS_Department
   add constraint FK_SYS_DEPA_REFERENCE_WX_APP foreign key (AppId)
      references WX_App (AppId)
go

alter table SYS_Department
   add constraint FK_SYS_DEPA_REFERENCE_SYS_USER foreign key (UserId)
      references SYS_User (UserId)
go

alter table SYS_Function
   add constraint FK_SYS_FUNC_REFERENCE_SYS_FUNC foreign key (ParentID)
      references SYS_Function (FunctionID)
go

alter table SYS_Right
   add constraint FK_SYS_RIGH_REFERENCE_SYS_ROLE foreign key (RoleId)
      references SYS_Role (RoleId)
go

alter table SYS_Right
   add constraint FK_SYS_RIGH_REFERENCE_SYS_FUNC foreign key (FunctionID)
      references SYS_Function (FunctionID)
go

alter table SYS_Role
   add constraint FK_SYS_ROLE_REFERENCE_WX_APP foreign key (AppId)
      references WX_App (AppId)
go

alter table SYS_Setting
   add constraint FK_SYS_SETT_REFERENCE_WX_APP foreign key (AppId)
      references WX_App (AppId)
go

alter table SYS_User
   add constraint FK_SYS_USER_REFERENCE_SYS_ROLE foreign key (RoleId)
      references SYS_Role (RoleId)
go

alter table SYS_User
   add constraint FK_SYS_USER_REFERENCE_WX_APP foreign key (AppId)
      references WX_App (AppId)
go

alter table WX_CustomMsg
   add constraint FK_WX_CUSTO_REFERENCE_WX_USER foreign key (OpenID)
      references WX_User (OpenID)
go

alter table WX_CustomMsg
   add constraint FK_WX_CUSTO_REFERENCE_SYS_USER foreign key (UserId)
      references SYS_User (UserId)
go

alter table WX_CustomMsg
   add constraint FK_WX_CUSTO_REFERENCE_WX_APP foreign key (AppId)
      references WX_App (AppId)
go

alter table WX_Media
   add constraint FK_WX_MEDIA_REFERENCE_WX_APP foreign key (AppId)
      references WX_App (AppId)
go

alter table WX_Menu
   add constraint FK_WX_MENU_REFERENCE_WX_APP foreign key (AppId)
      references WX_App (AppId)
go

alter table WX_Menu
   add constraint FK_WX_MENU_REFERENCE_WX_MENU foreign key (ParentMenuId)
      references WX_Menu (MenuId)
go

alter table WX_Queue
   add constraint FK_WX_QUEUE_REFERENCE_WX_USER foreign key (OpenID)
      references WX_User (OpenID)
go

alter table WX_Queue
   add constraint FK_WX_QUEUE_REFERENCE_WX_APP foreign key (AppId)
      references WX_App (AppId)
go

alter table WX_User
   add constraint FK_WX_USER_REFERENCE_WX_APP foreign key (AppId)
      references WX_App (AppId)
go

alter table WX_User
   add constraint FK_WX_USER_REFERENCE_SYS_USER foreign key (UserId)
      references SYS_User (UserId)
go

alter table WX_User
   add constraint FK_WX_USER_REFERENCE_WX_USERG foreign key (GrooupId)
      references WX_UserGroup (GroupId)
go

alter table WX_User
   add constraint FK_WX_USER_REFERENCE_WX_USERT foreign key (TageId)
      references WX_UserTag (TageId)
go

alter table WX_UserGroup
   add constraint FK_WX_USERG_REFERENCE_WX_APP foreign key (AppId)
      references WX_App (AppId)
go

alter table WX_UserTag
   add constraint FK_WX_USERT_REFERENCE_WX_APP foreign key (AppId)
      references WX_App (AppId)
go

