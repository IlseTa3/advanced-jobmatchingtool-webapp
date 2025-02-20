namespace advanced_jobmatchingtool_webapp.Models
{
    public class Categorie
    {
        public int Id { get; set; }
        public string NaamCategorie { get; set; }


        public ICollection<Vraag> Vragen { get; set; } = new List<Vraag>();
    }
}
