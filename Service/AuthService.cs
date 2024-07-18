
using M_5_S_1.Models;
using System.Data.SqlClient;

namespace M_5_S_1.Service
{
    public class AuthService : IAuthService
    {
        private string connectionString;
        private const string LOGIN_COMMAND = "SELECT IdUtente, Username, PasswordHash FROM dbo.Utenti WHERE Username = @username AND PasswordHash = @password";
        private const string ROLES_COMMAND = "SELECT Nome FROM Ruoli ro JOIN UtentiRuoli ur ON ro.IdRuolo = ur.IdRuolo WHERE IdUtente =@id";

        public AuthService(IConfiguration config)
        {
            connectionString = config.GetConnectionString("CON")!;
        }

        public Utente Login(string username, string password)
        {
            try
            {
                 var conn = new SqlConnection(connectionString);
                conn.Open();
                using var cmd = new SqlCommand(LOGIN_COMMAND, conn);
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var utente = new Utente
                    {
                        IdUtente = reader.GetInt32(0),
                        Username = username,
                        PasswordHash = password,
                       
                    };
                    reader.Close();
                    using var roleCmd = new SqlCommand(ROLES_COMMAND, conn);
                    roleCmd.Parameters.AddWithValue("@id", utente.IdUtente);
                    using var re = roleCmd.ExecuteReader();
                    while (re.Read()) { utente.Ruolo.Add(re.GetString(0)); }    
                    return utente;
                }
            }
            catch (Exception ex)
            {
               
                throw new Exception("Errore durante il login.", ex);
            }
            return null;



        }
    }
}