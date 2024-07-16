using M_5_S_1.Models;
using M_5_S_1.Service;


namespace M_5_S_1.Services
{
    public class AggiornamentoSpedizioneService : IAggiornamentoSpedizioneService
    {
        private readonly SqlServerServiceBase _serviceBase;

        public AggiornamentoSpedizioneService(SqlServerServiceBase serviceBase)
        {
            _serviceBase = serviceBase;
        }

        public IEnumerable<AggiornamentoSpedizione> GetAggiornamentiBySpedizioneId(int spedizioneId)
        {
            var aggiornamenti = new List<AggiornamentoSpedizione>();
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "SELECT * FROM AggiornamentoSpedizione WHERE SpedizioneId = @SpedizioneId ORDER BY DataOra DESC");
                _serviceBase.AddParameter(command, "@SpedizioneId", spedizioneId);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        aggiornamenti.Add(new AggiornamentoSpedizione
                        {
                            IdAggiornamento = reader.GetInt32(0),
                            SpedizioneId = reader.GetInt32(1),
                            Stato = reader.GetString(2),
                            Luogo = reader.GetString(3),
                            Descrizione = reader.GetString(4),
                            DataOra = reader.GetDateTime(5)
                        });
                    }
                }
            }
            return aggiornamenti;
        }

        public void AddAggiornamentoSpedizione(AggiornamentoSpedizione aggiornamento)
        {
            using (var connection = _serviceBase.GetConnection())
            {
                connection.Open();
                var command = _serviceBase.GetCommand( "INSERT INTO AggiornamentoSpedizione (SpedizioneId, Stato, Luogo, Descrizione, DataOra) VALUES (@SpedizioneId, @Stato, @Luogo, @Descrizione, @DataOra)");
                _serviceBase.AddParameter(command, "@SpedizioneId", aggiornamento.SpedizioneId);
                _serviceBase.AddParameter(command, "@Stato", aggiornamento.Stato);
                _serviceBase.AddParameter(command, "@Luogo", aggiornamento.Luogo);
                _serviceBase.AddParameter(command, "@Descrizione", aggiornamento.Descrizione);
                _serviceBase.AddParameter(command, "@DataOra", aggiornamento.DataOra);
                command.ExecuteNonQuery();
            }
        }
    }
}
