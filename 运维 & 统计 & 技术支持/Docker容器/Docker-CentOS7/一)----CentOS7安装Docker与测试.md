

# Docker(一)----CentOS7安装Docker与测试

  2016年11月05日 20:36:53           [lfendo](https://me.csdn.net/u011781521)           阅读数 2970                                                                  

​                   

 版权声明：本文为博主原创文章，未经博主允许不得转载。                    

 https://blog.csdn.net/u011781521/article/details/53047674                 

一、安装docker



  yum -y install docker-io



![img](assets/20161105204624450)



安装成功！



二、启动Docker



​    service docker start

​    ps -ef | grep docker



![img](assets/20161105205021424)



●加入开机启动

​     chkconfig docker on



![img](assets/20161105205436020)



●从docker.io中下载centos镜像到本地  /var/lib/docker/graph

​     docker pull centos:latest



![img](assets/20161105213443512)



●查看已下载的镜像

​    docker images



![img](assets/20161105213648296)



●启动一个容器

 docker  run -i -t centos /bin/bash



![img](assets/20161105213815662)



●查看所有容器

  docker  ps -a



![img](assets/20161105214007038)