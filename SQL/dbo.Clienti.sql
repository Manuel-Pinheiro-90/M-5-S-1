CREATE TABLE [dbo].[Clienti] (
    [IdCliente]     INT            IDENTITY (1, 1) NOT NULL,
    [Nome]          NVARCHAR (100) NOT NULL,
    [TipoCliente]   NVARCHAR (50)  NOT NULL,
    [CodiceFiscale] NVARCHAR (16)  NULL,
    [PartitaIVA]    NVARCHAR (11)  NULL,
    [Indirizzo]     NVARCHAR (255) NOT NULL,
    [Citta]         NVARCHAR (100) NOT NULL,
    [CAP]           NVARCHAR (5)   NOT NULL,
    [Email]         NVARCHAR (255) NOT NULL,
    PRIMARY KEY CLUSTERED ([IdCliente] ASC),
    CONSTRAINT [CK_Cliente_CodiceFiscalePartitaIVA] CHECK ([TipoCliente]='Privato' AND [CodiceFiscale] IS NOT NULL AND [PartitaIVA] IS NULL OR [TipoCliente]='Azienda' AND [PartitaIVA] IS NOT NULL AND [CodiceFiscale] IS NULL),
    CHECK ([TipoCliente]='Azienda' OR [TipoCliente]='Privato')
);

