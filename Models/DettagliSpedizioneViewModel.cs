namespace M_5_S_1.Models
{
    public class DettagliSpedizioneViewModel
    {
        public Spedizione Spedizione { get; set; }
        public IEnumerable<AggiornamentoSpedizione> Aggiornamenti { get; set; }
    }
}