/***************************************************************************************
1、int 类型聚集索引主键，insert 2000万条数据，2分26秒
2、索引层级4层，索引碎片 0%
***************************************************************************************/
--第一步：insert 数据，查看耗时
create table Test_int(
	Id int,
	TestInt int identity(1,1) not null primary key,
)
go

with tep(Id) as
(
select 1
union all
select Id+1 from tep where Id<=20000000
)
insert into Test_int(Id)
select Id from tep option(maxrecursion 0)

--第二步：查看表上的索引结构，存储层级、碎片、索引页大小等

--第三步：rebuild 索引，消除碎片，Test_int无需操作

--第四步：查看表上的索引结构，存储层级、碎片、索引页大小等，此时两个表的索引碎片都是0%

--第五步：插入新的数据1万条,观察表上的索引结构，存储层级、碎片、索引页大小等，此时Test_int仍然没有产生碎片页

;with tep(Id) as
(
select 20000001
union all
select Id+1 from tep where Id<=20010000
)
insert into Test_int(Id)
select Id from tep option(maxrecursion 0)