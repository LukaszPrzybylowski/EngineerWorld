CREATE PROCEDURE [dbo].[Article_GetAll]
	@Offset INT,
	@PageSize INT
AS
	SELECT 
		   [ArticleId]
		  ,[ApplicationUserId]
		  ,[Username]
		  ,[Title]
		  ,[Content]
		  ,[PhotoId]
		  ,[PublishDate]
		  ,[UpdateDate]
		  ,[ActiveInd]
	  FROM
		[aggregate].[Article] t1
	  WHERE
	    t1.[ActiveInd] = CONVERT(BIT, 1)
	  ORDER BY
		t1.[ArticleId]
	 OFFSET @Offset ROWS
	 FETCH NEXT @PageSize ROWS ONLY;

	 SELECT COUNT(*) FROM [aggregate].[Article] t1 WHERE t1.[ActiveInd] = CONVERT(BIT, 1);