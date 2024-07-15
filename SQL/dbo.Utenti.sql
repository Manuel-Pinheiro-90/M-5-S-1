CREATE TABLE [dbo].[Utenti] (
    [IdUtente]     INT            IDENTITY (1, 1) NOT NULL,
    [Username]     NVARCHAR (50)  NOT NULL,
    [PasswordHash] NVARCHAR (255) NOT NULL,
    [Ruolo]        NVARCHAR (20)  NOT NULL,
    PRIMARY KEY CLUSTERED ([IdUtente] ASC),
    CHECK ([RUOLO]='Admin' OR [RUOLO]='user')
);

