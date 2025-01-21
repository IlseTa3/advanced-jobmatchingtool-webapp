namespace advanced_jobmatchingtool_webapp.ViewModels.Werk
{
    public class WerkViewModel
    {
        public string UserId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public List<VraagAntwoordWerkViewModel> VragenAntwoorden { get; set; }
    }
}
