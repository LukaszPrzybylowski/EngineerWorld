CREATE PROCEDURE [dbo].[ForumComment_Upsert]
	@ForumComment ForumCommentType READONLY,
	@ApplicationUserId INT
AS

	MERGE INTO [dbo].[ForumComment] TARGET
	USING (
		SELECT
			[ForumCommentId],
			[ParentForumCommentId],
			[ForumId],
			[Content],
			@ApplicationUserId [ApplicationUserId]
		FROM
		@ForumComment
	)	AS SOURCE
	ON
	(
		TARGET.[ForumCommentId] = SOURCE.[ForumCommentId] AND TARGET.[ApplicationUserId] = SOURCE.[ApplicationUserId]
	)
	WHEN MATCHED THEN
		UPDATE SET	
			TARGET.[Content] = SOURCE.[Content],
			TARGET.[UpdateDate] = GETDATE()
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (
			[ParentForumCommentId],
			[ForumId],
			[ApplicationUserId],
			[Content]
		)
		VALUES
		(
			SOURCE.[ParentForumCommentId],
			SOURCE.[ForumId],
			SOURCE.[ApplicationUserId],
			SOURCE.[Content]
		);

	SELECT CAST(SCOPE_IDENTITY() AS INT);