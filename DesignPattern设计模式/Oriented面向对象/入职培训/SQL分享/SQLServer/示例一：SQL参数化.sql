set statistics time on

--����д��������д����ÿ��b.PKCommunity='' ����ı��������仯��SQL��ִ��ʱ����Ҫ���±���
SELECT 
	a.PKHouseDealRent 
FROM HouseDealRent a WITH(NOLOCK) JOIN Houses b WITH(NOLOCK) ON b.PKHouse=a.PKHouse 
WHERE b.PKCommunity='9DF75A44-494E-4412-89CB-9A01354A5CEE' ORDER BY a.LastRefreshTime DESC

--������д��������д����ÿ��b.PKCommunity='' ����ı��������仯��SQL��ִ��ʱ����Ҫ���±���
exec sp_executesql N'SELECT 
	a.PKHouseDealRent 
FROM HouseDealRent a WITH(NOLOCK) JOIN Houses b WITH(NOLOCK) ON b.PKHouse=a.PKHouse 
WHERE b.PKCommunity=@PKObject ORDER BY a.LastRefreshTime DESC',
N'@PKObject uniqueidentifier',
@PKObject=N'9DF75A44-494E-4412-89CB-9A02355A5CEE'