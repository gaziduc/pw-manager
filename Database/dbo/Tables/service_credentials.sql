CREATE TABLE [dbo].[service_credentials] (
    [id]          BIGINT          IDENTITY (1, 1) NOT NULL,
    [name]        NVARCHAR (50)   NULL,
    [url]         NVARCHAR (2048) NULL,
    [login]       NVARCHAR (MAX)  NULL,
    [password]    NVARCHAR (MAX)  NULL,
    [user_id]     BIGINT          NOT NULL,
    [category_id] SMALLINT        NOT NULL,
    [is_favorite] BIT             NOT NULL,
    CONSTRAINT [PK_dbo.service_credentials] PRIMARY KEY CLUSTERED ([id] ASC)
);

