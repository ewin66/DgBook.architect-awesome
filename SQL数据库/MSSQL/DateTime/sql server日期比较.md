# [sqlserver比较日期大小](https://www.cnblogs.com/linyuansun/p/10837852.html)

   通常sqlserver并不能直接比较日期大小，需用convert函数转换为响应的格式。

   常用 convert(varchar(8),getnow(),112)

   对 convert(data_type(length),data,style) ，把 data 按格式 style 转换为 data_type的数据，长度为length



style  对应格式

100   mon dd yyy hh:miAM(或PM)
101   mm/dd/yy
102   yy.mm.dd
103   dd/mm/yy
104   dd.mm.yy
105   dd-mm-yy
106   dd mon yy
107   Mon dd,yy
108   hh:mm:ss
109   mon dd yyyy hh:mi:ss:mmmAM(或PM)
110   mm-dd-yy
111   yy/mm/dd
112   yymmdd
113   dd mon yyyy hh:mm:ss:mmm(24h)
114   hh:mis:ss:mmm(24h)
120   yyyy-mm-dd hh:mi:ss(24h)
121   yyyy-mm-dd hh:mi:ss.mmm(24h)
126   yyyy-mm-ddThh:mm:ss.mmm(没有空格)
130   dd mon yyyy hh:mi:ss:mmmAM
131   dd/mm/yy hh:mi:ss:mmmAM

# MSSql中[比较日期的](https://cloud.tencent.com/developer/ask/46355)最佳方法 

-  

## 有一个SQL`datetime`

```js
declare @dateVar datetime = '2013-03-11;

select t.[DateColumn]
from MyTable t
where t.[DateColumn] = dateVar;
```

用户回答回答于 2018-02-11

## 转换为`DATE`

```js
DECLARE @dateVar datetime = '19700204';

-- Quickest when there is an index on t.[DateColumn], 
-- because CONVERT can still use the index.
SELECT t.[DateColumn]
FROM MyTable t
WHERE = CONVERT(DATE, t.[DateColumn]) = CONVERT(DATE, @dateVar);

-- Quicker when there is no index on t.[DateColumn]
DECLARE @dateEnd datetime = DATEADD(DAY, 1, @dateVar);
SELECT t.[DateColumn] 
FROM MyTable t
WHERE t.[DateColumn] >= @dateVar AND 
      t.[DateColumn] < @dateEnd;
```

以下是一个例子：

## 有一个Order表，其中有一个名为OrderDate的日期时间字段

```js
1) WHERE DateDiff(dd, OrderDate, '01/01/2006') = 0
2) WHERE Convert(varchar(20), OrderDate, 101) = '01/01/2006'
3) WHERE Year(OrderDate) = 2006 AND Month(OrderDate) = 1 and Day(OrderDate)=1
4) WHERE OrderDate LIKE '01/01/2006%'
5) WHERE OrderDate >= '01/01/2006'  AND OrderDate < '01/02/2006'
```



# 两个日期（datetime）的年月相差多少个月

select DATEDIFF(MONTH,'2019-02-01','2018-05-13')

# sql server[日期比较](https://blog.csdn.net/qq_24364529/article/details/79112939)

​    转载       [深圳古月月](https://me.csdn.net/qq_24364529)                               

###    1. 当前系统日期、时间
select getdate() 
###    2. dateadd 在向指定日期加上一段时间的基础上，返回新的 datetime 值
例如：向日期加上2天
select dateadd(day,2,'2004-10-15') --返回：2004-10-17 00:00:00.000

###    3. datediff 返回跨两个指定日期的日期和时间边界数。
select datediff(day,'2004-09-01','2004-09-18') --返回：17

###    4. datepart 返回代表指定日期的指定日期部分的整数。
SELECT DATEPART(month, '2004-10-15') --返回 10

###    5. datename 返回代表指定日期的指定日期部分的字符串
SELECT datename(weekday, '2004-10-15') --返回：星期五

###    6. day(), month(),year() --可以与datepart对照一下

select 当前日期=convert(varchar(10),getdate(),120)
,当前时间=convert(varchar(8),getdate(),114)

###    7. select datename(dw,'2004-10-15')

select 本年第多少周=datename(week,getdate())
,今天是周几=datename(weekday,getdate())



函数 参数/功能 
GetDate( ) --返回系统目前的日期与时间 
DateDiff (interval,date1,date2) --以interval 指定的方式，返回date2 与date1两个日期之间的差值 date2-date1
DateAdd (interval,number,date) --以interval指定的方式，加上number之后的日期 
DatePart (interval,date) ---返回日期date中，interval指定部分所对应的整数值 
DateName (interval,date) --返回日期date中，interval指定部分所对应的字符串名称 

参数 interval的设定值如下：

值 缩 写（Sql Server） Access 和 ASP 说明 
Year Yy yyyy 年 1753 ~ 9999 
Quarter Qq q 季 1 ~ 4 
Month Mm m 月1 ~ 12 
Day of year Dy y 一年的日数,一年中的第几日 1-366 
Day Dd d 日，1-31 
Weekday Dw w 一周的日数，一周中的第几日 1-7 
Week Wk ww 周，一年中的第几周 0 ~ 51 
Hour Hh h 时0 ~ 23 
Minute Mi n 分钟0 ~ 59 
Second Ss s 秒 0 ~ 59 
Millisecond Ms - 毫秒 0 ~ 999 

access 和 asp 中用date()和now()取得系统日期时间；其中DateDiff,DateAdd,DatePart也同是能用于Access和asp中，这些函数的用法也类似

举例：
1.GetDate() 用于sql server :select GetDate()

2.DateDiff('s','2005-07-20','2005-7-25 22:56:32')返回值为 514592 秒
DateDiff('d','2005-07-20','2005-7-25 22:56:32')返回值为 5 天

3.DatePart('w','2005-7-25 22:56:32')返回值为 2 即星期一(周日为1，周六为7)
DatePart('d','2005-7-25 22:56:32')返回值为 25即25号
DatePart('y','2005-7-25 22:56:32')返回值为 206即这一年中第206天
DatePart('yyyy','2005-7-25 22:56:32')返回值为 2005即2005年 

SQL Server DATEPART() 函数返回 SQLServer datetime 字段的一部分。 

SQL Server DATEPART() 函数的语法是： 
DATEPART(portion, datetime)

其中 datetime 是 SQLServer datetime 字段和部分的名称是下列之一： Ms for Milliseconds
Yy for Year
Qq for Quarter of the Year
Mm for Month
Dy for the Day of the Year
Dd for Day of the Month
Wk for Week
Dw for the Day of the Week
Hh for Hour
Mi for Minute
Ss for Second


--1.编写函数，实现按照'年月日，星期几,上午下午晚上'输出时间信息(2009年3月16日星期一下午)
select datename(yy,getdate()) + '年' +
datename(mm,getdate()) + '月' + 
datename(dd,getdate()) + '日' +
datename(weekday,getdate()) +
case when datename(hh,getdate()) < 12 then '上午' else '下午' end 
--2.编写函数，根据输入时间。输出该天是该年的第几天
select datepart(dy,getdate())
--3.求出随机输出字符‘a-z
select char(97+abs(checksum(newid()))%26)
select char(97+rand()*26)                                    