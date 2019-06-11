### [MariaDB 10正式发布，引入NoSQL特性](https://www.iteye.com/news/28926-MariaDB-release)                                                         

​           2014-04-01 13:09 by 副主编 [wangguo](https://wangguo.iteye.com)           [评论(10)](https://www.iteye.com/news/28926-MariaDB-release#comments)           有14695人浏览                    

​                       [MariaDB](https://www.iteye.com/news/tag/MariaDB)                       [MySQL](https://www.iteye.com/news/tag/MySQL)                       [NoSQL](https://www.iteye.com/news/tag/NoSQL)                   

#### 声明：ITeye资讯文章的版权属于ITeye网站所有，严禁任何网站转载本文，否则必将追究法律责任！

经过了多个测试版本，MariaDB基金会正式发布了MariaDB 10版本

。在该分支之前，MariaDB的版本号与MySQL保持一致。 

![img](assets/124530c5-1605-3b9a-ba28-1da5ebe69a5a.png)

MariaDB是MySQL的一个分支，由MySQL的创始人Michael Widenius主导开发。MariaDB的API和协议兼容MySQL，另外又添加了一些功能。开发这个分支的原因之一是为了避免MySQL被甲骨文公司收购之后可能存在的闭源风险。 

自发布以来，MariaDB社区增长迅速，MariaDB也逐渐代替MySQL成为Red Hat、Fedora、Suse、Debian等Linux发行版的数据库管理系统。 

MariaDB 10包含了诸多大的改进，其中包括来自Google、Fusion-IO和淘宝等大型互联网企业所开发的创新特性。

该版本的主要改进如下： 

复制改进：

- MariaDB 10在性能上树立了一个新的标准，比之前的几个分支速度更快。 
- 现在可以通过多来源复制功能，从多个主服务器中复制数据。

NoSQL特性：

- CONNECT引擎支持动态访问不同的数据源，包括非结构化的文件，比如文件夹中的日志文件或任意ODBC数据库。 
- 更好的ETL（Extraction、Transformation、Load）和实时分析能力。 
- MariaDB 10中，动态列（Dynamic Columns）存储表中每一行不同标签的数据对象时的方式和NoSQL技术基本一致。 
- 可以直接访问Cassandra数据库中的数据，并可以直接与目前主流的大数据技术进行交互。

分片（Sharding）改进：

- MariaDB 10内置了SPIDER引擎形式的分片功能，允许将大数据表跨多个服务器进行分割，与新的复制功能相结合，大大提升了可用性。

更多信息

：

The MariaDB Blog

下载地址

：

https://downloads.mariadb.org/mariadb/