using advanced_jobmatchingtool_webapp.Models;
using MongoDB.Driver;

namespace advanced_jobmatchingtool_webapp.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;
        public MongoDbService(IMongoDatabase database)
        {
            _database = database;
        }


        //Alle vragen oproepen
        public async Task<List<Vraag>> GetAlleVragenAsync()
        {
            var collectie = _database.GetCollection<Vraag>("vragenlijst");

            return await collectie.Find(Builders<Vraag>.Filter.Empty).ToListAsync();
        }

        //Nieuwe vraag toevoegen
        public async Task AddVraagAsync(Vraag vraag)
        {
            if (vraag == null)
            {
                throw new ArgumentNullException(nameof(vraag), "Vraag mag niet null/leeg zijn");
            }
            var collectie = _database.GetCollection<Vraag>("vragenlijst");

            vraag.Id = await GetNextIdAsync();
            await collectie.InsertOneAsync(vraag);
        }

        //Automatische nummering toevoegen bij nieuwe vraag
        public async Task<int> GetNextIdAsync()
        {
            var collectie = _database.GetCollection<Vraag>("vragenlijst");

            var hoogsteId = (await collectie
                .Find(Builders<Vraag>.Filter.Empty)
                .SortByDescending(x => x.Id)
                .Limit(1)
                .FirstOrDefaultAsync())?.Id ?? 0;

            return hoogsteId + 1;
        }

        //Vraag oproepen obv de id
        public async Task<Vraag> GetVraagByIdAsync(int id)
        {
            var collectie = _database.GetCollection<Vraag>("vragenlijst");
            return await collectie.Find(v => v.Id == id).FirstOrDefaultAsync();
        }

        //Updaten van een bestaande vraag
        public async Task UpdateVraagAsync(Vraag updatedVraag)
        {
            var collectie = _database.GetCollection<Vraag>("vragenlijst");
            var filter = Builders<Vraag>.Filter.Eq(v => v.Id, updatedVraag.Id);
            await collectie.ReplaceOneAsync(filter, updatedVraag);
        }

        //Verwijderen van een bestaande vraag
        public async Task DeleteVraagAsync(int id)
        {
            var collectie = _database.GetCollection<Vraag>("vragenlijst");
            var filter = Builders<Vraag>.Filter.Eq(v => v.Id, id);
            await collectie.DeleteOneAsync(filter);
        }
    }
}
