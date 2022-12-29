CREATE PROCEDURE [dbo].[Forum_GetAll]
	@Offset INT,
	@PageSize INT
AS
	SELECT 
		   [ForumId]
		  ,[ApplicationUserId]
		  ,[Username]
		  ,[Title]
		  ,[Content]
		  ,[PhotoId]
		  ,[PublishDate]
	  FROM
		[aggregate].[Forum] t1
	  WHERE
	    t1.[ActiveInd] = CONVERT(BIT, 1)
	  ORDER BY
		t1.[ForumId]
	 OFFSET @Offset ROWS
	 FETCH NEXT @PageSize ROWS ONLY;

	 SELECT COUNT(*) FROM [aggregate].[Forum] t1 WHERE t1.[ActiveInd] = CONVERT(BIT, 1);