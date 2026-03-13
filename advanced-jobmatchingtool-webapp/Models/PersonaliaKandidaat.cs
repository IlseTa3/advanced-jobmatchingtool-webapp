using System.ComponentModel.DataAnnotations;

namespace advanced_jobmatchingtool_webapp.Models
{
    public class PersonaliaKandidaat
    {
        public int Id { get; set; }

        public string Gsmnr { get; set; }

        public string Telnr { get; set; }

        public string Postcode { get; set; }

        public string Stad {  get; set; }

        public string Land { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}
