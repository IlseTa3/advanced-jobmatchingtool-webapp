namespace advanced_jobmatchingtool_webapp.Models
{
    public class Kandidaat
    {
        public int Id { get; set; }
        public string Voornaam { get; set; }
        public string Familienaam { get; set; }
        public DateTime Geboortedatum { get; set; }
        public string Email { get; set; }
        public string GsmNr { get; set; }
        public string Straat { get; set; }
        public string Huisnr { get; set; }
        public string Busnr { get; set; }
        public string Postcode { get; set; }
        public string Stad { get; set; }
    }
}
