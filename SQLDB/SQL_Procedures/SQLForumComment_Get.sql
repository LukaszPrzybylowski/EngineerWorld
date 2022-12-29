CREATE PROCEDURE [dbo].[ForumComment_Get]
	@ForumCommentId INT
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
	t1.[ForumCommentId] = @ForumCommentId AND
	t1.[ActiveInd] = CONVERT(BIT, 1)

