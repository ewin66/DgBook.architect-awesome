/***************************************************************************************
1、uniqueidentifier 类型聚集索引主键，insert 2000万条数据，3分20秒
2、索引层级4层，索引碎片 > 99%
***************************************************************************************/
--第一步：insert 数据，查看耗时
create table Test_uniqueidentifier(
	Id int,
	TestPrimaryKey uniqueidentifier not null primary key
)
go

with tep(ID,TestPrimaryKey) as
(
select 1,newid()
union all
select id+1,newid() from tep where id<=20000000
)
insert into Test_uniqueidentifier(Id,TestPrimaryKey)
select Id,TestPrimaryKey from tep option(maxrecursion 0)

--第二步：查看表上的索引结构，存储层级、碎片、索引页大小等

--第三步：rebuild 索引，消除碎片
alter index [PK__Test_uni__7569A1E929A46E64] on Test_uniqueidentifier rebuild

--第四步：查看表上的索引结构，存储层级、碎片、索引页大小等，此时两个表的索引碎片都是0%

--第五步：插入新的数据1万条,观察表上的索引结构，存储层级、碎片、索引页大小等，此时Test_uniqueidentifier碎片已产生大量碎片页

;with tep(ID,TestPrimaryKey) as
(
select 20000001,newid()
union all
select id+1,newid() from tep where id<=20010000
)
insert into Test_uniqueidentifier(Id,TestPrimaryKey)
select Id,TestPrimaryKey from tep option(maxrecursion 0)