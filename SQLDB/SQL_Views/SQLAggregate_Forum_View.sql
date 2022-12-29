CREATE VIEW [aggregate].[Forum]
AS
	SELECT
		t1.ForumId,
		t1.ApplicationUserId,
		t2.Username,
		t2.Profession,
		t1.Title,
		t1.Content,
		t1.PhotoId,
		t1.PublishDate,
		t1.UpdateDate,
		t1.ActiveInd
	FROM
		dbo.Forum t1
	INNER JOIN
		dbo.ApplicationUser t2 ON t1.ApplicationUserId = t2.ApplicationUserId