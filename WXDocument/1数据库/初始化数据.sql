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

--End新增公众号


--Start新增用户
insert into SYS_User(AppId,RoleId,LoginId,Password,UserName,Telphone,Email,WXId,UserState)values('wxb51501fa9702675f',1,'Donald','Donald','Donald','18973305244',null,'gh_f98920d67737','正常')
insert into SYS_User(AppId,RoleId,LoginId,Password,UserName,Telphone,Email,WXId,UserState)values('wx5e6fd9f5a08db03a',1,'Litle','Litle','Litle','18973305244',null,'gh_ac32a7de334e','正常')
--End新增用户

