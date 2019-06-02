/****************************************************************************************
问题点：谓词in内包含带有谓词or的子查询会导致SQL OS无法判断出正确的数据量，进而生成错误的执行计划
优化：程序无需变更，仅修改SQL实现方式，性能提升30倍+
	1、将谓词in内的子查询用临时表join代替
****************************************************************************************/
--效率低的存储过程，平均执行时间2S，优化后100MS
exec Weizhan_GetWeizhanTrailVisitSta @PKCompany='BFB2AD4B-595C-44A8-B173-023A9E43F9A2'

--主要优化地方：
--原SQL：
SELECT PKWeizhan,MAX(VisitCountTotal),MAX(LastVisitTime) FROM WeizhanVisitSta WITH(NOLOCK) 
WHERE PKWeizhan IN 
(
	SELECT PKWeizhan FROM Weizhans WITH(NOLOCK) WHERE (SiteType=0 AND PKObject=@PKCompany) 
	OR (
		SiteType=1 AND PKObject IN (
			SELECT PKUser FROM Users WITH(NOLOCK) WHERE PKCompany = @PKCompany
			)
		)
)
GROUP BY PKWEIZHAN
--改动之后：
declare @PKWeizhan table ([PKWeizhan] [uniqueidentifier])
insert into @PKWeizhan
SELECT PKWeizhan FROM Weizhans WITH(NOLOCK) 
WHERE (SiteType=0 AND PKObject=@PKCompany) 
	OR (
		SiteType=1 AND PKObject IN (
			SELECT PKUser FROM Users WITH(NOLOCK) WHERE PKCompany = @PKCompany
			)
		)

SELECT a.PKWeizhan,max(VisitCountTotal),max(LastVisitTime)
FROM WeizhanVisitSta a WITH(NOLOCK) inner join @PKWeizhan b on a.PKWeizhan=b.PKWeizhan 
GROUP BY a.PKWEIZHAN

