/****************************************************************************************
问题点：单独把下面的SQL拿出来执行，两个语句的执行计划是一样的。但是放到存储过程里，就会出现差异。差异的原因是存储过程的预编译+传参导致执行计划选择异常。属于参数嗅探的一种
优化：尽早的缩小结果集
原SQL：
SELECT  UserName,SmsContent,SendTime FROM 
(
	select u.UserName,u.PKCompany,sr.SmsContent,SR.SendTime,ROW_NUMBER() OVER(PARTITION BY u.PKCompany ORDER BY u.ClientLastLoginTime desc) AS CNT 
	from SmsRecords sr join Users u on u.Cellphone=sr.CellphoneList and u.PKCompany=sr.PKCompany 
	where u.UserName<>'网站委托'
) A WHERE PKCompany=@PKCompany AND A.CNT=1

改动之后
SELECT  UserName,SmsContent,SendTime FROM 
(
	select u.UserName,u.PKCompany,sr.SmsContent,SR.SendTime,ROW_NUMBER() OVER(PARTITION BY u.PKCompany ORDER BY u.ClientLastLoginTime desc) AS CNT 
	from SmsRecords sr join Users u on u.Cellphone=sr.CellphoneList and u.PKCompany=sr.PKCompany 
	where u.UserName<>'网站委托' and u.PKCompany=@PKCompany
) A WHERE A.CNT=1
****************************************************************************************/
exec Expert_OA_GetCompanyTackingByType @Type=3,@PKCompany='44F61AE9-BCC5-4FD3-9520-0026A061C06E'