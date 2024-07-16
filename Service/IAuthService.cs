using M_5_S_1.Models;

namespace M_5_S_1.Service
{
    public interface IAuthService
    {
        Utente Login(String username, String password);
    }
}
