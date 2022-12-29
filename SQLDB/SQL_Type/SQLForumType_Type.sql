CREATE TYPE [dbo].[ForumType] AS TABLE(
	[ForumId] INT NOT NULL,
	[Title] VARCHAR(50) NOT NULL,
	[Content] VARCHAR(MAX) NOT NULL,
	[PhotoId] INT NULL
)