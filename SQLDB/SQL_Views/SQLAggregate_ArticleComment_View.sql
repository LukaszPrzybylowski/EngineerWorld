CREATE VIEW [aggregate].[ArticleComment]
AS
	SELECT
		t1.ArticleCommentId,
		t1.ParentArticleCommentId,
		t1.ArticleId,
		t1.Content,
		t2.Username,
		t2.Company,
		t1.ApplicationUserId,
		t1.PublishDate,
		t1.UpdateDate,
		t1.ActiveInd
	FROM
		dbo.ArticleComment t1
	INNER JOIN
		dbo.ApplicationUser t2 ON t1.ApplicationUserId = t2.ApplicationUserId