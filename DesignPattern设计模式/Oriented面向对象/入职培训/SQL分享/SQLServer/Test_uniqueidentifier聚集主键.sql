/***************************************************************************************
1��uniqueidentifier ���;ۼ�����������insert 2000�������ݣ�3��20��
2�������㼶4�㣬������Ƭ > 99%
***************************************************************************************/
--��һ����insert ���ݣ��鿴��ʱ
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

--�ڶ������鿴���ϵ������ṹ���洢�㼶����Ƭ������ҳ��С��

--��������rebuild ������������Ƭ
alter index [PK__Test_uni__7569A1E929A46E64] on Test_uniqueidentifier rebuild

--���Ĳ����鿴���ϵ������ṹ���洢�㼶����Ƭ������ҳ��С�ȣ���ʱ�������������Ƭ����0%

--���岽�������µ�����1����,�۲���ϵ������ṹ���洢�㼶����Ƭ������ҳ��С�ȣ���ʱTest_uniqueidentifier��Ƭ�Ѳ���������Ƭҳ

;with tep(ID,TestPrimaryKey) as
(
select 20000001,newid()
union all
select id+1,newid() from tep where id<=20010000
)
insert into Test_uniqueidentifier(Id,TestPrimaryKey)
select Id,TestPrimaryKey from tep option(maxrecursion 0)