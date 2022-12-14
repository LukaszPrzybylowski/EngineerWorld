USE [EngineerWorldDB]
GO
/****** Object:  StoredProcedure [dbo].[Article_Upsert]    Script Date: 21.12.2022 17:58:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Article_Upsert]
	@Article ArticleType READONLY,
	@ApplicationUserId INT
AS
	
	MERGE INTO [dbo].[Article] TARGET
	USING (
		SELECT
			ArticleId,
			@ApplicationUserId [ApplicationUserId],
			Title,
			Content,
			PhotoId
		FROM
			@Article
	) AS SOURCE
	ON(
		TARGET.ArticleId = SOURCE.ArticleId AND TARGET.ApplicationUserId = SOURCE.ApplicationUserId
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