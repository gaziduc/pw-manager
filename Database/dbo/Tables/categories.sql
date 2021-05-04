CREATE TABLE [dbo].[categories] (
    [id]   SMALLINT      IDENTITY (1, 1) NOT NULL,
    [name] NVARCHAR (30) NULL,
    CONSTRAINT [PK_dbo.categories] PRIMARY KEY CLUSTERED ([id] ASC)
);