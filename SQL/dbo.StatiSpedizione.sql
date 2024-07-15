CREATE TABLE [dbo].[StatiSpedizione] (
    [IdStatoSpedizione]    INT            IDENTITY (1, 1) NOT NULL,
    [IdSpedizione]         INT            NOT NULL,
    [Stato]                NVARCHAR (50)  NOT NULL,
    [Luogo]                NVARCHAR (255) NOT NULL,
    [Descrizione]          NVARCHAR (255) NULL,
    [DataOraAggiornamento] DATETIME2 (7)  NOT NULL,
    PRIMARY KEY CLUSTERED ([IdStatoSpedizione] ASC),
    FOREIGN KEY ([IdSpedizione]) REFERENCES [dbo].[Spedizioni] ([IdSpedizione])
);

