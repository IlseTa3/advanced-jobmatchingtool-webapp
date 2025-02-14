namespace advanced_jobmatchingtool_webapp.Models
{
    public class SubCategorie
    {
        public int Id { get; set; }
        public string NaamSubCategorie { get; set; }

        public ICollection<Vragenlijst> Vragenlijst { get; set; }
    }
}
