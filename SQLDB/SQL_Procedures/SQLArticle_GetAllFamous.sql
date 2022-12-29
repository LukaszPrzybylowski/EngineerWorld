CREATE PROCEDURE [dbo].[Aricle_GetAllFamous]
AS
	SELECT 
		   t1.[ArticleId]
		  ,t1.[ApplicationUserId]
		  ,t1.[Username]
		  ,t1.[PhotoId]
		  ,t1.[Title]
		  ,t1.[Content]
		  ,t1.[PublishDate]
		  ,t1.[UpdateDate]
		  ,t1.[ActiveInd]
	  FROM 
		   [aggregate].[Article] t1
	  INNER JOIN
		   [dbo].[ArticleComment] t2 ON t1.ArticleId = t2.ArticleId
	  WHERE
		t1.ActiveInd = CONVERT(BIT, 1) AND
		t2.ActiveInd = CONVERT(BIT, 1)
	  GROUP BY
		   t1.[ArticleId]
		  ,t1.[ApplicationUserId]
		  ,t1.[Username]
		  ,t1.[PhotoId]
		  ,t1.[Title]
		  ,t1.[Content]
		  ,t1.[PublishDate]
		  ,t1.[UpdateDate]
		  ,t1.[ActiveInd]
	 ORDER BY
		COUNT(t2.ArticleCommentId)
	 DESC

