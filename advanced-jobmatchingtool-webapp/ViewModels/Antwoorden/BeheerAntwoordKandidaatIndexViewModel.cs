using System.ComponentModel.DataAnnotations;

namespace advanced_jobmatchingtool_webapp.ViewModels.Antwoorden
{
    public class BeheerAntwoordKandidaatIndexViewModel
    {
        public int Id { get; set; }

        [Display(Name ="Voornaam")]
        public string UserVoornaam { get; set; }

        [Display(Name = "Familienaam")]
        public string UserFamilienaam { get; set; }

        [Display(Name = "Vraag")]
        public string VraagTekst { get; set; }

        [Display(Name = "Antwoord")]
        public string AntwoordTekst { get; set; }

        public string Categorie { get; set; }

        [Display(Name = "Extra info")]
        public string ExtraInfo { get; set; }
    }
}
