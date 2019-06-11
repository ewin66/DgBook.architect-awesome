





# MariaDB企业版结合SQL与NoSQL

来源：网界网  

2014/4/4 17:00:34

[大](javascript:;)[中](javascript:;)[小](javascript:;)



为了帮助数据库管理员和开发人员更灵活地处理大量数据，SkySQL公司（基于开源MariaDB提供商业化产品）推出了基于MariaDB Enterprise和Enterprise Cluster的新产品，这些产品提供与NoSQL数据库的整合功能。

分享到： [0](http://www.vsharing.com/k/server/2014-4/697399.html#) [新浪微博](http://www.vsharing.com/k/server/2014-4/697399.html#) [腾讯微博](http://www.vsharing.com/k/server/2014-4/697399.html#)  

本文关键字： [MariaDB](http://search.vsharing.com/r/k_MariaDB/db_org)  [SQL](http://search.vsharing.com/r/k_SQL/db_org)  [NoSQL](http://search.vsharing.com/r/k_NoSQL/db_org)  [SkySQL](http://search.vsharing.com/r/k_SkySQL/db_org)  [数据库](http://search.vsharing.com/r/k_数据库/db_org) 

为了帮助数据库管理员和开发人员更灵活地处理大量数据，SkySQL公司（基于开源MariaDB提供商业化产品）推出了基于MariaDB Enterprise和Enterprise Cluster的新产品，这些产品提供与NoSQL数据库的整合功能。

 

随着移动设备和云服务用户的不断增加，企业要处理的数据量也在快速增长。这种情况已经改变了对数据库的需求，同时也给NoSQL数据库带来了机会。NoSQL数据库非常灵活，能够比传统的关系型数据库更加容易地进行扩展。但据SkySQL表示，对于每一家企业而言，SQL数据库及其提供的数据一致性仍然非常重要。

 

为了将这两个领域结合在一起，这个基于SQL的MariaDB  Enterprise 2和Enterprise Cluster 2整合了NoSQL数据库Apache  Cassandra。SkySQL公司销售副总裁Dion  Cornett表示，整合NoSQL是“姗姗来迟”的独特功能。他表示：“通常情况下，SQL一直是数据库管理员的领域，而NoSQL则是开发人员的领域。这个产品及其提供的灵活性桥接了这两个领域。”

 

新增加的Cassandra   SE（存储引擎）功能允许访问Cassandra集群中的数据。在这种整合后，Cassandra的存储卷现在看起来像是MariaDB中的一个表。用户可以插入数据到这些表中，并从其中选择数据。用户还可以整合存储在MariaDB和Cassandra中的数据。

 

另外的一个消息是，MariaDB  10数据库的发布，这是对开源MySQL的替换，也是MariaDB Enterprise 2和MariaDB Enterprise  Cluster 2的基石。MariaDB基金会表示，MariaDB 10比之前的两个版本都更快，因为增加了并行复制等功能。MariaDB  10还包含内置共享Spider存储引擎的形式，允许大数据[注]库表分布在多个[服务器](http://server.vsharing.com/)，以获得更好的性能和规模。

 

SkySQL称，谷歌和维基百科等公司已经在数千个数据库中使用MariaDB。从5.5版本跳转到10版本对于MariaDB是很大的一步，版本名称也不再模仿MySQL（目前的版本号是5.6）。放弃采用MySQL的版本编号是MariaDB在2012年做出的决定，当时引起了一些质疑。

 

版本编号变化的目的是为了更加清晰地显示MariaDB和MySQL之间的差异。MariaDB包含与MySQL中相应功能的类似功能，但部署方式不同，并且它包含了MySQL没有的功能。近期，SkySQL的MariaDB网站上公布了MariaDB  10与MariaDB 5.5、MySQL 5.6的功能对比。

 

新版MariaDB Enterprise 2和Enterprise Cluster 2的发布，以及MariaDB 10的发布，并不是巧合。开源数据库供应商和用户本周聚集在Percona Live MySQL大会，这一会议在4月1日拉开了序幕。

