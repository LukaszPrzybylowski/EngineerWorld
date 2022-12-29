CREATE PROCEDURE [dbo].[ArticleComment_Get]
	@ArticleCommentId INT
AS

SELECT 
	t1.[ArticleCommentId]
      ,t1.[ParentArticleCommentId]
      ,t1.[ArticleId]
      ,t1.[ApplicationUserId]
	  ,t1.[Username]
      ,t1.[Content]
      ,t1.[PublishDate]
      ,t1.[UpdateDate]
      ,t1.[ActiveInd]
  FROM [aggregate].[ArticleComment] t1
  WHERE
	t1.[ArticleCommentId] = @ArticleCommentId AND
	t1.[ActiveInd] = CONVERT(BIT, 1)

