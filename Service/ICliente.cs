using M_5_S_1.Models;
using System.Collections.Generic;

namespace M_5_S_1.Services
{
    public interface IClienteService
    {
        IEnumerable<Cliente> GetAllClienti();
        Cliente GetClienteById(int id);
        Cliente GetClienteByCodiceFiscale(string codiceFiscale);
        void AddCliente(Cliente cliente);
        void UpdateCliente(Cliente cliente);
        void DeleteCliente(int id);
    }
}
