namespace advanced_jobmatchingtool_webapp.Models
{
    public class AntwoordOptie
    {
        public int Id { get; set; }
        public string OptieTekst { get; set; }

        public int VraagId { get; set; }
        public Vraag Vraag { get; set; }
    }
}
