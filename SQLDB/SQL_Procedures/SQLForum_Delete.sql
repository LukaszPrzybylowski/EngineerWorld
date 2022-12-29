CREATE PROCEDURE [dbo].[Forum_Delete]
	@ForumId INT
AS
	
	UPDATE [dbo].[ForumComment]
	SET [ActiveInd] = CONVERT(BIT, 0)
	WHERE [ForumId] = @ForumId

	UPDATE	[dbo].[Forum]
	SET	
		[PhotoId] = NULL,
		[ActiveInd] = CONVERT(BIT, 0)
	WHERE
		[ForumId] = @ForumId
