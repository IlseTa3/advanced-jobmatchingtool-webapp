using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace advanced_jobmatchingtool_webapp.ViewModels.Kandidaat
{
    public class KandidaatProfielViewModel
    {
        public PersonaliaKandidaatViewModel Personalia {  get; set; }
        public StatuutKandidaatViewModel Statuut { get; set; }

        //uit ApplicationUser

        //[Required]
        public string Voornaam { get; set; }
        //[Required]
        public string Familienaam {  get; set; }

        //[Required]
        public string Email { get; set; }
    }

    
}
