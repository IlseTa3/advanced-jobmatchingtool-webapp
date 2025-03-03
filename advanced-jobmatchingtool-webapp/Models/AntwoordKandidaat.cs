namespace advanced_jobmatchingtool_webapp.Models
{
    public class AntwoordKandidaat
    {
        public int Id { get; set; }
        public int VraagKandidaatId { get; set; }
        public string UserId { get; set; }

        public string AntwoordTekst { get; set; }

        public VraagKandidaat VraagKandidaat { get; set; }
        public ApplicationUser User { get; set; }
    }
}
