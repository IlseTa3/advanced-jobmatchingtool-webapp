namespace advanced_jobmatchingtool_webapp.Models
{
    public class AntwoordOptie
    {
        public int Id { get; set; }
        public string OptieTekst { get; set; }

        public ICollection<VraagKandidaat> VragenKandidaten { get; set; } = new List<VraagKandidaat>();
        public ICollection<VraagKlant> VragenKlanten { get; set; } = new List<VraagKlant>();
    }
}
