using advanced_jobmatchingtool_webapp.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace advanced_jobmatchingtool_webapp.ViewModels.Kandidaat
{
    public class StatuutKandidaatViewModel
    {

        [Display(Name = "Heb je een diagnose?")]

        public DiagnoseOpties Diagnose {  get; set; }

        [Display(Name = "Heb je een statuut Individueel Maatwerk?")]

        public bool HeeftIMWStatuut { get; set; }

        [Display(Name = "Upload bewijs van Individueel Maatwerk (PDF, DOC, JPG, PNG)")]

        public List<IFormFile> IMWBestanden { get; set; }

        [Display(Name = "Heb je graag hulp nodig bij het invullen?")]

        public bool HulpNodigBijInvullen { get; set; }

        //Optioneel
        [ValidateNever]
        public List<string> BestaandeBestanden { get; set; } = new List<string>();
    }
}