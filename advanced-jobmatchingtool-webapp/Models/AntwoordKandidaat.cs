namespace advanced_jobmatchingtool_webapp.Models
{
    public class AntwoordKandidaat
    {
        public int Id { get; set; }
        public int VragenlijstId { get; set; }
        public string UserId { get; set; }

        public string Antwoord { get; set; }

        public Vraag Vragenlijst { get; set; }
        public ApplicationUser User { get; set; }
    }
}
