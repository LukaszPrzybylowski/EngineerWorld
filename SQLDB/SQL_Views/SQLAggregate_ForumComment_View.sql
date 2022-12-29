CREATE VIEW [aggregate].[ForumComment]
AS
	SELECT
		t1.ForumCommentId,
		t1.ParentForumCommentId,
		t1.ForumId,
		t1.Content,
		t2.Username,
		t2.Profession,
		t2.Company,
		t1.ApplicationUserId,
		t1.PublishDate,
		t1.UpdateDate,
		t1.ActiveInd
	FROM
		dbo.ForumComment t1
	INNER JOIN
		dbo.ApplicationUser t2 ON t1.ApplicationUserId = t2.ApplicationUserId 