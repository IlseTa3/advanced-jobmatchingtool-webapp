using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.Collections.ObjectModel;

namespace advanced_jobmatchingtool_webapp.Models
{
    public class VraagKandidaat
    {
        public int Id { get; set; }
        public string VraagText { get; set; }
        public int CategorieSubCatId { get; set; }
        public int? AntwoordOptieId { get; set; }
        public EnumSoortAntwoord SoortAntwoord { get; set; }

        [ValidateNever]
        public CategorieSubCat Categorie { get; set; }
        [ValidateNever]
        public AntwoordOptie AntwoordOptie { get; set; }

        [ValidateNever]
        public ICollection<AntwoordKandidaat> AntwoordenKandidaten { get; set; }
    }
}
