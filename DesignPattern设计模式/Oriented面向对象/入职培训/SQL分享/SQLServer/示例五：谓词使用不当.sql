/****************************************************************************************
����㣺ν��in�ڰ�������ν��or���Ӳ�ѯ�ᵼ��SQL OS�޷��жϳ���ȷ�����������������ɴ����ִ�мƻ�
�Ż������������������޸�SQLʵ�ַ�ʽ����������30��+
	1����ν��in�ڵ��Ӳ�ѯ����ʱ��join����
****************************************************************************************/
--Ч�ʵ͵Ĵ洢���̣�ƽ��ִ��ʱ��2S���Ż���100MS
exec Weizhan_GetWeizhanTrailVisitSta @PKCompany='BFB2AD4B-595C-44A8-B173-023A9E43F9A2'

--��Ҫ�Ż��ط���
--ԭSQL��
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
--�Ķ�֮��
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

