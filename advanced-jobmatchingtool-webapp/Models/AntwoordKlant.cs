using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace advanced_jobmatchingtool_webapp.Models
{
    public class AntwoordKlant
    {
        public int Id { get; set; }
        public int VraagKlantId { get; set; }
        public string UserId { get; set; }
        public string AntwoordTekst { get; set; }
        public string? ExtraInfo { get; set; } //extra info voor beheerder in te vullen
        public string? Categorie { get; set; }
        public DateTime DatumIngevuld { get; set; } = DateTime.Now;


        [ValidateNever]
        public VraagKlant VraagKlant { get; set; }
        [ValidateNever]
        public ApplicationUser User { get; set; }
    }
}
