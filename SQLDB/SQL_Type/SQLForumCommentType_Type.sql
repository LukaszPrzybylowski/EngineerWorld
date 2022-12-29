CREATE TYPE [dbo].[ForumCommentType] AS TABLE(
	[ForumCommentId] INT NOT NULL,
	[ParentForumCommentId] INT NULL,
	[ForumId] INT NOT NULL,
	[Content] VARCHAR(1000) NOT NULL
)