namespace advanced_jobmatchingtool_webapp.Models
{
    public class SoortAntwoord
    {
        public int Id { get; set; }
        public string NaamSoortAntwoord { get; set; }

        public ICollection<OptieAntwoord> AntwoordOpties { get; set; }
    }
}
