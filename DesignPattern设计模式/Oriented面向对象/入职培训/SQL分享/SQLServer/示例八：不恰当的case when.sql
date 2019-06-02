drop table #DealsTable
create table #DealsTable(
	PKUser uniqueidentifier,
	HouseType smallint,
	BusinessSource int,
	Modifier int,
	BusinessState int
)

declare @PKCompany uniqueidentifier ='E0B8C984-9F0A-462E-B6D6-34427937A3FA'
,@PKUser uniqueidentifier ='61807D09-D644-4AFF-A1A1-40E0A5E0A186'

--create nonclustered index IDX_PKUser on [dbo].HouseDealRent([PKUser])with(online=on) on [IndexGroup]
--create nonclustered index IDX_PKUser on [dbo].HouseDealSecond([PKUser])with(online=on) on [IndexGroup]
--create nonclustered index IDX_PKUser on [dbo].HouseDealNew([PKUser])with(online=on) on [IndexGroup]
--drop index IDX_PKUser on [dbo].HouseDealRent
--drop index IDX_PKUser on [dbo].HouseDealSecond
--drop index IDX_PKUser on [dbo].HouseDealNew

	--优化：不改变原逻辑的前提下

	--insert into #DealsTable
	--select * from 
	--	( 
	--		select PKUserOwner PKUser, HouseType = 0, BusinessSource, Modifier, BusinessState from HouseDealRent WITH(NOLOCK) where PKCompany=CASE WHEN @PKCompany IS NULL THEN PKCompany ELSE @PKCompany END AND DeleteStatus = 0 AND PKUser=CASE WHEN @PKUser IS NULL THEN PKUser ELSE @PKUser END
	--		union all
	--		select PKUserOwner PKUser, HouseType = 1, BusinessSource, Modifier, BusinessState from HouseDealSecond WITH(NOLOCK) where PKCompany=CASE WHEN @PKCompany IS NULL THEN PKCompany ELSE @PKCompany END AND DeleteStatus=0 AND PKUser=CASE WHEN @PKUser IS NULL THEN PKUser ELSE @PKUser END
	--		union all
	--		select PKUserOwner PKUser, HouseType = 2, BusinessSource, Modifier, BusinessState from HouseDealNew WITH(NOLOCK) where PKCompany=CASE WHEN @PKCompany IS NULL THEN PKCompany ELSE @PKCompany END AND DeleteStatus=0 AND PKUser=CASE WHEN @PKUser IS NULL THEN PKUser ELSE @PKUser END 
	--	) T 

	--select * from #DealsTable

	if @PKCompany is not null and @PKUser is not null
	begin
		insert into #DealsTable
		select * from 
		( 
			select PKUserOwner PKUser, HouseType = 0, BusinessSource, Modifier, BusinessState from HouseDealRent WITH(NOLOCK) where PKCompany=@PKCompany AND DeleteStatus = 0 AND PKUser=@PKUser
			union all
			select PKUserOwner PKUser, HouseType = 1, BusinessSource, Modifier, BusinessState from HouseDealSecond WITH(NOLOCK) where PKCompany=@PKCompany AND DeleteStatus=0 AND PKUser=@PKUser
			union all
			select PKUserOwner PKUser, HouseType = 2, BusinessSource, Modifier, BusinessState from HouseDealNew WITH(NOLOCK) where PKCompany=@PKCompany AND DeleteStatus=0 AND PKUser=@PKUser
		) T 
	end

	else if @PKCompany is not null and @PKUser is null
	begin
		insert into #DealsTable
		select * from 
		( 
			select PKUserOwner PKUser, HouseType = 0, BusinessSource, Modifier, BusinessState from HouseDealRent WITH(NOLOCK) where PKCompany=@PKCompany AND DeleteStatus = 0 --AND PKUser=@PKUser
			union all
			select PKUserOwner PKUser, HouseType = 1, BusinessSource, Modifier, BusinessState from HouseDealSecond WITH(NOLOCK) where PKCompany=@PKCompany AND DeleteStatus=0 --AND PKUser=@PKUser
			union all
			select PKUserOwner PKUser, HouseType = 2, BusinessSource, Modifier, BusinessState from HouseDealNew WITH(NOLOCK) where PKCompany=@PKCompany AND DeleteStatus=0 --AND PKUser=@PKUser
		) T 
	end

	else if @PKCompany is null and @PKUser is not null
	begin
		insert into #DealsTable
		select * from 
		( 
			select PKUserOwner PKUser, HouseType = 0, BusinessSource, Modifier, BusinessState from HouseDealRent WITH(NOLOCK) where /*PKCompany=@PKCompany AND*/ DeleteStatus = 0 AND PKUser=@PKUser
			union all
			select PKUserOwner PKUser, HouseType = 1, BusinessSource, Modifier, BusinessState from HouseDealSecond WITH(NOLOCK) where /*PKCompany=@PKCompany AND*/ DeleteStatus=0 AND PKUser=@PKUser
			union all
			select PKUserOwner PKUser, HouseType = 2, BusinessSource, Modifier, BusinessState from HouseDealNew WITH(NOLOCK) where /*PKCompany=@PKCompany AND*/ DeleteStatus=0 AND PKUser=@PKUser
		) T
	end

	else
	begin
		insert into #DealsTable
		select * from 
		( 
			select PKUserOwner PKUser, HouseType = 0, BusinessSource, Modifier, BusinessState from HouseDealRent WITH(NOLOCK) where /*PKCompany=@PKCompany AND*/ DeleteStatus = 0 --AND PKUser=@PKUser
			union all
			select PKUserOwner PKUser, HouseType = 1, BusinessSource, Modifier, BusinessState from HouseDealSecond WITH(NOLOCK) where /*PKCompany=@PKCompany AND*/ DeleteStatus=0 --AND PKUser=@PKUser
			union all
			select PKUserOwner PKUser, HouseType = 2, BusinessSource, Modifier, BusinessState from HouseDealNew WITH(NOLOCK) where /*PKCompany=@PKCompany AND*/ DeleteStatus=0 --AND PKUser=@PKUser
		) T
	end

	select * from #DealsTable