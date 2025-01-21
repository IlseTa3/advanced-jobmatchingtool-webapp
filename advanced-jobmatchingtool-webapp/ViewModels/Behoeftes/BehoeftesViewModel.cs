namespace advanced_jobmatchingtool_webapp.ViewModels.Behoeftes
{
    public class BehoeftesViewModel
    {
        public string UserId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public List<VraagAntwoordBehoeftesViewModel> VragenAntwoorden { get; set; }
    }
}
