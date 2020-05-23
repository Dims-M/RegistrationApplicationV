CREATE TABLE [dbo].[Client]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [lastName] NCHAR(50) NULL, 
    [firstName] NCHAR(50) NULL, 
    [middleName] NCHAR(50) NULL, 
    [birthDate] DATETIME NULL, 
    [loanSum] INT NULL, 
    [image] NCHAR(50) NULL
)
