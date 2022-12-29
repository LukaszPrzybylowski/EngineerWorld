CREATE TYPE [dbo].[ArticleCommentType] AS TABLE(
	[ArticleCommentId] INT NOT NULL,
	[ParentArticleCommentId] INT NULL,
	[ArticleId] INT NOT NULL,
	[Content] VARCHAR(300) NOT NULL
)