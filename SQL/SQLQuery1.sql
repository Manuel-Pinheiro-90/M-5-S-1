-- Spedizioni in consegna nella data odierna
SELECT * FROM Spedizioni
WHERE DataConsegnaPrevista = CAST(GETDATE() AS DATE);

-- Numero totale delle spedizioni in attesa di consegna
SELECT COUNT(*) AS NumeroSpedizioniInAttesa
FROM Spedizioni
WHERE DataConsegnaPrevista > CAST(GETDATE() AS DATE);

-- Numero totale delle spedizioni per città destinataria
SELECT CittaDestinataria, COUNT(*) AS NumeroSpedizioni
FROM Spedizioni
GROUP BY CittaDestinataria;

--quesry basata su codice fiscale cliente
DECLARE @CodiceFiscale NVARCHAR(16) = 'RSSMRA80A01H501Z';  
DECLARE @NumeroIdentificativo NVARCHAR(50) = 'SP001';  
SELECT s.NumeroIdentificativo, s.DataSpedizione, s.Peso, s.CittaDestinataria, s.IndirizzoDestinatario, s.NominativoDestinatario, s.Costo, s.DataConsegnaPrevista, st.Stato, st.Luogo, st.Descrizione, st.DataOraAggiornamento
FROM Spedizioni s
JOIN StatiSpedizione st ON s.IdSpedizione = st.IdSpedizione
JOIN Clienti c ON s.IdCliente = c.IdCliente
WHERE (c.CodiceFiscale = @CodiceFiscale OR c.PartitaIVA = @CodiceFiscale) AND s.NumeroIdentificativo = @NumeroIdentificativo
ORDER BY st.DataOraAggiornamento DESC;