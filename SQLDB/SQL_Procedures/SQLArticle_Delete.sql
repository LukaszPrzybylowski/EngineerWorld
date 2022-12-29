CREATE PROCEDURE [dbo].[Article_Delete]
	@ArticleId INT
AS
	
	UPDATE [dbo].[ArticleComment]
	SET [ActiveInd] = CONVERT(BIT, 0)
	WHERE [ArticleId] = @ArticleId

	UPDATE	[dbo].[Article]
	SET	
		[PhotoId] = NULL,
		[ActiveInd] = CONVERT(BIT, 0)
	WHERE
		[ArticleId] = @ArticleId
