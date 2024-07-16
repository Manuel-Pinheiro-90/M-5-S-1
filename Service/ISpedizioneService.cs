using M_5_S_1.Models;
using System.Collections.Generic;

namespace M_5_S_1.Services
{
    public interface ISpedizioneService
    {
        IEnumerable<Spedizione> GetAllSpedizioni();
        Spedizione GetSpedizioneById(int id);
        Spedizione GetSpedizioneByNumeroSpedizione(string numeroSpedizione);
        void AddSpedizione(Spedizione spedizione);
        void UpdateSpedizione(Spedizione spedizione);
        void DeleteSpedizione(int id);
        IEnumerable<Spedizione> GetSpedizioniInConsegnaOggi();
        int GetNumeroSpedizioniTotali();
        Dictionary<string, int> GetNumeroSpedizioniPerCitta();
    }
}
