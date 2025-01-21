namespace advanced_jobmatchingtool_webapp.ViewModels.Personalia
{
    public class PersonaliaViewModel
    {
        public string UserId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public List<VraagAntwoordPersonaliaViewModel> VragenAntwoorden { get; set; }
    }
}
