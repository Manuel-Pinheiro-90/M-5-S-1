CREATE TABLE [dbo].[Spedizioni] (
    [IdSpedizione]           INT             IDENTITY (1, 1) NOT NULL,
    [IdCliente]              INT             NOT NULL,
    [NumeroIdentificativo]   NVARCHAR (50)   NOT NULL,
    [DataSpedizione]         DATETIME2            NOT NULL,
    [Peso]                   DECIMAL (10, 2) NOT NULL,
    [CittaDestinataria]      NVARCHAR (100)  NOT NULL,
    [IndirizzoDestinatario]  NVARCHAR (255)  NOT NULL,
    [NominativoDestinatario] NVARCHAR (100)  NOT NULL,
    [Costo]                  DECIMAL (10, 2) NOT NULL,
    [DataConsegnaPrevista]   DATETIME2            NOT NULL,
    PRIMARY KEY CLUSTERED ([IdSpedizione] ASC),
    FOREIGN KEY ([IdCliente]) REFERENCES [dbo].[Clienti] ([IdCliente])
);

