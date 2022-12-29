CREATE PROCEDURE [dbo].[Account_Insert]
	@Account AccountType READONLY
AS
	INSERT INTO [dbo].[ApplicationUser]
			   ([UserRole],
			    [Username]
			   ,[NormalizedUsername]
			   ,[Email]
			   ,[NormalizedEmail]
			   ,[Fullname]
			   ,[Lastname]
			   ,[Company]
			   ,[Profession]
			   ,[PasswordHash])
	SELECT 
		[UserRole],
		[Username],
		[NormalizedUsername],
		[Email]
		,[NormalizedEmail]
		,[Fullname]
		,[Lastname]
		,[Company]
		,[Profession]
		,[PasswordHash]
	FROM
		@Account;

	SELECT CAST(SCOPE_IDENTITY() AS INT);

