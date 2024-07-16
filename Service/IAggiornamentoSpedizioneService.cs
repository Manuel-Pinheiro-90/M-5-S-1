using M_5_S_1.Models;

namespace M_5_S_1.Services
{
    public interface IAggiornamentoSpedizioneService
    {
        IEnumerable<AggiornamentoSpedizione> GetAggiornamentiBySpedizioneId(int spedizioneId);
        void AddAggiornamentoSpedizione(AggiornamentoSpedizione aggiornamento);
    }
}
