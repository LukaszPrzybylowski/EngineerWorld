CREATE PROCEDURE [dbo].[Forum_Upsert]
	@Forum ForumType READONLY,
	@ApplicationUserId INT
AS
	
	MERGE INTO [dbo].[Forum] TARGET
	USING (
		SELECT
			ForumId,
			@ApplicationUserId [ApplicationUserId],
			Title,
			Content,
			PhotoId
		FROM
			@Forum
	) AS SOURCE
	ON(
		TARGET.ForumId = SOURCE.ForumId AND TARGET.ApplicationUserId = SOURCE.ApplicationUserId
	)
	WHEN MATCHED THEN
		UPDATE SET
			TARGET.[Title] = SOURCE.[Title],
			TARGET.[Content] = SOURCE.[Content],
			TARGET.[PhotoId] = SOURCE.[PhotoId],
			TARGET.[UpdateDate] = GETDATE()
	WHEN NOT MATCHED BY TARGET THEN
		INSERT (
			[ApplicationUserId],
			[Title],
			[Content],
			[PhotoId]
		)
		VALUES(
			SOURCE.[ApplicationUserId],
			SOURCE.[Title],
			SOURCE.[Content],
			SOURCE.[PhotoId]
		);

	SELECT CAST(SCOPE_IDENTITY() AS INT);