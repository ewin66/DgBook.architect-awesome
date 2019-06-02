SELECT  '[' + OBJECT_SCHEMA_NAME(ddips.[object_id],
														 DB_ID()) + '].['
			+ OBJECT_NAME(ddips.[object_id], DB_ID()) + ']' AS [statement] ,
			i.[name] AS [index_name] ,
			ddips.[index_type_desc] ,
			ddips.[partition_number] ,
			ddips.[alloc_unit_type_desc] ,
			ddips.[index_depth] ,
			ddips.[index_level] ,
			CAST(ddips.[avg_fragmentation_in_percent] AS SMALLINT) AS [avg_frag_%],
			ddips.[fragment_count] ,
			ddips.[page_count]
	FROM    sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, 'limited') ddips
			INNER JOIN sys.[indexes] i ON ddips.[object_id] = i.[object_id]
										  AND ddips.[index_id] = i.[index_id]
	where  OBJECT_NAME(ddips.[object_id], DB_ID()) in('Test_int','Test_uniqueidentifier')
	ORDER BY ddips.[avg_fragmentation_in_percent] ,
			OBJECT_NAME(ddips.[object_id], DB_ID()) ,
			i.[name]