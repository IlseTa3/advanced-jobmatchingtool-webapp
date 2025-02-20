using System.Collections.ObjectModel;

namespace advanced_jobmatchingtool_webapp.Models
{
    public class Vraag
    {
        public int Id { get; set; }
        public string VraagText { get; set; }
        public int CategorieId { get; set; }
        public int? SubCategorieId { get; set; }
        public EnumSoortAntwoord SoortAntwoord { get; set; }
        public decimal GewichtsScore { get; set; }


        public Categorie Categorie { get; set; }
        public SubCategorie SubCategorie { get; set; }

        public ICollection<AntwoordOptie> AntwoordOpties { get; set; } = new Collection<AntwoordOptie>();
    }
}
