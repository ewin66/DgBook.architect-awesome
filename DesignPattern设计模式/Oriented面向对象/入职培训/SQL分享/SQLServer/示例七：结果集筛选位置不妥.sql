/****************************************************************************************
����㣺�����������SQL�ó���ִ�У���������ִ�мƻ���һ���ġ����Ƿŵ��洢������ͻ���ֲ��졣�����ԭ���Ǵ洢���̵�Ԥ����+���ε���ִ�мƻ�ѡ���쳣�����ڲ�����̽��һ��
�Ż����������С�����
ԭSQL��
SELECT  UserName,SmsContent,SendTime FROM 
(
	select u.UserName,u.PKCompany,sr.SmsContent,SR.SendTime,ROW_NUMBER() OVER(PARTITION BY u.PKCompany ORDER BY u.ClientLastLoginTime desc) AS CNT 
	from SmsRecords sr join Users u on u.Cellphone=sr.CellphoneList and u.PKCompany=sr.PKCompany 
	where u.UserName<>'��վί��'
) A WHERE PKCompany=@PKCompany AND A.CNT=1

�Ķ�֮��
SELECT  UserName,SmsContent,SendTime FROM 
(
	select u.UserName,u.PKCompany,sr.SmsContent,SR.SendTime,ROW_NUMBER() OVER(PARTITION BY u.PKCompany ORDER BY u.ClientLastLoginTime desc) AS CNT 
	from SmsRecords sr join Users u on u.Cellphone=sr.CellphoneList and u.PKCompany=sr.PKCompany 
	where u.UserName<>'��վί��' and u.PKCompany=@PKCompany
) A WHERE A.CNT=1
****************************************************************************************/
exec Expert_OA_GetCompanyTackingByType @Type=3,@PKCompany='44F61AE9-BCC5-4FD3-9520-0026A061C06E'