using System.ComponentModel.DataAnnotations;

namespace advanced_jobmatchingtool_webapp.ViewModels.CatSubCats
{
    public class CatSubCatIndexViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Naam Categorie")]
        public string NaamCategorie { get; set; }

        [Display(Name = "Naam Subcategorie")]
        public string NaamSubCategorie { get; set; }
    }
}
