namespace advanced_jobmatchingtool_webapp.ViewModels.Stress
{
    public class StressViewModel
    {
        public string UserId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public List<VraagAntwoordStressViewModel> VragenAntwoorden { get; set; }
    }
}
