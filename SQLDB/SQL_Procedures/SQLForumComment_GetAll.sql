CREATE PROCEDURE [dbo].[ForumComment_GetAll]
	@ForumId INT
AS
SELECT 
	t1.[ForumCommentId]
      ,t1.[ParentForumCommentId]
      ,t1.[ForumId]
      ,t1.[ApplicationUserId]
	  ,t1.[Username]
      ,t1.[Content]
      ,t1.[PublishDate]
      ,t1.[UpdateDate]
  FROM [aggregate].[ForumComment] t1
  WHERE
	t1.[ForumId] = @ForumId AND
	t1.[ActiveInd] = CONVERT(BIT, 1)
  ORDER BY
	t1.[UpdateDate]
  DESC

