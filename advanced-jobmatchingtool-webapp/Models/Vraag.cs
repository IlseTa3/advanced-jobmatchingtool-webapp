using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace advanced_jobmatchingtool_webapp.Models
{
    public class Vraag
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public string Id { get; set; }

        [BsonElement("vraag")]
        public string VraagText { get; set; }

        [BsonElement("categorie")]
        public string Categorie { get; set; }

        [BsonElement("subcategorie")]
        public string SubCategorie { get; set; }

        [BsonElement("type")]
        public string Type { get; set; }


        [BsonElement("opties")]
        public List<string> Opties { get; set; } = new List<string>();

        [BsonElement("extra_info")]
        public string ExtraInformatie { get; set; }


        [BsonElement("voor_wie")]
        public string VoorWie { get; set; }

        public string OptiesString
        {
            get => string.Join(", ", Opties);
            set => Opties = value?.Split(',').Select(s => s.Trim()).ToList() ?? new List<string>();
        }
    }
}
