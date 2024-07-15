namespace M_5_S_1.Models
{
    public class AggiornamentoSpedizione
    {
        public int IdAggiornamento { get; set; }
        public int IdSpedizione { get; set; }
        public string Stato { get; set; } //poi inserirò questi 4 stati  'In transito', 'In consegna', 'Consegnato', 'Non consegnato'
        public string Luogo { get; set; }
        public string Descrizione { get; set; }
        public DateTime DataOra { get; set; }

        public Spedizione Spedizione { get; set; }
    }
}
