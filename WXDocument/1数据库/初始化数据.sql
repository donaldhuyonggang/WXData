use WXData
go


--Start新增用户角色
insert into SYS_Role(RoleSign,RoleName)values('SYS_ADMIN','超级管理员')
insert into SYS_Role(RoleSign,RoleName)values('APP_ADMIN','平台管理员')
insert into SYS_Role(RoleSign,RoleName)values('APP_CUSTOM','平台客服')
--End新增用户角色

--Start新增公众号类型
insert into WX_AppType(TypeName)values('订阅号')
insert into WX_AppType(TypeName)values('公众号')
insert into WX_AppType(TypeName)values('企业号')
insert into WX_AppType(TypeName)values('小程序')
--End新增公众号类型

--Start新增公众号
insert into WX_App(AppId,AppName,AppSecret,WXId,TypeId,AppState,Remark)values('wxb51501fa9702675f','E妹时尚女装','a56e69ded9b5eab3579ce771f2f9a668','gh_f98920d67737','1','正常','')
insert into WX_App(AppId,AppName,AppSecret,WXId,TypeId,AppState,Remark)values('wx5e6fd9f5a08db03a','李添乐测试','8f3079b54c134f3e67a88778b4a24f27','gh_ac32a7de334e','1','正常','')
insert into WX_App(AppId,AppName,AppSecret,WXId,TypeId,AppState,Remark)values('wxabecfe243f78bfef','谢春永测试','7fa21940796bc96d45abe36a38e65204','gh_29758758c9d8','1','正常','')

--End新增公众号

 
--Start新增用户
insert into SYS_User(AppId,RoleId,LoginId,Password,UserName,Telphone,Email,WXId,UserState)values('wxb51501fa9702675f',1,'Donald','Donald','Donald','18973305244',null,'gh_f98920d67737','正常')
insert into SYS_User(AppId,RoleId,LoginId,Password,UserName,Telphone,Email,WXId,UserState)values('wx5e6fd9f5a08db03a',1,'Litle','Litle','Litle','18973305244',null,'gh_ac32a7de334e','正常')
insert into SYS_User(AppId,RoleId,LoginId,Password,UserName,Telphone,Email,WXId,UserState)values('wxabecfe243f78bfef',1,'xcy','xcy','xcy','18973305244',null,'gh_29758758c9d8','正常')

--End新增用户


--新增功能
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (null,'系统管理','funcImg/icon1.png',null,'系统',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (null,'客服经理','funcImg/icon2.png',null,'系统',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (null,'客服功能','funcImg/icon3.png',null,'系统',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (null,'菜单管理','funcImg/icon4.png',null,'系统',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (null,'素材管理','funcImg/icon5.png',null,'系统',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (null,'系统设置','funcImg/icon6.png',null,'系统',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (1,'公众号管理',null,'/Base/App/Index','模块',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (1,'系统用户管理',null,'/Base/SYSUser/Index','模块',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (1,'角色管理',null,'/Base/Role/Index','模块',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (2,'客服管理',null,null,'模块',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (2,'标签管理',null,null,'模块',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (2,'组别管理',null,null,'模块',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (2,'粉丝管理',null,'/WXUser/Home/Index','模块',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (2,'常用语管理',null,null,'模块',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (3,'我的用户',null,'/wxcustom/home/index','模块',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (3,'我的分组',null,null,'模块',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (3,'我的二维码',null,'/wxcustom/qr/index','模块',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (3,'我的常用语',null,null,'模块',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (3,'我的素材',null,null,'模块',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (5,'公共素材管理',null,'/Base/Media/Index','模块',null)




--新增功能end

--新增权限

--新增权限end

--Start新增用户组别
insert into WX_UserGroup(GroupId,AppId,UserId,GroupName,GroupSort)values(0,null,null,'未分组',1)
--End新增用户组别
