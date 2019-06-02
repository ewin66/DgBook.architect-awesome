/****************************************************************************************
问题点：跨表的列进行 or 运算，SQL OS无法正确选择索引。
优化：拆分后用union all或者union合并
原SQL：
SELECT TOP(1) @PKUser=U.PKUser,@_name=UserName,@_email=Email,@_pkcity=PKCity,@_type=UserType 
FROM Users U WITH(NOLOCK) LEFT OUTER JOIN UserAlias UA WITH(NOLOCK) ON U.PKUser=UA.PKUser  
WHERE U.Cellphone=@cellPhone OR Email =@email OR UA.Cellphone=@cellPhone 
ORDER BY ClientLastLoginTime DESC

改动之后：从平均2S降低到100MS以内
;with temptable as(
	SELECT U.PKUser,UserName,Email,PKCity,UserType,ClientLastLoginTime
	FROM Users U WITH(NOLOCK) LEFT OUTER JOIN UserAlias UA WITH(NOLOCK) ON U.PKUser=UA.PKUser  
	WHERE U.Cellphone=@cellPhone OR Email =@email--OR UA.Cellphone=@cellPhone 
	union all
	SELECT U.PKUser,UserName,Email,PKCity,UserType,ClientLastLoginTime 
	FROM Users U WITH(NOLOCK) LEFT OUTER JOIN UserAlias UA WITH(NOLOCK) ON U.PKUser=UA.PKUser  
	WHERE --U.Cellphone=@cellPhone OR Email =@email OR 
	UA.Cellphone=@cellPhone 
)
select top(1) @PKUser=PKUser,@_name=UserName,@_email=Email,@_pkcity=PKCity,@_type=UserType from temptable order by ClientLastLoginTime desc
****************************************************************************************/
exec NewSta_Activity_AddReservationUser @pkCity='6B4AD5B8-78C4-4CA7-92F8-817E48B5DB87',@name=N'李丹',@cellPhone=N'17685687156',@email=N'',@type=0,@intents=N'手机微站'