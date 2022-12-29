CREATE TYPE [dbo].[AccountType] AS TABLE(
	[UserRole] VARCHAR (20) NOT NULL,
	[Username] VARCHAR(20) NOT NULL,
	[NormalizedUsername] VARCHAR(20) NOT NULL,
	[Email] VARCHAR(30) NOT NULL,
	[NormalizedEmail] VARCHAR(30) NOT NULL,
	[Fullname] VARCHAR(30) NULL,
	[Lastname] VARCHAR(30) NULL,
	[Company] VARCHAR(30) NULL,
	[Profession] VARCHAR(30) NOT NULL,
	[PasswordHash] NVARCHAR(MAX) NOT NULL
)