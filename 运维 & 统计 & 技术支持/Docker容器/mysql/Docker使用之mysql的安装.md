2.5：使用 docker run 启动镜像，以Mysql为例，出现下方字符串，启动成功

    #  docker run --name mysql -p 3306:3306 -v /mysql/datadir:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=123456  -d mysql
    787b06122b723e547b1216d499b69d99725a703217cd09f49e9ce548f9ba27dc

--name: 以什么名字启动容器

-p 3306:3306 :将容器端口映射到服务器端口

-v /mysql/datadir:/var/lib/mysql :将mysql的配置路径映射到本地datadir上

-e MYSQL_ROOT_PASSWORD=123456 :设置服务器密码为123456

-d mysql：需要启动的容器的名称
--------------------- 
作者：mdw5521 
来源：CSDN 
原文：https://blog.csdn.net/mdw5521/article/details/79174094 
版权声明：本文为博主原创文章，转载请附上博文链接！