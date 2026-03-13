using System.ComponentModel.DataAnnotations;

namespace advanced_jobmatchingtool_webapp.Models
{
    public class Prospect
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Naam onderneming")]
        public string NaamOnderneming {  get; set; }

        [Required]
        [Display(Name = "Naam contactpersoon binnen de onderneming")]
        public string NaamContactpersoon { get; set; }

        [Required]
        [Display(Name = "btw nummer")]
        public string Btwnr {  get; set; }

        [Required]
        public string Adres { get; set; }

        [Required]
        public string Postcode { get; set; }

        [Required]
        [Display(Name = "Stad/Gemeente")]
        public string Stad {  get; set; }

        [Required]
        public string Land { get; set; }

        [Display(Name = "Telefoonnummer")]
        public string Telefoonnr { get; set; }

        [Display(Name = "Gsmnummer")]
        public string Gsmnr { get; set; }

        [Required]
        [Display(Name = "E-mailadres")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Omschrijving van de Onderneming")]
        public string OmschrijvingOnderneming { get; set; }

        [Required]
        [Display(Name = "Ik ga akkoord met de algemene voorwaarden, zie link")]
        public bool TermsCond {  get; set; }

    }
}
