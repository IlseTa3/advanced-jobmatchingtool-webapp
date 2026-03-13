using Microsoft.AspNetCore.Identity;

namespace advanced_jobmatchingtool_webapp.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        
        public bool ProfileComplete { get; set; }
        public bool TermsCond {  get; set; }

        public string Role { get; set; }


        //Terms and Conditions bool

        //Navigatie naar statuut en personalia
        public virtual StatuutKandidaat Statuut {  get; set; }
        public virtual PersonaliaKandidaat Personalia {  get; set; }

        //public bool? HeefIMWStatuut { get; set; }

        //public string? IMWStatuutBestand { get; set; }
    }
}
