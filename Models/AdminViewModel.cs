namespace M_5_S_1.Models
{
    public class AdminViewModel
    {
        public IEnumerable<Spedizione> SpedizioniInConsegnaOggi { get; set; }
       public int NumeroSpedizioniTotali { get; set; }
        public Dictionary<string, int> NumeroSpedizioniPerCitta { get; set; }


    }
}
