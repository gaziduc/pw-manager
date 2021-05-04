CREATE TABLE [dbo].[users] (
    [id]       BIGINT         IDENTITY (1, 1) NOT NULL,
    [login]    NVARCHAR (128) NULL,
    [password] NVARCHAR (128) NULL,
    CONSTRAINT [PK_dbo.users] PRIMARY KEY CLUSTERED ([id] ASC)
);

