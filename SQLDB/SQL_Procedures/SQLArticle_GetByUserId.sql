CREATE PROCEDURE [dbo].[Article_GetByUserId]
	@ApplicaitonUserId	INT
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
		t1.[ApplicationUserId] = @ApplicaitonUserId AND
		t1.[ActiveInd] = CONVERT(BIT, 1)

