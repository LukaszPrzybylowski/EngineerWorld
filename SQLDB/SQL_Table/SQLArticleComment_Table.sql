CREATE TABLE ArticleComment (
	ArticleCommentId INT NOT NULL IDENTITY(1,1),
	ParentArticleCommentId INT NULL,
	ArticleId INT NOT NULL,
	ApplicationUserId INT NOT NULL,
	Content VARCHAR(300) NOT NULL,
	PublishDate DATETIME NOT NULL DEFAULT GETDATE(),
	UpdateDate DATETIME NOT NULL DEFAULT GETDATE(),
	ActiveInd BIT NOT NULL DEFAULT CONVERT(BIT, 1),
	PRIMARY KEY (ArticleCommentId),
	FOREIGN KEY (ArticleId) REFERENCES Article(ArticleId),
	FOREIGN KEY (ApplicationUserId) REFERENCES ApplicationUser(ApplicationUserId)
)