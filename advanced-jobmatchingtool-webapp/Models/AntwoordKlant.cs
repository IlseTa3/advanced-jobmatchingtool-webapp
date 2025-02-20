namespace advanced_jobmatchingtool_webapp.Models
{
    public class AntwoordKlant
    {
        public int Id { get; set; }
        public int VragenlijstId { get; set; }
        public string UserId { get; set; }
        public string Antwoord { get; set; }
        public decimal GewichtsScore { get; set; }

        public Vraag Vragenlijst { get; set; }
        public ApplicationUser User { get; set; }
    }
}
