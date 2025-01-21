namespace advanced_jobmatchingtool_webapp.ViewModels.Triggers
{
    public class TriggersViewModel
    {
        public string UserId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public List<VraagAntwoordTriggersViewModel> VragenAntwoorden { get; set; }
    }
}
