using M_5_S_1.Models;
using M_5_S_1.Service;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks.Dataflow;

namespace M_5_S_1.Services
{
    public class SpedizioneService :    ISpedizioneService
    {
        private readonly SqlServerServiceBase _serviceBase;

        public SpedizioneService(SqlServerServiceBase serviceBase)
        {
            _serviceBase = serviceBase;
        }

        private const string VER_SPED = "SELECT ss.Stato, ss.Luogo, ss.Descrizione From StatiSpedizione AS ss " +
            "JOIN spedizioni AS s ON ss.IdSpedizione = s.IdSpedizione" +
            "JOIN Clienti AS c ON s.IdCliente = c.IdCliente" +
            "WHERE (c.CodiceFiscale = @CFOrPIVA  OR c.PartitaIVA = @CFOrPIVA) AND s.NumeroIdentificativo = @NumeroIdentificativo " +
            "ORDER BY ss.DataOraAggiornamento DESC"; 
       
        public IEnumerable <AggiornamentoSpedizione> VerificaAggiornamentoSpedizione(string CFOrPIVA, string NumeroIdentificativo) 
        {
        var AggiornamentoSpedizione = new List <AggiornamentoSpedizione>();
            using (var connection = _serviceBase.GetConnection())
            {
                var cmd = _serviceBase.GetCommand(VER_SPED);
                cmd.Parameters.Add(new SqlParameter("@CFOrPIVA", CFOrPIVA));
                cmd.Parameters.Add(new SqlParameter("@NumeroSpedizione", NumeroIdentificativo));
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
                            NomeDestinatario = reader.GetString(reader.GetOrdinal("NomeDestinatario")),
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
                var command = _serviceBase.GetCommand( "SELECT * FROM Spedizione WHERE IdSpedizione = @IdSpedizione");
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
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "INSERT INTO Spedizione (NumeroSpedizione, DataSpedizione, Peso," +
                    " CittaDestinatario, IndirizzoDestinatario, NomeDestinatario, CostoSpedizione, DataConsegnaPrevista," +
                    " ClienteId) VALUES (@NumeroSpedizione, @DataSpedizione, @Peso, @CittaDestinataria, @IndirizzoDestinatario," +
                    " @NomeDestinatario, @CostoSpedizione, @DataConsegnaPrevista, @ClienteId)");
                _serviceBase.AddParameter(command, "@NumeroIdentificativo", spedizione.NumeroIdentificativo);
                _serviceBase.AddParameter(command, "@DataSpedizione", spedizione.DataSpedizione);
                _serviceBase.AddParameter(command, "@Peso", spedizione.Peso);
                _serviceBase.AddParameter(command, "@CittaDestinataria", spedizione.CittaDestinataria);
                _serviceBase.AddParameter(command, "@IndirizzoDestinatario", spedizione.IndirizzoDestinatario);
                _serviceBase.AddParameter(command, "@NomeDestinatario", spedizione.NomeDestinatario);
                _serviceBase.AddParameter(command, "@CostoSpedizione", spedizione.Costo);
                _serviceBase.AddParameter(command, "@DataConsegnaPrevista", spedizione.DataConsegnaPrevista);
                _serviceBase.AddParameter(command, "@ClienteId", spedizione.ClienteId);
                command.ExecuteNonQuery();
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
            using (var connection = _serviceBase.GetConnection())
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
            return spedizioni;
        }

        public int GetNumeroSpedizioniTotali()
        {
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "SELECT COUNT(*) FROM Spedizioni");
                return (int)command.ExecuteScalar();
            }
        }

        public Dictionary<string, int> GetNumeroSpedizioniPerCitta()
        {
            var result = new Dictionary<string, int>();
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "SELECT CittaDestinataria, COUNT(*) FROM Spedizione GROUP BY CittaDestinataria");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetString(0), reader.GetInt32(1));
                    }
                }
            }
            return result;
        }
    }
}
