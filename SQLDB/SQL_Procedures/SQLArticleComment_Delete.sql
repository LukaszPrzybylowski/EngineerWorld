ALTER PROCEDURE [dbo].[ArticleComment_Delete1]
	@ArticleCommentId INT
AS

	DROP TABLE IF EXISTS #ArticleCommentsToBeDeleted;

	WITH cte_blogComments AS (
		
		SELECT
			t1.[ArticleCommentId],
			t1.[ParentArticleCommentId]
		FROM
			[dbo].[ArticleComment] t1
		WHERE
			t1.[ArticleCommentId] = @ArticleCommentId
		UNION ALL
		SELECT 
			t2.[ArticleCommentId],
			t2.[ParentArticleCommentId]
		FROM
			[dbo].[ArticleComment] t2
			INNER JOIN cte_articleComments t3
				ON t3.[ArticleCommentId] = t2.[ParentArticleCommentId]

	)
	SELECT 
		[ArticleCommentId],
		[ParentArticleCommentId]
	INTO
		#ArticleCommentsToBeDeleted
	FROM
		cte_blogComments;

	UPDATE t1
	SET
		t1.[ActiveInd] = CONVERT(BIT, 1),
		t1.[UpdateDate] = GETDATE()
	FROM
		[dbo].[BlogComments] t1
		INNER JOIN #ArticleCommentsToBeDeleted t2
			ON t1.[ArticleCommentId] = t2.[ArticleCommentId]
GO