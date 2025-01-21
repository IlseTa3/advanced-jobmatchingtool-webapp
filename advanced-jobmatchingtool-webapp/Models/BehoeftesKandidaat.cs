namespace advanced_jobmatchingtool_webapp.Models
{
    public class BehoeftesKandidaat
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string VraagId { get; set; }
        public string? Antwoord { get; set; }
        public string Categorie { get; set; }
        public DateTime DatumIngevuld { get; set; } = DateTime.Now;
    }
}
