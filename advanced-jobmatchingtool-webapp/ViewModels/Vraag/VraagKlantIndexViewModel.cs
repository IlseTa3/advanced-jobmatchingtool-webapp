using advanced_jobmatchingtool_webapp.Models;
using System.ComponentModel.DataAnnotations;

namespace advanced_jobmatchingtool_webapp.ViewModels.Vraag
{
    public class VraagKlantIndexViewModel
    {
        public int Id { get; set; }
        public string VraagTekst { get; set; }
        public string Categorie { get; set; }
        public string SubCategorie { get; set; }
        [Display(Name = "Antwoordopties (gescheiden door komma's)")]
        public string AntwoordOptie { get; set; }

        public EnumSoortAntwoord SoortAntwoord { get; set; }

        public string SoortAntwoordWeergave => SoortAntwoord.ToString();
    }
}
