namespace M_5_S_1.Models
{
    public class Spedizione
    {
        public int IdSpedizione { get; set; }
        public int IdCliente { get; set; }
        public string NumeroIdentificativo { get; set; }
        public DateTime DataSpedizione { get; set; }
        public float Peso { get; set; }
        public string CittaDestinataria { get; set; }
        public string IndirizzoDestinatario { get; set; }
        public string NominativoDestinatario { get; set; }
        public decimal Costo { get; set; }
        public DateTime DataConsegnaPrevista { get; set; }

        public Cliente Cliente { get; set; }
        public ICollection<AggiornamentoSpedizione> Aggiornamenti { get; set; }



    }
}
