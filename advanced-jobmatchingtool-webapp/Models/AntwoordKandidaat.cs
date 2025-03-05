using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace advanced_jobmatchingtool_webapp.Models
{
    public class AntwoordKandidaat
    {
        public int Id { get; set; }
        [ValidateNever]
        public int VraagKandidaatId { get; set; }
        [ValidateNever]
        public string UserId { get; set; }

        [ValidateNever]
        public string AntwoordTekst { get; set; }
        [ValidateNever]
        public string? ExtraInfo { get; set; } //extra info voor beheerder in te vullen

        public string? Categorie { get; set; }

        public DateTime DatumIngevuld { get; set; } = DateTime.Now;

        [ValidateNever]
        public VraagKandidaat VraagKandidaat { get; set; }
        [ValidateNever]
        public ApplicationUser User { get; set; }
    }
}
