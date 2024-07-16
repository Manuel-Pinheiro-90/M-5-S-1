namespace M_5_S_1.Models
{
    public class Utente
    {
        public int IdUtente { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public List<string> Ruolo { get; set; } = [];
    }
}
