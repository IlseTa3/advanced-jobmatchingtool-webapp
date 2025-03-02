using Microsoft.AspNetCore.Identity;

namespace advanced_jobmatchingtool_webapp.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
       
        public string Role { get; set; }

        public bool? HeefIMWStatuut { get; set; }

        public string? IMWStatuutBestand { get; set; }
    }
}
