



# 【MySql】查询数据库中所有表及列的信息

```mysql
SELECT
TABLE_NAME,
COLUMN_NAME,
DATA_TYPE,
COLUMN_COMMENT
FROM
INFORMATION_SCHEMA. COLUMNS
WHERE
TABLE_SCHEMA = 'research_home'		
```



https://blog.csdn.net/ystyaoshengting/article/details/78841560



mysql获取某个数据库的所有表名

```
select TABLE_NAME from information_schema.tables where TABLE_SCHEMA="your database name"
```



mysql获取某个表的所有字段名  

```
select COLUMN_NAME from information_schema.COLUMNS where table_name = 'your_table_name';
```




 上述的做法有一点问题，如果多个数据库中存在你想要查询的表名，那么查询的结果会包括全部的字段信息。通过DESC information_schema.COLUMNS可以看到该表中列名为TABLE_SCHEMA是记录数据库名，因此下面的写法更为严格







# mysql查询某个表所有字段名

有时我们需要获取一个表的所有字段名，比如我们要往一个表里面插入数据（利用已有数据，当然这只是为了制造测试数据而已），那么我们就可以使用下面的方法：

select GROUP_CONCAT(COLUMN_NAME) from information_schema.COLUMNS where table_name = '表名' and table_schema ="数据库名";


作者：johnHuster 
来源：CSDN 
原文：https://blog.csdn.net/john1337/article/details/75349269 
版权声明：本文为博主原创文章，转载请附上博文链接！

 select COLUMN_NAME from information_schema.COLUMNS where table_name = 'your_table_name' and table_schema = 'your_db_name';