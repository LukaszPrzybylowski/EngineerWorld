CREATE PROCEDURE [dbo].[ArticleComment_Upsert]
	@ArticleComment ArticleCommentType READONLY,
	@ApplicationUserId INT
AS

	MERGE INTO [dbo].[ArticleComment] TARGET
	USING (
		SELECT
			[ArticleCommentId],
			[ParentArticleCommentId],
			[ArticleId],
			[Content],
			@ApplicationUserId [ApplicationUserId]
		FROM
		@ArticleComment
	)	AS SOURCE
	ON
	(
		TARGET.[ArticleCommentId] = SOURCE.[ArticleCommentId] AND TARGET.[ApplicationUserId] = SOURCE.[ApplicationUserId]
	)
	WHEN MATCHED THEN
		UPDATE SET	
			TARGET.[Content] = SOURCE.[Content],
			TARGET.[UpdateDate] = GETDATE()
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (
			[ParentArticleCommentId],
			[ArticleId],
			[ApplicationUserId],
			[Content]
		)
		VALUES
		(
			SOURCE.[ParentArticleCommentId],
			SOURCE.[ArticleId],
			SOURCE.[ApplicationUserId],
			SOURCE.[Content]
		);

	SELECT CAST(SCOPE_IDENTITY() AS INT);