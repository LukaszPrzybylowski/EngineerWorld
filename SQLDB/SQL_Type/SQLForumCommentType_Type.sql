CREATE TYPE [dbo].[ForumCommentType] AS TABLE(
	[ForumCommentId] [int] NOT NULL,
	[ParentForumCommentId] [int] NULL,
	[ForumId] [int] NOT NULL,
	[Content] [varchar](1000) NOT NULL
)



