

# Docker使用之[mysql的安装](https://blog.csdn.net/mdw5521/article/details/79174094)

```html


docker run --name mysql -p 3306:3306 -v /mysql/datadir:/var/lib/mysql -e MYSQL_ROOT_PASSWORD=123456  -d mysql
```

# [在Dockerfile中配置了ROOT密码为什么还是显示密码为空](https://segmentfault.com/q/1010000011389249)

`docker run -e MYSQL_ROOT_PASSWORD=123456`





```
docker run --name mysql -e MYSQL_ROOT_PASSWORD=123456 -p 3307:3306 -d mysql
```



docker run --name mysql58 -e MYSQL_ROOT_PASSWORD=123456 -p 3306:3306 -d mysql

docker pull  mysql/mysql-server : 8.0.15 

docker run --name mysql58 -d -p 3306:3306 
mysql/mysql-server:8.0.15  -- character-set-server=utf8mb4 
-- collation-server=utf8mb4_general_ci





docker exec -it mysql58 bash