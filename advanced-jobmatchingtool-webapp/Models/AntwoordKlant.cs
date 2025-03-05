namespace advanced_jobmatchingtool_webapp.Models
{
    public class AntwoordKlant
    {
        public int Id { get; set; }
        public int VraagKlantId { get; set; }
        public string UserId { get; set; }
        public string AntwoordTekst { get; set; }
        public string? ExtraInfo { get; set; } //extra info voor beheerder in te vullen
        

        public VraagKlant VraagKlant { get; set; }
        public ApplicationUser User { get; set; }
    }
}
