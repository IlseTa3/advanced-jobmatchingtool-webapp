namespace advanced_jobmatchingtool_webapp.Models
{
    public class OptieAntwoord
    {
        public int Id { get; set; }
        public int SoortAntwoordId { get; set; }
        public string OptieTekst { get; set; }
        public int VragenlijstId { get; set; }

        public SoortAntwoord SoortAntwoord { get; set; }
        public Vragenlijst Vragenlijst { get; set; }
    }
}
