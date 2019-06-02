set statistics time on

--常见写法：这种写法，每次b.PKCommunity='' 后面的变量发生变化，SQL在执行时都需要重新编译
SELECT 
	a.PKHouseDealRent 
FROM HouseDealRent a WITH(NOLOCK) JOIN Houses b WITH(NOLOCK) ON b.PKHouse=a.PKHouse 
WHERE b.PKCommunity='9DF75A44-494E-4412-89CB-9A01354A5CEE' ORDER BY a.LastRefreshTime DESC

--参数化写法：这种写法，每次b.PKCommunity='' 后面的变量发生变化，SQL在执行时不需要重新编译
exec sp_executesql N'SELECT 
	a.PKHouseDealRent 
FROM HouseDealRent a WITH(NOLOCK) JOIN Houses b WITH(NOLOCK) ON b.PKHouse=a.PKHouse 
WHERE b.PKCommunity=@PKObject ORDER BY a.LastRefreshTime DESC',
N'@PKObject uniqueidentifier',
@PKObject=N'9DF75A44-494E-4412-89CB-9A02355A5CEE'