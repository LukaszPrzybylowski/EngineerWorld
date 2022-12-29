CREATE PROCEDURE [dbo].[Article_GET]
	@ArticleId INT
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
			t1.[ArticleId] = @ArticleId AND
			t1.ActiveInd = CONVERT(BIT, 1)

