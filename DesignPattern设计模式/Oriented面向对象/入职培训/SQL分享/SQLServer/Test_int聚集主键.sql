/***************************************************************************************
1��int ���;ۼ�����������insert 2000�������ݣ�2��26��
2�������㼶4�㣬������Ƭ 0%
***************************************************************************************/
--��һ����insert ���ݣ��鿴��ʱ
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

--�ڶ������鿴���ϵ������ṹ���洢�㼶����Ƭ������ҳ��С��

--��������rebuild ������������Ƭ��Test_int�������

--���Ĳ����鿴���ϵ������ṹ���洢�㼶����Ƭ������ҳ��С�ȣ���ʱ�������������Ƭ����0%

--���岽�������µ�����1����,�۲���ϵ������ṹ���洢�㼶����Ƭ������ҳ��С�ȣ���ʱTest_int��Ȼû�в�����Ƭҳ

;with tep(Id) as
(
select 20000001
union all
select Id+1 from tep where Id<=20010000
)
insert into Test_int(Id)
select Id from tep option(maxrecursion 0)