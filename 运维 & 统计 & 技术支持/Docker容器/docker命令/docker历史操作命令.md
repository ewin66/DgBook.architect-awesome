# docker历史操作命令

```
 清除(关闭全部容器) ：docker kill $(docker ps -a -q)
 删除全部容器：docker rm $(docker ps -a -q)
```

> ping 是测试ip或者域名是否通
>  telnet 才是测试端口是否通

```
https://www.xinyuefeifei.com

docker run -d -P -p 4444:4444 -v E:/Test001/docfile:/dev/shm --name selenium-hub1 selenium/hub

# 同时停止多个容器
docker stop cec2573c3a3c  cf1108d6ed37 81860f31e887

# 同时删除多个容器
docker rm cec2573c3a3c  cf1108d6ed37 81860f31e887

docker run -d --name selenium-hub -p 4444:4444 selenium/hub
docker run -d -P -p 5900:5900 --link selenium-hub:hub selenium/node-chrome-debug
docker run -d -P -p 5901:5900 --link selenium-hub:hub selenium/node-firefox-debug
docker ps -a
#以上四条命令的作用分别是：
#第一条：启动一个Hub的镜像，名称为selenium-hub
#第二条：启动一个node的镜像（带chrome浏览器），和vnc通信的端口为5900
#第三条：启动一个node的镜像（带firefox浏览器），和vnc通信的端口为5901
#检查hub和node的链接情况，用命令：docker logs selenium-hub


# window执行, 映射对外ip地址
route add 172.17.0.0/8 mask 255.255.255.0 10.0.75.1
获取所有容器名称及其IP地址只需一个命令
docker inspect -f '{{.Name}} - {{.NetworkSettings.IPAddress }}' $(docker ps -aq)
如果使用docker-compose命令将是：
docker inspect -f '{{.Name}} - {{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' $(docker ps -aq)
显示所有容器IP地址：
docker inspect --format='{{.Name}} - {{range .NetworkSettings.Networks}}{{.IPAddress}}{{end}}' $(docker ps -aq)

通过容器 id 获取 ip 
$ sudo docker inspect <container_id> | grep IPAddress | cut -d ’"’ -f 4
#设置基础镜像baseImage
FROM alpine:latest

#设置 MAINTAINER
LABEL maintainer xinyuefeifei 

#定义时区变量--此系统需要安装 timezone 数据包 apk add -U tzdata
ENV TIME_ZONE Asia/Shanghai

# 设置镜像仓库地址
RUN echo "https://mirror.tuna.tsinghua.edu.cn/alpine/v3.8/main/" > /etc/apk/repositories

# 创建临时文件夹
WORKDIR /home/InstallFile
ARG path=/home/InstallFile

COPY ./*.whl ${path}/ 

RUN apk add --no-cache -U python3 \
    && apk add --no-cache -U tzdata \
    && if [ ! -e /usr/bin/pip ]; then ln -s pip3 /usr/bin/pip ; fi \
    && if [[ ! -e /usr/bin/python ]]; then ln -sf /usr/bin/python3 /usr/bin/python; fi \
    && python3 -m ensurepip \
    && rm -r /usr/lib/python*/ensurepip \
    && pip install ${path}/pip-18.0-py2.py3-none-any.whl \
    && pip install ${path}/*.whl \
    #设置时区
    && ln -sf /usr/share/zoneinfo/${TIME_ZONE} /etc/localtime \ 
    && echo "${TIME_ZONE}" > /etc/timezone \ 
    #&& rm -rf ${path}/*
ENTRYPOINT ["/bin/sh"]
# docker中启动locust测试脚步,在外部访问
# test 是包含测试脚本locustfile.py文件的docker镜像
docker run -it -p 8089:8089 test  locust -f /home/locustfile.py --host=http://www.baidu.c
```

作者：心悦飞飞

链接：https://www.jianshu.com/p/15a8338b936d

来源：简书

简书著作权归作者所有，任何形式的转载都请联系作者获得授权并注明出处。