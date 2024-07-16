using M_5_S_1.Models;
using M_5_S_1.Service;


namespace M_5_S_1.Services
{
    public class ClienteService : IClienteService
    {
        private readonly SqlServerServiceBase _serviceBase;

        public ClienteService(SqlServerServiceBase serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public IEnumerable<Cliente> GetAllClienti()
        {
            var clienti = new List<Cliente>();
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "SELECT * FROM Clienti");
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        clienti.Add(new Cliente
                        {
                            IdCliente = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            TipoCliente = reader.GetString(2),
                            CodiceFiscale = reader.IsDBNull(3) ? null : reader.GetString(3),
                            PartitaIVA = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Indirizzo = reader.GetString(5),
                            Citta = reader.GetString(6),
                            CAP = reader.GetString(7),
                            Email = reader.GetString(8)
                        });
                    }
                }
            }
            return clienti;
        }

        public Cliente GetClienteById(int id)
        {
            Cliente cliente = null;
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "SELECT * FROM Clienti WHERE IdCliente = @IdCliente");
                _serviceBase.AddParameter(command, "@IdCliente", id);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cliente = new Cliente
                        {
                            IdCliente = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            TipoCliente = reader.GetString(2),
                            CodiceFiscale = reader.IsDBNull(3) ? null : reader.GetString(3),
                            PartitaIVA = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Indirizzo = reader.GetString(5),
                            Citta = reader.GetString(6),
                            CAP = reader.GetString(7),
                            Email = reader.GetString(8)
                        };
                    }
                }
            }
            return cliente;
        }

        public Cliente GetClienteByCodiceFiscale(string codiceFiscale)
        {
            Cliente cliente = null;
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "SELECT * FROM Clienti WHERE CodiceFiscale = @CodiceFiscale");
                _serviceBase.AddParameter(command, "@CodiceFiscale", codiceFiscale);
                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        cliente = new Cliente
                        {
                            IdCliente = reader.GetInt32(0),
                            Nome = reader.GetString(1),
                            TipoCliente = reader.GetString(2),
                            CodiceFiscale = reader.IsDBNull(3) ? null : reader.GetString(3),
                            PartitaIVA = reader.IsDBNull(4) ? null : reader.GetString(4),
                            Indirizzo = reader.GetString(5),
                            Citta = reader.GetString(6),
                            CAP = reader.GetString(7),
                            Email = reader.GetString(8)
                        };
                    }
                }
            }
            return cliente;
        }

        public void AddCliente(Cliente cliente)
        {
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "INSERT INTO Clienti (Nome, TipoCliente, CodiceFiscale, PartitaIVA, Indirizzo, Citta, CAP, Email) VALUES (@Nome, @TipoCliente, @CodiceFiscale, @PartitaIVA, @Indirizzo, @Citta, @CAP, @Email)");
                _serviceBase.AddParameter(command, "@Nome", cliente.Nome);
                _serviceBase.AddParameter(command, "@TipoCliente", cliente.TipoCliente);
                _serviceBase.AddParameter(command, "@CodiceFiscale", cliente.CodiceFiscale ?? (object)DBNull.Value);
                _serviceBase.AddParameter(command, "@PartitaIVA", cliente.PartitaIVA ?? (object)DBNull.Value);
                _serviceBase.AddParameter(command, "@Indirizzo", cliente.Indirizzo);
                _serviceBase.AddParameter(command, "@Citta", cliente.Citta);
                _serviceBase.AddParameter(command, "@CAP", cliente.CAP);
                _serviceBase.AddParameter(command, "@Email", cliente.Email);
                command.ExecuteNonQuery();
            }
        }

        public void UpdateCliente(Cliente cliente)
        {
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "UPDATE Clienti SET Nome = @Nome, TipoCliente = @TipoCliente, CodiceFiscale = @CodiceFiscale, PartitaIVA = @PartitaIVA, Indirizzo = @Indirizzo, Citta = @Citta, CAP = @CAP, Email = @Email WHERE IdCliente = @IdCliente");
                _serviceBase.AddParameter(command, "@IdCliente", cliente.IdCliente);
                _serviceBase.AddParameter(command, "@Nome", cliente.Nome);
                _serviceBase.AddParameter(command, "@TipoCliente", cliente.TipoCliente);
                _serviceBase.AddParameter(command, "@CodiceFiscale", cliente.CodiceFiscale ?? (object)DBNull.Value);
                _serviceBase.AddParameter(command, "@PartitaIVA", cliente.PartitaIVA ?? (object)DBNull.Value);
                _serviceBase.AddParameter(command, "@Indirizzo", cliente.Indirizzo);
                _serviceBase.AddParameter(command, "@Citta", cliente.Citta);
                _serviceBase.AddParameter(command, "@CAP", cliente.CAP);
                _serviceBase.AddParameter(command, "@Email", cliente.Email);
                command.ExecuteNonQuery();
            }
        }

        public void DeleteCliente(int id)
        {
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "DELETE FROM Clienti WHERE IdCliente = @IdCliente");
                _serviceBase.AddParameter(command, "@IdCliente", id);
                command.ExecuteNonQuery();
            }
        }
    }
}
