using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Repositories.Beheer;
using advanced_jobmatchingtool_webapp.ViewModels.Antwoorden;

namespace advanced_jobmatchingtool_webapp.Services.Beheer
{
    public class BeheerAntwoordKandidaatService : IBeheerAntwoordKandidaatService
    {
        private readonly IBeheerAntwoordKandidaatRepository _repository;
        public BeheerAntwoordKandidaatService(IBeheerAntwoordKandidaatRepository repository)
        {
            _repository = repository;
        }

        public bool AntwoordExists(int id)
        {
            return _repository.AntwoordExists(id);
        }

        public async Task<IEnumerable<BeheerAntwoordKandidaatIndexViewModel>> GetAllAntwoordenKandidaatAsync()
        {
            var antwoordKandidaat = await _repository.GetAllAntwoordenKandidaatAsync();
            return antwoordKandidaat.Select(a => new BeheerAntwoordKandidaatIndexViewModel
            {
                Id = a.Id,
                UserVoornaam = a.User.Voornaam,
                UserFamilienaam = a.User.Familienaam,
                VraagTekst = a.VraagKandidaat.VraagText != null ? a.VraagKandidaat.VraagText : "Geen Vraag",
                AntwoordTekst = a.AntwoordTekst != null ? a.AntwoordTekst : "Geen antwoord",
                Categorie = a.Categorie != null ? a.Categorie : "Geen categorie",
                ExtraInfo = a.ExtraInfo != null ? a.ExtraInfo : "Geen extra info"
            }).ToList();
        }

        public async Task<AntwoordKandidaat> GetAntwoordKandidaatByIdAsync(int id)
        {
            return await _repository.GetAntwoordKandidaatByIdAsync(id);
        }

        public Task<AntwoordKandidaat> GetAntwoordKandidaatByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAntwoordKandidaatAsync(AntwoordKandidaat antwoordKandidaat)
        {
            await _repository.UpdateAntwoordKandidaatAsync(antwoordKandidaat);
        }
    }
}
