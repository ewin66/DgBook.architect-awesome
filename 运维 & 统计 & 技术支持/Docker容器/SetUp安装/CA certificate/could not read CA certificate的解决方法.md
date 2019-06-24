# could not read CA certificate的解决方法





版权声明：本文为博主原创文章，未经博主允许不得转载。 https://blog.csdn.net/wuchengzeng/article/details/85232547

这篇文章忽略所有的概念, 我们直接切入主题:
安装过Docker Toolbox（Windows 7，Windows 8用这个）， 所以Windows 10的话就不需要Docker Toolbox。 如果安装过Docker Toolbox，会出现类似如下错误：
没有发现CA凭证等。
“could not read CA certificate “C:\Users\username\.docker\machine\machines\default\ca.pem”: open C:\Users\yqiu29.docker\machine\machines\default\ca.pem: The system cannot find the file specified.”
解决方法：
1.控制面板–>系统和安全–>系统–>高级系统设置–>环境变量–>用户变量中的有四个前缀是DOCKER_变量全部删掉，点击确定即可。
在这里插入图片描述
在这里插入图片描述
在Powershell中执行如下命令：docker-machine env -u
could not read CA certificate docker-machine env -u

结果的最后一行会提示怎样设置环境，复制，然后执行，如下如所示：
复制第二红框 & 符号及后面的内容，复制到下面然后运行。
2.然后以管理员身份运行CMD或者PowerShell,
输入以下指令：

docker --version
docker-compose --version 
docker-machine --version 
docker ps
docker version
docker info
docker run hello-world

```bash
docker --version
docker-compose --version 
docker-machine --version 
docker ps
docker version
docker info
docker run hello-world

```

docker version
docker run hello-world
--------------------- 
作者：Websites 
来源：CSDN 
原文：https://blog.csdn.net/websites/article/details/85232547 
版权声明：本文为博主原创文章，转载请附上博文链接！