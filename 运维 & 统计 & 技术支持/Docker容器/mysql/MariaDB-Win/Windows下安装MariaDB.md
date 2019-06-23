# Windows下安装MariaDB

​                                                   2013年04月20日 02:20:59           [吴军雄](https://me.csdn.net/wujunxiong)           阅读数 9084                   

​                   

**一、官网下载MariaDB**

​        地址：<https://downloads.mariadb.org/>



**二、安装**

​        1.解压。

​        2.解压后在目录下看到my-huge.ini、my-innodb-heavy-4G.ini、my-large.ini、my-medium.ini、my-small.ini  5个文件，根据机子内存大小选择其中之一COPY到C:\Windows目录下，并修改名字为my.ini。



​        3.打开my.ini，添加



​         [WinMySQLAdmin]  



​         Server=D:/dev/mariadb/bin/mysqld.exe



​        user=root



​        password=mariadb



​         [mysqld]   



​        basedir=解压后路径



​        datadir=解压后路径/data





**三、添加删除服务**



​        1.CMD,切换到MariaDB解压后路径/bin,执行"mysqld --install yourservicename"。



​        2.CMD,sc deleteyourservicename。





**四、系统错误1076**



​        启动服务时遇到“系统错误1076”，在MariaDB解压后路径/data目录下找到“主机名.err ”，即可找到原因。



**五、修改ROOT密码**



​        CMD，切换到MariaDB解压后路径/bin,执行“mysql -uroot  -p"切换至MariaDB模式，再执行“SET PASSWORD FOR 'root'@'localhost' = PASSWORD('新密码');”，即可。





**六、可视化工具(Navicat Premium)**

​         下载地址：http://xiazai.xiazaiba.com/Soft/N/Navicat_Premium_10.1.7_XiaZaiBa.exe。