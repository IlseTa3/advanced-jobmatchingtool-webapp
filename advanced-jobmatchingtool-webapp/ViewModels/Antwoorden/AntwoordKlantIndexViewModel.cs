namespace advanced_jobmatchingtool_webapp.ViewModels.Antwoorden
{
    public class AntwoordKlantIndexViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public List<VraagAntwoordKlantViewModel> VragenAntwoorden { get; set; }
    }
}
