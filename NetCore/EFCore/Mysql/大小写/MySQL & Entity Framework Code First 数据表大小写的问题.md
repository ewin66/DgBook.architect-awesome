##  			[MySQL & Entity Framework Code First 数据表大小写的问题](https://www.cnblogs.com/jlzhou/archive/2013/03/18/2966106.html) 		

以前都是使用Linux平台下的MySQL，现在为了调试方便，在开发机上安装了MySQL的Windows版本5.6.10，在使用Entity  Framework Code  First生成数据库对象时，发现所有的数据表的表名都变成了小写字符，而字段的名称是混合大小写的，这个问题在Linux平台上没有出现过，于是着手弄明白这是肿么一回事。

经过网上搜索，找到这篇文章：

>  [Entity Framework with mysql, Table Capitalization issue between linux and windows](http://stackoverflow.com/questions/9445678/entity-framework-with-mysql-table-capitalization-issue-between-linux-and-window)

大致的意思是说：这个问题产生的根源是操作系统，MySQL保存数据表到文件，最初MySQL是在Linux平台开发的，文件名和数据表名称都是大小写敏感的，因为绝大多数的Linux文件系统是大小写敏感的。

后来，MySQL推出Windows平台的版本，而Windows平台是大小写不敏感的，所以无法区分大小写的名称。为了解决这个问题，需要添加设置来忽略表名的大小写。于是建立了lower_case_table_names设置选项。在Windows平台可以在my.ini文件中设置，该文件在Windows7或Windows2008操作系统中位于 C:\ProgramData\MySQL\MySQL Server 5.6` 目录下。在Linux平台可以修改my.cnf中的设置项。`

缺省的选项－－
Linux平台：大小写敏感 Case-Sensitive 
Windows平台：大小写不敏感 Case-Insenstitive

问题的原因找到啦，我们可以在Linux中设置lower_case_table_names为1启用大小写不敏感。也可以设置MySQL在Windows中大小写敏感，但是这不是个好主意。

记住，更改设置后需要重启MySQL服务。

在Linux中最好使用大小写敏感的设置，会获得更高的性能。在Windows平台最好使用大小写不敏感的设置，因为不能存在两个仅有大小写差异而文件名字母相同的表。

由此而生，这也是为什么Linux下MySQL的性能会好过Windows下的原因。（其他原因有：更好的计划调度更快的磁盘IO和文件系统）

也可以在创建数据库时使用下面的语句来指定大小写敏感设置：

```
CREATE DATABASE test_database CHARACTER SET utf8 COLLATE utf8_general_cs; //实测：这一句不能在Windows平台下的MySQL使用；

CREATE DATABASE test_database CHARACTER SET utf8 COLLATE utf8_general_ci;
```

相应的，也可以为某一个数据表设置大小写敏感：

[![复制代码](assets/copycode.gif)](javascript:void(0);)

```
DROP TABLE IF EXISTS single_test_table;
CREATE TABLE single_test_table(
  single_test_id int unsigned NOT NULL auto_increment,
  ...

  PRIMARY KEY PK_single_test_id (single_test_id ),
  ...
) ENGINE=MyISAM DEFAULT CHARSET=utf8 COLLATE utf8_general_cs;
```

[![复制代码](https://common.cnblogs.com/images/copycode.gif)](javascript:void(0);)

官方文档5.6.10中指出：

If you are using InnoDB tables, you should set this variable to 1 on all platforms to force names to be converted to lowercase. 

如果使用InnoDB引擎，应该在所有的平台设置lower_case_table_names＝1强制名称都转换为小写。

```
￼[mysqld]
lower_case_table_names=1
```

 如果要使Windows平台下的MySQL和Linux平台下的MySQL协同工作，你需要设置Linux平台下的全局变量lower_case_table_names=1，强制将数据表名称转换为小写（大小写不敏感）。

 

后记：

如果在Windows平台下设置了lower_case_table_names=0，意味着开启大小写敏感，这时Entity Framework Code First初始化数据库创建的表名是大小写混合的，可是在执行SQL语句查询时，SQL表名仍然是大小写不敏感的。

Entity Framework Code First初始化数据库创建了一个表：testdb.AppUsers，在Workbench中执行一个SQL语句，使用小写的数据表名（如果用testdb.AppUsers就不会出问题），例如SELECT  * FROM testdb.appusers，不关查询窗口，然后尝试删除数据库：DROP DATABASE  testdb，数据库无法正确的删除，会在testdb目录下留下一个文件：appusers.idb，而且MySQL服务也无法停掉，强制重启系统也无济于事，最后只能卸载MySQL数据库软件，删除数据目录，然后重新安装。

又经过反复交叉试验（这要感谢VMWare Fusion的Snapshot功能），Windows平台下去掉lower_case_table_names=0，就不会有上述问题，得出结论：不要玩火！不能在Windows平台下启用大小写敏感！

 







----



你是在Windows下吧，MySQL在Linux下一般是区分大小写的，而在Windows下都不区分大小写，当然这只是默认配置，你可以更改，只要对数据库的配置做下改动就行了，在MySQL的配置文件中my.ini 中增加一行

　　lower_case_table_names = 1

　　参数解释：

　　0：区分大小写

　　1：不区分大小写

https://ask.csdn.net/questions/172578

---

