using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Repositories.Beheer;
using advanced_jobmatchingtool_webapp.ViewModels.Antwoorden;

namespace advanced_jobmatchingtool_webapp.Services.Beheer
{
    public class BeheerAntwoordKlantService : IBeheerAntwoordKlantService
    {
        private readonly IBeheerAntwoordKlantRepository _repository;
        public BeheerAntwoordKlantService(IBeheerAntwoordKlantRepository repository)
        {
            _repository = repository;
        }

        public bool AntwoordExists(int id)
        {
            return _repository.AntwoordExists(id);
        }

        public async Task<IEnumerable<BeheerAntwoordKlantIndexViewModel>> GetAllAntwoordenKlantAsync()
        {
            var antwoordKlant = await _repository.GetAllAntwoordenKlantAsync();
            return antwoordKlant.Select(a => new BeheerAntwoordKlantIndexViewModel
            {
                Id = a.Id,
                UserVoornaam = a.User.Voornaam,
                UserFamilienaam = a.User.Familienaam,
                VraagTekst = a.VraagKlant.VraagText != null ? a.VraagKlant.VraagText : "Geen Vraag",
                AntwoordTekst = a.AntwoordTekst != null ? a.AntwoordTekst : "Geen antwoord",
                Categorie = a.Categorie != null ? a.Categorie : "Geen categorie",
                ExtraInfo = a.ExtraInfo != null ? a.ExtraInfo : "Geen extra info"
            }).ToList();
        }

        public async Task<AntwoordKlant> GetAntwoordKlantByIdAsync(int id)
        {
            return await _repository.GetAntwoordKlantByIdAsync(id);
        }

        public Task<AntwoordKlant> GetAntwoordKlantByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAntwoordKlantAsync(AntwoordKlant antwoordKlant)
        {
            await _repository.UpdateAntwoordKlantAsync(antwoordKlant);
        }
    }
}
