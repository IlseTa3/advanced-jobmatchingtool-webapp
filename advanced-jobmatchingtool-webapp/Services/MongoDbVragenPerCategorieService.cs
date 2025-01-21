using advanced_jobmatchingtool_webapp.Models;
using MongoDB.Driver;

namespace advanced_jobmatchingtool_webapp.Services
{
    public class MongoDbVragenPerCategorieService
    {
        private readonly IMongoDatabase _database;
        public MongoDbVragenPerCategorieService(IMongoDatabase database)
        {
            _database = database;
        }

        public async Task<List<Vraag>> GetVragenByCategorieAsync(string categorie)
        {
            var collectie = _database.GetCollection<Vraag>("vragenlijst");
            return await collectie.Find(v => v.Categorie == categorie).ToListAsync();
        }

        public async Task<List<Vraag>> GetVragenByMultipleCategories(string cat1, string cat2, string cat3)
        {
            var collectie = _database.GetCollection<Vraag>("vragenlijst");
            var categorieen = new List<string> { cat1, cat2, cat3 };

            return await collectie.Find(v => categorieen.Contains(v.Categorie)).ToListAsync();
        }

        public async Task<List<Vraag>> GetVragenByClusteredCategories(string pattern)
        {
            var collectie = _database.GetCollection<Vraag>("vragenlijst");
            var filter = Builders<Vraag>.Filter.Regex("Categorie", new MongoDB.Bson.BsonRegularExpression(pattern, "i"));

            return await collectie.Find(filter).ToListAsync();
        }

        public async Task<Vraag> GetVraagByIdAsync(string vraagId)
        {
            var collectie = _database.GetCollection<Vraag>("vragenlijst");
            return await collectie.Find(v => v.Id == vraagId).FirstOrDefaultAsync();
        }
    }
}
