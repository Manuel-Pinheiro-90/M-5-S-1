namespace M_5_S_1.Models
{
    public class Spedizione
    {
        public int IdSpedizione { get; set; }
        public Cliente Cliente { get; set; }
        public int ClienteId { get; set; }
        public string NumeroSpedizione { get; set; }
        public DateTime DataSpedizione { get; set; }
        public decimal Peso { get; set; }
        public string CittaDestinatario { get; set; }
        public string IndirizzoDestinatario { get; set; }
        public string NomeDestinatario { get; set; }
        public decimal Costo { get; set; }
        public DateTime DataConsegnaPrevista { get; set; }
        public ICollection<AggiornamentoSpedizione> Aggiornamenti { get; set; } = new List<AggiornamentoSpedizione>();
    }
}

