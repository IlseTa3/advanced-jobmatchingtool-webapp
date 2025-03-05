namespace advanced_jobmatchingtool_webapp.ViewModels.Antwoorden
{
    public class AntwoordKandidaatIndexViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam {  get; set; }
        public List<VraagAntwoordKandidaatViewModel> VragenAntwoorden { get; set; }
        
    }
}
