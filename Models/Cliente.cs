namespace M_5_S_1.Models
{
    public class Cliente
    {
        public int IdCliente { get; set; }
        public string Nome { get; set; }
        public string TipoCliente { get; set; } //le scelte sono 'Privato' o 'Azienda'
        public string CodiceFiscale { get; set; } // Valorizzato solo se TipoCliente è 'Privato'
        public string PartitaIVA { get; set; } // Valorizzato solo se TipoCliente è 'Azienda'
        public string Indirizzo { get; set; }
        public string Citta { get; set; }
        public string CAP { get; set; }
        public string Email { get; set; }

        public ICollection<Spedizione> Spedizioni { get; set; }




    }
}
