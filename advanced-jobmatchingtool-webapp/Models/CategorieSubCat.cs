using System.ComponentModel.DataAnnotations;

namespace advanced_jobmatchingtool_webapp.Models
{
    public class CategorieSubCat
    {
        public int Id { get; set; }
        [Display(Name = "Naam Categorie")]
        public string NaamCategorie { get; set; }
        [Display(Name = "Naam Subcategorie")]
        public string? NaamSubCategorie { get; set; }


        public ICollection<VraagKandidaat> VragenKandidaten { get; set; } = new List<VraagKandidaat>();
        public ICollection<VraagKlant> VragenKlanten { get; set; } = new List<VraagKlant>();
    }
}
