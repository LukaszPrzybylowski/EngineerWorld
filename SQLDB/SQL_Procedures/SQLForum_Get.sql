CREATE PROCEDURE [dbo].[Forum_Get]
	@ForumId INT
AS
	SELECT 
		   [ForumId]
		  ,[ApplicationUserId]
		  ,[Username]
		  ,[Title]
		  ,[Content]
		  ,[PhotoId]
		  ,[PublishDate]
		  ,[UpdateDate]
	  FROM 
		   [aggregate].[Forum] t1
	  WHERE
			t1.[ForumId] = @ForumId AND
			t1.ActiveInd = CONVERT(BIT, 1)

