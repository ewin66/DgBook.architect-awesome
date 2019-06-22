

###	获取镜像

docker pull mysql

### 使用镜像

docker run -d -P mysql

docker ps -a 发现没有启动

docker logs 10

docker run -d -P -e MYSQL_ROOT_PASSWORD=root mysql









sudo docker rename modest_goldberg mysql



sudo docker run --name mysql_client --link mysql:db -d mysql



##	docker run 命令参数

-d 后台运行

-p 暴露端口

-e 设置环境变量，与在dockerfile env设置相同效果

--name 设置namne


https://hub.docker.com/_/mysql/

http://blog.csdn.net/wongson/article/details/49077353
--------------------- 
作者：芋智波佐助 
来源：CSDN 
原文：https://blog.csdn.net/u011686226/article/details/52815788 
版权声明：本文为博主原创文章，转载请附上博文链接！



## mysql 修改



```bash
mysql>use mysql;
mysql>update  user set host = '%' where user= 'root';
mysql>selecthost, userfromuser;
```

(docker-compose，也是这样)。