CREATE PROCEDURE [dbo].[Forum_GetByUserId]
	@ApplicaitonUserId	INT
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
		t1.[ApplicationUserId] = @ApplicaitonUserId AND
		t1.[ActiveInd] = CONVERT(BIT, 1)

