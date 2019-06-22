# Ubuntu15安装Docker

​                                                   2017年02月15日 21:47:48           [lfendo](https://me.csdn.net/u011781521)           阅读数 788                                                                  

​                   

​                                                                                                                                             版权声明：本文为博主原创文章，未经博主允许不得转载。                     https://blog.csdn.net/u011781521/article/details/55222935                 

**1.更新系统**



sudo apt-get update



![img](assets/20170215205651173)



**二、添加Docker的APT仓库**



先通过命令



lsb_release



查看Ubuntu发行版本



![img](assets/20170215210137992)



报: No lsb modules are available 错误、解决方法为安装LSB模块


 sudo apt-get install lsb-core



![img](assets/20170215210338229)



再次输入



sudo lsb_release -- codename | cut -f2 



命令进行查看Ubuntu发行版本



![img](assets/20170215211005831)



然后添加Docker的APT仓库地址



sudo sh -c "echo deb https://apt.dockerproject.org/repo ubuntu-wily main > /etc/apt/sources.list.d/docker.list"



![img](assets/20170215211218613)





**三、添加Docker仓库的GPG秘钥**



sudo apt-key adv --keyserver hkp://p80.pool.sks-keyservers.net:80 --recv-keys 58118E89F3A912897C070ADBF76221572C52609D



![img](assets/20170215211738303)



**四、更新APT源**



sudo apt-get update



![img](assets/20170215212026782)



**五、安装Docker**



sudo apt-get install docker-engine



自Docker 1.8.0开始，Docker的软件包名称已经从Lxc-docker变为docker-engine。



![img](assets/20170215212503821)



**六、测试Docker**



sudo docker info



![img](assets/20170215214657647)