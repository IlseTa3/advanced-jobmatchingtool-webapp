namespace advanced_jobmatchingtool_webapp.ViewModels.Studies
{
    public class StudiesViewModel
    {
        public string UserId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public List<VraagAntwoordStudiesViewModel> VragenAntwoorden { get; set; }
    }
}
