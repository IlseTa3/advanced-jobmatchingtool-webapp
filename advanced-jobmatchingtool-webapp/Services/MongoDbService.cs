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

        // Alle vragen oproepen
        public async Task<List<Vraag>> GetAlleVragenAsync()
        {
            var collectie = _database.GetCollection<Vraag>("vragenlijst");

            var vragen = await collectie.Find(Builders<Vraag>.Filter.Empty).ToListAsync();

            var invalidIds = vragen.Where(v => !v.Id.All(char.IsDigit)).ToList();
            if (invalidIds.Any())
            {
                throw new FormatException("Ongeldige ID's gevonden in de database. Controleer de collectie.");
            }
            return vragen;
        }

        // Nieuwe vraag toevoegen
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

        // Automatische nummering toevoegen bij nieuwe vraag
        public async Task<string> GetNextIdAsync()
        {
            var collectie = _database.GetCollection<Vraag>("vragenlijst");

            // Haal de vraag met de hoogste ID op
            var hoogsteIdVraag = await collectie
                .Find(Builders<Vraag>.Filter.Empty)
                .SortByDescending(v => v.Id)
                .Limit(1)
                .FirstOrDefaultAsync();

            if (hoogsteIdVraag == null || string.IsNullOrWhiteSpace(hoogsteIdVraag.Id))
            {
                return "1"; // Begin bij 1 als er geen vragen zijn
            }

            // Probeer de huidige ID te converteren naar een getal
            if (int.TryParse(hoogsteIdVraag.Id, out int hoogsteId))
            {
                return (hoogsteId + 1).ToString(); // Verhoog met 1 en converteer terug naar string
            }
            else
            {
                throw new FormatException("Kan de hoogste ID niet converteren naar een getal. Controleer de data-integriteit.");
            }
        }

        // Vraag oproepen obv de ID
        public async Task<Vraag> GetVraagByIdAsync(string id)
        {
            var collectie = _database.GetCollection<Vraag>("vragenlijst");
            return await collectie.Find(v => v.Id == id).FirstOrDefaultAsync();
        }

        // Updaten van een bestaande vraag
        public async Task UpdateVraagAsync(Vraag updatedVraag)
        {
            var collectie = _database.GetCollection<Vraag>("vragenlijst");
            var filter = Builders<Vraag>.Filter.Eq(v => v.Id, updatedVraag.Id);
            await collectie.ReplaceOneAsync(filter, updatedVraag);
        }

        // Verwijderen van een bestaande vraag
        public async Task DeleteVraagAsync(string id)
        {
            var collectie = _database.GetCollection<Vraag>("vragenlijst");
            var filter = Builders<Vraag>.Filter.Eq(v => v.Id, id);
            await collectie.DeleteOneAsync(filter);
        }
    }
}
