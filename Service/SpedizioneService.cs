using M_5_S_1.Models;
using M_5_S_1.Service;
using System.Data.SqlClient;


namespace M_5_S_1.Services
{
    public class SpedizioneService :    ISpedizioneService
    {
        private readonly SqlServerServiceBase _serviceBase;
        private readonly ILogger<SpedizioneService> _logger;
        public SpedizioneService(SqlServerServiceBase serviceBase, ILogger<SpedizioneService> logger)
        {
            _serviceBase = serviceBase;
            _logger = logger;
        }




        /// /////////////////////Verifica aggiornamenti spedizione in base a codici forniti ///////////////////////////////////////////////

        private const string VER_SPED = "SELECT ss.Stato, ss.Luogo, ss.Descrizione, ss.DataOraAggiornamento FROM StatiSpedizione AS ss " +
            "JOIN Spedizioni AS s ON ss.IdSpedizione = s.IdSpedizione " +
            "JOIN Clienti AS c ON s.IdCliente = c.IdCliente " +
            "WHERE (c.CodiceFiscale = @CFOrPIVA OR c.PartitaIVA = @CFOrPIVA) AND s.NumeroIdentificativo = @NumeroIdentificativo " +
            "ORDER BY ss.DataOraAggiornamento DESC";

        public IEnumerable <AggiornamentoSpedizione> VerificaAggiornamentoSpedizione(string CFOrPIVA, string NumeroIdentificativo) 
        {
        var AggiornamentoSpedizione = new List <AggiornamentoSpedizione>();
            using (var connection = _serviceBase.GetConnection())
            {
                var cmd = _serviceBase.GetCommand(VER_SPED);
                cmd.Parameters.Add(new SqlParameter("@CFOrPIVA", CFOrPIVA));
                cmd.Parameters.Add(new SqlParameter("@NumeroIdentificativo", NumeroIdentificativo));
                connection.Open();
                using var reader = cmd.ExecuteReader();
                ; while (reader.Read()) {
                    AggiornamentoSpedizione.Add(new AggiornamentoSpedizione
                    {
                        
                       Stato =reader.GetString(0),  
                       Luogo =reader.GetString(1),
                       Descrizione =reader.GetString(2),
                         
      


    });
                }

                return AggiornamentoSpedizione;

            }






        }

        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////




        public IEnumerable<Spedizione> GetAllSpedizioni()
        {
            var spedizioni = new List<Spedizione>();
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand(@"
            SELECT 
                s.*, 
                c.IdCliente, c.Nome, c.TipoCliente, c.CodiceFiscale, c.PartitaIVA, c.Indirizzo, c.Citta, c.CAP, c.Email 
            FROM 
                Spedizioni s
                INNER JOIN Clienti c ON s.IdCliente = c.IdCliente");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var spedizione = new Spedizione
                        {
                            IdSpedizione = reader.GetInt32(reader.GetOrdinal("IdSpedizione")),
                            NumeroIdentificativo = reader.GetString(reader.GetOrdinal("NumeroIdentificativo")),
                            DataSpedizione = reader.GetDateTime(reader.GetOrdinal("DataSpedizione")),
                            Peso = reader.GetDecimal(reader.GetOrdinal("Peso")),
                            CittaDestinataria = reader.GetString(reader.GetOrdinal("CittaDestinataria")),
                            IndirizzoDestinatario = reader.GetString(reader.GetOrdinal("IndirizzoDestinatario")),
                           NomeDestinatario = reader.GetString(reader.GetOrdinal("NominativoDestinatario")),
                            Costo = reader.GetDecimal(reader.GetOrdinal("Costo")),
                            DataConsegnaPrevista = reader.GetDateTime(reader.GetOrdinal("DataConsegnaPrevista")),
                            ClienteId = reader.GetInt32(reader.GetOrdinal("IdCliente")),
                            Cliente = new Cliente
                            {
                                IdCliente = reader.GetInt32(reader.GetOrdinal("IdCliente")),
                                Nome = reader.GetString(reader.GetOrdinal("Nome")),
                                TipoCliente = reader.GetString(reader.GetOrdinal("TipoCliente")),
                                CodiceFiscale = reader.IsDBNull(reader.GetOrdinal("CodiceFiscale")) ? null : reader.GetString(reader.GetOrdinal("CodiceFiscale")),
                                PartitaIVA = reader.IsDBNull(reader.GetOrdinal("PartitaIVA")) ? null : reader.GetString(reader.GetOrdinal("PartitaIVA")),
                                Indirizzo = reader.GetString(reader.GetOrdinal("Indirizzo")),
                                Citta = reader.GetString(reader.GetOrdinal("Citta")),
                                CAP = reader.GetString(reader.GetOrdinal("CAP")),
                                Email = reader.GetString(reader.GetOrdinal("Email"))
                            }
                        };
                        spedizioni.Add(spedizione);
                    }
                }
            }
            return spedizioni;
        }

        public Spedizione GetSpedizioneById(int id)
        {
            Spedizione spedizione = null;
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "SELECT * FROM Spedizioni WHERE IdSpedizione = @IdSpedizione");
                _serviceBase.AddParameter(command, "@IdSpedizione", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        spedizione = new Spedizione
                        {
                            IdSpedizione = reader.GetInt32(0),
                            NumeroIdentificativo = reader.GetString(2),
                            DataSpedizione = reader.GetDateTime(3),
                            Peso = reader.GetDecimal(4),
                            CittaDestinataria = reader.GetString(5),
                            IndirizzoDestinatario = reader.GetString(6),
                            NomeDestinatario = reader.GetString(7),
                            Costo = reader.GetDecimal(8),
                            DataConsegnaPrevista = reader.GetDateTime(9),
                            ClienteId = reader.GetInt32(1)
                        };
                    }
                }
            }
            return spedizione;
        }

        public Spedizione GetSpedizioneByNumeroSpedizione(string numeroSpedizione)
        {
            Spedizione spedizione = null;
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "SELECT * FROM dbo.Spedizioni WHERE NumeroIdentificativo = @NumeroIdentificativo");
                _serviceBase.AddParameter(command, "@NumeroIdentificativo", numeroSpedizione);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        spedizione = new Spedizione
                        {
                            IdSpedizione = reader.GetInt32(reader.GetOrdinal("IdSpedizione")),
                            NumeroIdentificativo = reader.GetString(reader.GetOrdinal("NumeroIdentificativo")),
                            DataSpedizione = reader.GetDateTime(reader.GetOrdinal("DataSpedizione")),
                            Peso = reader.GetDecimal(reader.GetOrdinal("Peso")),
                            CittaDestinataria = reader.GetString(reader.GetOrdinal("CittaDestinatario")),
                            IndirizzoDestinatario = reader.GetString(reader.GetOrdinal("IndirizzoDestinatario")),
                            NomeDestinatario = reader.GetString(reader.GetOrdinal("NomeDestinatario")),
                            Costo = reader.GetDecimal(reader.GetOrdinal("CostoSpedizione")),
                            DataConsegnaPrevista = reader.GetDateTime(reader.GetOrdinal("DataConsegnaPrevista")),
                            ClienteId = reader.GetInt32(reader.GetOrdinal("ClienteId"))
                        };
                    }
                }
            }
            return spedizione;
        }

        public void AddSpedizione(Spedizione spedizione)
        {
            var connection = _serviceBase.GetConnection();
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "INSERT INTO Spedizioni (NumeroIdentificativo, DataSpedizione, Peso," +
                    " CittaDestinataria, IndirizzoDestinatario, NominativoDestinatario, Costo, DataConsegnaPrevista," +
                    "IdCliente) VALUES (@NumeroIdentificativo, @DataSpedizione, @Peso, @CittaDestinataria, @IndirizzoDestinatario," +
                    " @NominativoDestinatario, @Costo, @DataConsegnaPrevista, @IdCliente)");
                command.Parameters.Add(new SqlParameter ("@NumeroIdentificativo", spedizione.NumeroIdentificativo));
                command.Parameters.Add(new SqlParameter ("@DataSpedizione", spedizione.DataSpedizione));
                command.Parameters.Add(new SqlParameter ("@Peso", spedizione.Peso));
                command.Parameters.Add(new SqlParameter ("@CittaDestinataria", spedizione.CittaDestinataria));
                command.Parameters.Add(new SqlParameter ("@IndirizzoDestinatario", spedizione.IndirizzoDestinatario));
                command.Parameters.Add(new SqlParameter ("@NominativoDestinatario", spedizione.NomeDestinatario));
                command.Parameters.Add(new SqlParameter ("@Costo", spedizione.Costo));
                command.Parameters.Add(new SqlParameter ("@DataConsegnaPrevista", spedizione.DataConsegnaPrevista));
                command.Parameters.Add(new SqlParameter ("@IdCliente", spedizione.ClienteId));
                command.ExecuteNonQuery();
                var result = command.ExecuteScalar();
                if (result == null)
                    throw new Exception("Creazione non completata");


            }
        }

        public void UpdateSpedizione(Spedizione spedizione)
        {
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand("UPDATE Spedizione SET NumeroSpedizione = @NumeroIdentificativo," +
                    " DataSpedizione = @DataSpedizione, Peso = @Peso, " +
                    "CittaDestinataria = @CittaDestinataria," +
                    " IndirizzoDestinatario = @IndirizzoDestinatario," +
                    " NomeDestinatario = @NomeDestinatario, CostoSpedizione = @CostoSpedizione," +
                    " DataConsegnaPrevista = @DataConsegnaPrevista, ClienteId = @ClienteId WHERE IdSpedizione = @IdSpedizione");
                _serviceBase.AddParameter(command, "@IdSpedizione", spedizione.IdSpedizione);
                _serviceBase.AddParameter(command, "@NumeroIdentificativo", spedizione.NumeroIdentificativo);
                _serviceBase.AddParameter(command, "@DataSpedizione", spedizione.DataSpedizione);
                _serviceBase.AddParameter(command, "@Peso", spedizione.Peso);
                _serviceBase.AddParameter(command, "@CittaDestinatario", spedizione.CittaDestinataria);
                _serviceBase.AddParameter(command, "@IndirizzoDestinatario", spedizione.IndirizzoDestinatario);
                _serviceBase.AddParameter(command, "@NomeDestinatario", spedizione.NomeDestinatario);
                _serviceBase.AddParameter(command, "@CostoSpedizione", spedizione.Costo);
                _serviceBase.AddParameter(command, "@DataConsegnaPrevista", spedizione.DataConsegnaPrevista);
                _serviceBase.AddParameter(command, "@ClienteId", spedizione.ClienteId);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteSpedizione(int id)
        {
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "DELETE FROM Spedizioni WHERE IdSpedizione = @IdSpedizione");
                _serviceBase.AddParameter(command, "@IdSpedizione", id);
                command.ExecuteNonQuery();
            }
        }




        public IEnumerable<Spedizione> GetSpedizioniInConsegnaOggi()
        {
            var spedizioni = new List<Spedizione>();
            var connection = _serviceBase.GetConnection();
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "SELECT * FROM Spedizioni WHERE DataConsegnaPrevista = CAST(GETDATE() AS DATE)");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        spedizioni.Add(new Spedizione
                        {
                            IdSpedizione = reader.GetInt32(0),
                            NumeroIdentificativo = reader.GetString(2),
                            DataSpedizione = reader.GetDateTime(3),
                            Peso = reader.GetDecimal(4),
                            CittaDestinataria = reader.GetString(5),
                            IndirizzoDestinatario = reader.GetString(6),
                            NomeDestinatario = reader.GetString(7),
                            Costo = reader.GetDecimal(8),
                            DataConsegnaPrevista = reader.GetDateTime(9),
                            ClienteId = reader.GetInt32(1)
                        });
                    }
                }
            }
            connection.Close();
            return spedizioni;
        }

        public int GetNumeroSpedizioniTotali()
        {
            var connection = _serviceBase.GetConnection();
            try
            {
                

                connection.Open();
                var command = _serviceBase.GetCommand("SELECT COUNT(*) FROM Spedizioni");
                command.Connection = connection; 

                
                return (int)command.ExecuteScalar();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il conteggio delle spedizioni totali.");


                throw;
            }
            finally {connection.Close(); }
        
        }


        public Dictionary<string, int> GetNumeroSpedizioniPerCitta()
        {
            var result = new Dictionary<string, int>();
            var connection = _serviceBase.GetConnection();
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "SELECT CittaDestinataria, COUNT(*) FROM Spedizioni GROUP BY CittaDestinataria");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetString(0), reader.GetInt32(1));
                    }
                }
            }
            connection.Close();
            return result;
        }
    }
}
