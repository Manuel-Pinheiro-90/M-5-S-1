using M_5_S_1.Models;
using M_5_S_1.Service;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace M_5_S_1.Services
{
    public class SpedizioneService : ISpedizioneService
    {
        private readonly SqlServerServiceBase _serviceBase;

        public SpedizioneService(SqlServerServiceBase serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public IEnumerable<Spedizione> GetAllSpedizioni()
        {
            var spedizioni = new List<Spedizione>();
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "SELECT * FROM Spedizione");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        spedizioni.Add(new Spedizione
                        {
                            IdSpedizione = reader.GetInt32(0),
                            NumeroSpedizione = reader.GetString(2),
                            DataSpedizione = reader.GetDateTime(3),
                            Peso = reader.GetDecimal(4),
                            CittaDestinatario = reader.GetString(5),
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
                            NumeroSpedizione = reader.GetString(2),
                            DataSpedizione = reader.GetDateTime(3),
                            Peso = reader.GetDecimal(4),
                            CittaDestinatario = reader.GetString(5),
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
                var command = _serviceBase.GetCommand( "SELECT * FROM Spedizione WHERE NumeroSpedizione = @NumeroSpedizione");
                _serviceBase.AddParameter(command, "@NumeroSpedizione", numeroSpedizione);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        spedizione = new Spedizione
                        {
                            IdSpedizione = reader.GetInt32(0),
                            NumeroSpedizione = reader.GetString(1),
                            DataSpedizione = reader.GetDateTime(2),
                            Peso = reader.GetDecimal(3),
                            CittaDestinatario = reader.GetString(4),
                            IndirizzoDestinatario = reader.GetString(5),
                            NomeDestinatario = reader.GetString(6),
                            Costo = reader.GetDecimal(7),
                            DataConsegnaPrevista = reader.GetDateTime(8),
                            ClienteId = reader.GetInt32(9)
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
                var command = _serviceBase.GetCommand( "INSERT INTO Spedizione (NumeroSpedizione, DataSpedizione, Peso, CittaDestinatario, IndirizzoDestinatario, NomeDestinatario, CostoSpedizione, DataConsegnaPrevista, ClienteId) VALUES (@NumeroSpedizione, @DataSpedizione, @Peso, @CittaDestinatario, @IndirizzoDestinatario, @NomeDestinatario, @CostoSpedizione, @DataConsegnaPrevista, @ClienteId)");
                _serviceBase.AddParameter(command, "@NumeroSpedizione", spedizione.NumeroSpedizione);
                _serviceBase.AddParameter(command, "@DataSpedizione", spedizione.DataSpedizione);
                _serviceBase.AddParameter(command, "@Peso", spedizione.Peso);
                _serviceBase.AddParameter(command, "@CittaDestinatario", spedizione.CittaDestinatario);
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
                var command = _serviceBase.GetCommand( "UPDATE Spedizione SET NumeroSpedizione = @NumeroSpedizione, DataSpedizione = @DataSpedizione, Peso = @Peso, CittaDestinatario = @CittaDestinatario, IndirizzoDestinatario = @IndirizzoDestinatario, NomeDestinatario = @NomeDestinatario, CostoSpedizione = @CostoSpedizione, DataConsegnaPrevista = @DataConsegnaPrevista, ClienteId = @ClienteId WHERE IdSpedizione = @IdSpedizione");
                _serviceBase.AddParameter(command, "@IdSpedizione", spedizione.IdSpedizione);
                _serviceBase.AddParameter(command, "@NumeroSpedizione", spedizione.NumeroSpedizione);
                _serviceBase.AddParameter(command, "@DataSpedizione", spedizione.DataSpedizione);
                _serviceBase.AddParameter(command, "@Peso", spedizione.Peso);
                _serviceBase.AddParameter(command, "@CittaDestinatario", spedizione.CittaDestinatario);
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
                            NumeroSpedizione = reader.GetString(2),
                            DataSpedizione = reader.GetDateTime(3),
                            Peso = reader.GetDecimal(4),
                            CittaDestinatario = reader.GetString(5),
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
                var command = _serviceBase.GetCommand( "SELECT CittaDestinatario, COUNT(*) FROM Spedizione GROUP BY CittaDestinatario");
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
