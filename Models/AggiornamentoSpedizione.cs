namespace M_5_S_1.Models
{
    public class AggiornamentoSpedizione
    {
        public int IdAggiornamento { get; set; }
        public int SpedizioneId { get; set; }
        public Spedizione Spedizione { get; set; }
        public string Stato { get; set; }  // "In Transito", "In Consegna", "Consegnato", "Non Consegnato"
        public string Luogo { get; set; }
        public string Descrizione { get; set; }
        public DateTime DataOra { get; set; }
    }
}
