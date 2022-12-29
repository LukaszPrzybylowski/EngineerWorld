CREATE PROCEDURE [dbo].[ForumComment_Delete]
	@ForumCommentId INT
AS

	DROP TABLE IF EXISTS #ForumCommentsToBeDeleted;

	WITH cte_forumComments AS (
		
		SELECT
			t1.[ForumCommentId],
			t1.[ParentForumCommentId]
		FROM
			[dbo].[ForumComment] t1
		WHERE
			t1.[ForumCommentId] = @ForumCommentId
		UNION ALL
		SELECT 
			t2.[ForumCommentId],
			t2.[ParentForumCommentId]
		FROM
			[dbo].[ForumComment] t2
			INNER JOIN cte_forumComments t3
				ON t3.[ForumCommentId] = t2.[ParentForumCommentId]

	)
	SELECT 
		[ForumCommentId],
		[ParentForumCommentId]
	INTO
		#ForumCommentsToBeDeleted
	FROM
		cte_forumComments;

	UPDATE t1
	SET
		t1.[ActiveInd] = CONVERT(BIT, 1),
		t1.[UpdateDate] = GETDATE()
	FROM
		[dbo].[ForumComments] t1
		INNER JOIN #ForumCommentsToBeDeleted t2
			ON t1.[ForumCommentId] = t2.[ForumCommentId]
