CREATE TYPE [dbo].[ArticleType] AS TABLE(
	[ArticleId] INT NOT NULL,
	[Title] VARCHAR(50) NOT NULL,
	[Content] VARCHAR(MAX) NOT NULL,
	[PhotoId] INT NULL
)