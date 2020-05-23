CREATE TABLE [dbo].[tbClient] (
    [Id]          INT        IDENTITY (1, 1) NOT NULL,
    [lastName]    NCHAR (50) NULL,
    [firstName]   NCHAR (50) NULL,
    [middleName]  NCHAR (50) NULL,
    [birthDate]   DATETIME   NULL,
    [loanSum]     INT        NULL,
    [image]       NCHAR (50) NULL,
    [DateRequest] NCHAR(50)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

