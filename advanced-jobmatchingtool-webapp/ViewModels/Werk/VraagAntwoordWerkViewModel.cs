namespace advanced_jobmatchingtool_webapp.ViewModels.Werk
{
    public class VraagAntwoordWerkViewModel
    {
        public string VraagId { get; set; }
        public string UserId { get; set; }
        public string VraagText { get; set; }
        public string Type { get; set; }
        public List<string> Opties { get; set; }
        public string ExtraInformatie { get; set; }
        public string Antwoord { get; set; }
        public List<string> Antwoorden { get; set; }
        public string Categorie { get; set; }
    }
}
