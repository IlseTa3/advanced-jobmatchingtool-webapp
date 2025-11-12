using System.ComponentModel.DataAnnotations;

namespace advanced_jobmatchingtool_webapp.ViewModels.Kandidaat
{
    public class PersonaliaKandidaatViewModel
    {

        [Display(Name = "Gsmnummer")]
        public string Gsmnr { get; set; }

        [Display(Name = "Telefoonnummer")]
        public string Telnr { get; set; }

        public string Postcode { get; set; }

        [Display(Name = "Stad of Gemeente")]
        public string Stad {  get; set; }

        public string Land { get; set; }
    }
}