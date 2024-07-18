SELECT ss.Stato, ss.Luogo, ss.Descrizione From StatiSpedizione AS ss 
            JOIN spedizioni AS s ON ss.IdSpedizione = s.IdSpedizione
            JOIN Clienti AS c ON s.IdCliente = c.IdCliente
           WHERE (c.CodiceFiscale = 'RSSMRA80A01H501Z'  OR c.PartitaIVA = 'RSSMRA80A01H501Z') AND s.NumeroIdentificativo = 'SP001' 
           ORDER BY ss.DataOraAggiornamento DESC