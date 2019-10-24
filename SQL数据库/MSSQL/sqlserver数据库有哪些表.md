

https://zhidao.baidu.com/question/1822486178436600428.html

# 查看sqlserver数据库有哪些表





查看sqlserver数据库有哪些表可以使用以下sql语句：

select name from sysobjects where xtype='u'；

或者select * from sys.tables；

或者SELECT * FROM INFORMATION_SCHEMA.TABLES；

**扩展资料**

sqlserver中各个系统表的作用介绍：

sysaltfiles 主数据库 保存数据库的文件

syscharsets 主数据库 字符集与排序顺序

[![img](sqlserver%E6%95%B0%E6%8D%AE%E5%BA%93%E6%9C%89%E5%93%AA%E4%BA%9B%E8%A1%A8.assets/c995d143ad4bd1133e7710e254afa40f4afb05a3.jpg)](https://gss0.baidu.com/-vo3dSag_xI4khGko9WTAnF6hhy/zhidao/pic/item/c995d143ad4bd1133e7710e254afa40f4afb05a3.jpg)

sysconfigures 主数据库 配置选项

syscurconfigs 主数据库 当前配置选项

sysdatabases 主数据库 服务器中的数据库

syslanguages 主数据库 语言

syslogins 主数据库 登陆帐号信息

sysoledbusers 主数据库 链接服务器登陆信息