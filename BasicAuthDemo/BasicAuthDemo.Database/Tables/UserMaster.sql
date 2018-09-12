CREATE TABLE [dbo].[UserMaster]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[UserName] NVARCHAR(50),
	[Password] NVARCHAR(50),
	[IsActive] INT
)
