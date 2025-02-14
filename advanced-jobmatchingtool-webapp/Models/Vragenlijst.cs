namespace advanced_jobmatchingtool_webapp.Models
{
    public class Vragenlijst
    {
        public int Id { get; set; }
        public string VraagText { get; set; }
        public int CategorieId { get; set; }
        public int? SubCategorieId { get; set; }
        public int? SoortAntwoordId { get; set; }
        public decimal GewichtsScore { get; set; }


        public Categorie Categorie { get; set; }
        public SubCategorie SubCategorie { get; set; }
        public SoortAntwoord SoortAntwoord { get; set; }

        public ICollection<OptieAntwoord> AntwoordOpties { get; set; }
    }
}
