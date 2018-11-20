
create database WXData
go

/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2018-11-20 17:18:16                          */
/*==============================================================*/


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
   where r.fkeyid = object_id('WX_User') and o.name = 'FK_WX_USER_REFERENCE_WX_APP')
alter table WX_User
   drop constraint FK_WX_USER_REFERENCE_WX_APP
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
           where  id = object_id('SYS_Log')
            and   type = 'U')
   drop table SYS_Log
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
           where  id = object_id('WX_Message')
            and   type = 'U')
   drop table WX_Message
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
/* Table: SYS_Log                                               */
/*==============================================================*/
create table SYS_Log (
   LogId                int                  not null,
   constraint PK_SYS_LOG primary key (LogId)
)
go

/*==============================================================*/
/* Table: SYS_Role                                              */
/*==============================================================*/
create table SYS_Role (
   RoleId               int                  identity,
   RoleSign             varchar(50)          null,
   RoleName             nvarchar(50)         null,
   constraint PK_SYS_ROLE primary key (RoleId)
)
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
   constraint PK_SYS_USER primary key (UserId)
)
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
   Remark               nvarchar(255)        null,
   constraint PK_WX_APP primary key (AppId)
)
go

/*==============================================================*/
/* Table: WX_Media                                              */
/*==============================================================*/
create table WX_Media (
   MyMediaId            int                  identity,
   AccountId            varchar(20)          null,
   MediaId              varchar(50)          null,
   MediaName            varchar(50)          null,
   MediaType            varchar(50)          null,
   MediaContent         nvarchar(max)        null,
   MediaState           int                  null,
   UploadTime           datetime             null,
   constraint PK_WX_MEDIA primary key (MyMediaId)
)
go

/*==============================================================*/
/* Table: WX_Menu                                               */
/*==============================================================*/
create table WX_Menu (
   MenuId               int                  identity,
   AccountId            varchar(20)          null,
   ParentMenuId         int                  null,
   MenuName             nvarchar(50)         null,
   MenuType             int                  null,
   MenuKey              int                  null,
   MenuUrl              nvarchar(255)        null,
   MenuVisable          int                  null,
   MenuSort             int                  null,
   constraint PK_WX_MENU primary key (MenuId)
)
go

/*==============================================================*/
/* Table: WX_Message                                            */
/*==============================================================*/
create table WX_Message (
   Msg_Id               varchar(50)          not null,
   XmlContent           nvarchar(50)         null,
   ToUserName           varchar(50)          null,
   FromUserName         varchar(50)          null,
   CreateTime           datetime             null,
   Event                varchar(20)          null,
   Content              nvarchar(255)        null,
   MediaId              varchar(50)          null,
   MsgType              varchar(20)          null,
   constraint PK_WX_MESSAGE primary key (Msg_Id)
)
go

/*==============================================================*/
/* Table: WX_Queue                                              */
/*==============================================================*/
create table WX_Queue (
   MsgId                varchar(50)          not null,
   OpenID               varchar(50)          null,
   MsgType              varchar(20)          null,
   XmlContent           nvarchar(max)        null,
   MsgState             int                  null,
   CreateTime           datetime             null,
   constraint PK_WX_QUEUE primary key (MsgId)
)
go

/*==============================================================*/
/* Table: WX_User                                               */
/*==============================================================*/
create table WX_User (
   OpenID               VARCHAR(50)          not null,
   TageId               int                  null,
   GrooupId             int                  null,
   AppId                varchar(50)          null,
   UserNick             VARCHAR(50)          null,
   UserName             VARCHAR(50)          null,
   HeadImageUrl         VARCHAR(200)         null,
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

alter table WX_Media
   add constraint FK_WX_MEDIA_REFERENCE_WX_APP foreign key (AccountId)
      references WX_App (AppId)
go

alter table WX_Menu
   add constraint FK_WX_MENU_REFERENCE_WX_APP foreign key (AccountId)
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

alter table WX_User
   add constraint FK_WX_USER_REFERENCE_WX_APP foreign key (AppId)
      references WX_App (AppId)
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

