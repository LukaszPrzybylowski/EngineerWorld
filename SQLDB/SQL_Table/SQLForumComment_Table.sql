CREATE TABLE ForumComment(
	ForumCommentId INT NOT NULL IDENTITY(1,1),
	ParentForumCommentId INT NULL,
	ForumId INT NOT NULL,
	ApplicationUserId INT NOT NULL,
	Content VARCHAR(500) NOT NULL,
	PublishDate DATETIME NOT NULL DEFAULT GETDATE(),
	UpdateDate DATETIME NOT NULL DEFAULT GETDATE(),
	ActiveInd BIT NOT NULL DEFAULT CONVERT(BIT, 1),
	PRIMARY KEY(ForumCommentId),
	FOREIGN KEY(ForumId) REFERENCES Forum(ForumId),
	FOREIGN KEY(ApplicationUserId) REFERENCES ApplicationUser(ApplicationUserId)
)