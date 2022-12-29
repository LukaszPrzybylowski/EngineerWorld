ALTER PROCEDURE [dbo].[ArticleComment_GetAll]
	@ArticleId INT
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
  FROM [aggregate].[ArticleComment] t1
  WHERE
	t1.[ArticleId] = @ArticleId AND
	t1.[ActiveInd] = CONVERT(BIT, 1)
  ORDER BY
	t1.[UpdateDate]
  DESC


