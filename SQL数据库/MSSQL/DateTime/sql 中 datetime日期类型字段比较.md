sql 中 datetime日期类型字段比较

 

**mysql** 

可以直接用大于号，也可以用 between and

```
SELECT * FROM staff WHERE UPDATE_DATE >= '2019-08-14 11:41:09' AND UPDATE_DATE <= '2019-08-14 11:41:11';

SELECT * FROM staff WHERE UPDATE_DATE BETWEEN '2019-08-14 11:41:09' AND '2019-08-14 11:41:11';
```

 

Oracle

转：

[oracle sql日期比较](http://www.blogjava.net/forker/archive/2007/09/07/143467.html)

```
oracle sql日期比较:
在今天之前:
```

select * from up_date where update < to_date('2007-09-07 00:00:00','yyyy-mm-dd hh24:mi:ss')
select * from up_date where update <= to_date('2007-09-07 00:00:00','yyyy-mm-dd hh24:mi:ss')

```
在今天只后:
```

select * from up_date where update > to_date('2007-09-07 00:00:00','yyyy-mm-dd hh24:mi:ss')
select * from up_date where update >= to_date('2007-09-07 00:00:00','yyyy-mm-dd hh24:mi:ss')

```
精确时间:
```

select * from up_date where update = to_date('2007-09-07 00:00:00','yyyy-mm-dd hh24:mi:ss')

```
在某段时间内:
```

select * from up_date where update between to_date('2007-07-07 00:00:00','yyyy-mm-dd hh24:mi:ss') and to_date('2007-09-07 00:00:00','yyyy-mm-dd hh24:mi:ss')
select * from up_date where update < to_date('2007-09-07 00:00:00','yyyy-mm-dd hh24:mi:ss') and update > to_date('2007-07-07 00:00:00','yyyy-mm-dd hh24:mi:ss')
select * from up_date where update <= to_date('2007-09-07 00:00:00','yyyy-mm-dd hh24:mi:ss') and update >= to_date('2007-07-07 00:00:00','yyyy-mm-dd hh24:mi:ss')