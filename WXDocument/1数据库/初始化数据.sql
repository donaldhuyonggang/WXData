use WXData
go

--Start新增用户角色
insert into SYS_Role(RoleSign,RoleName)values('SYS_ADMIN','超级管理员')
insert into SYS_Role(RoleSign,RoleName)values('APP_ADMIN','平台管理员')
insert into SYS_Role(RoleSign,RoleName)values('APP_CUSTOM','平台客服')
--End新增用户角色

--Start新增公众号
insert into WX_App(AppId,AppName,AppSecret,WXId,AppType,AppState,Remark)values('wxb51501fa9702675f','E妹时尚女装','a56e69ded9b5eab3579ce771f2f9a668','gh_f98920d67737','服务号','正常','')
insert into WX_App(AppId,AppName,AppSecret,WXId,AppType,AppState,Remark)values('wx5e6fd9f5a08db03a','李添乐测试','8f3079b54c134f3e67a88778b4a24f27','gh_ac32a7de334e','服务号','正常','')
insert into WX_App(AppId,AppName,AppSecret,WXId,AppType,AppState,Remark)values('wxabecfe243f78bfef','谢春永测试','7fa21940796bc96d45abe36a38e65204','gh_29758758c9d8','服务号','正常','')

--End新增公众号


--Start新增用户
insert into SYS_User(AppId,RoleId,LoginId,Password,UserName,Telphone,Email,WXId,UserState)values('wxb51501fa9702675f',1,'Donald','Donald','Donald','18973305244',null,'gh_f98920d67737','正常')
insert into SYS_User(AppId,RoleId,LoginId,Password,UserName,Telphone,Email,WXId,UserState)values('wx5e6fd9f5a08db03a',1,'Litle','Litle','Litle','18973305244',null,'gh_ac32a7de334e','正常')
insert into SYS_User(AppId,RoleId,LoginId,Password,UserName,Telphone,Email,WXId,UserState)values('wxabecfe243f78bfef',1,'xcy','xcy','xcy','18973305244',null,'gh_29758758c9d8','正常')

--End新增用户


--新增功能
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (null,'系统管理',null,null,'系统',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (null,'素材管理',null,null,'系统',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (null,'客服管理',null,null,'系统',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (null,'客服经理',null,null,'系统',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (null,'群发',null,null,'系统',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (null,'微信菜单',null,null,'系统',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (null,'设置',null,null,'系统',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (1,'公众号管理',null,null,'模块',null)
insert into SYS_Function(ParentID,FunctionName,ImageUrl,FunctionUrl,FunctionType,Remark) values (1,'系统用户管理',null,null,'模块',null)
--新增功能end

--新增权限
insert into SYS_Right(RoleId,FunctionID) values(1,1)
insert into SYS_Right(RoleId,FunctionID) values(1,2)
insert into SYS_Right(RoleId,FunctionID) values(1,3)
insert into SYS_Right(RoleId,FunctionID) values(1,4)
insert into SYS_Right(RoleId,FunctionID) values(1,5)
insert into SYS_Right(RoleId,FunctionID) values(1,6)
insert into SYS_Right(RoleId,FunctionID) values(1,7)
insert into SYS_Right(RoleId,FunctionID) values(1,8)
insert into SYS_Right(RoleId,FunctionID) values(1,9)
--新增权限end
