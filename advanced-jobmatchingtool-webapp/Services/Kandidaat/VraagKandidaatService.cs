using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Repositories.Kandidaat;
using advanced_jobmatchingtool_webapp.ViewModels.Vraag;

namespace advanced_jobmatchingtool_webapp.Services.Kandidaat
{
    public class VraagKandidaatService : IVraagKandidaatService
    {
        private readonly IVraagKandidaatRepository _vraagRepository;

        public VraagKandidaatService(IVraagKandidaatRepository vraagRepository)
        {
            _vraagRepository = vraagRepository;
        }

        public async Task CreateVraagAsync(VraagKandidaat vraag)
        {
            await _vraagRepository.CreateVraagAsync(vraag);
        }


        public async Task<List<AntwoordOptie>> GetAllAntwoordOptiesAsync()
        {
            return await _vraagRepository.GetAllAntwoordOptiesAsync();
        }

        public async Task<List<CategorieSubCat>> GetAllCategoriesAsync()
        {
            return await _vraagRepository.GetAllCategoriesAsync();
        }

        public async Task<IEnumerable<VraagKandidaatIndexViewModel>> GetAllVragenAsync()
        {

            var vraagKandidaat = await _vraagRepository.GetAllVragenAsync();
            return vraagKandidaat.Select(vk => new VraagKandidaatIndexViewModel
            {
                Id = vk.Id,
                VraagTekst = vk.VraagText != null ? vk.VraagText : "Geen Vraag",
                Categorie = vk.Categorie != null ? vk.Categorie.NaamCategorie : "Geen Categorie",
                SubCategorie = vk.Categorie != null ? vk.Categorie.NaamSubCategorie : "Geen SubCategorie",
                AntwoordOptie = vk.AntwoordOptie != null ? vk.AntwoordOptie.OptieTekst : "Geen antwoordoptie",
                SoortAntwoord = vk.SoortAntwoord
            }).ToList();


        }

        public async Task<VraagKandidaat> GetVraagByIdAsync(int id)
        {
            return await _vraagRepository.GetVraagByIdAsync(id);
        }

        public Task<VraagKandidaat> GetVraagByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateVraagAsync(VraagKandidaat vraag)
        {
            await _vraagRepository.UpdateVraagAsync(vraag);
        }

        public async Task DeleteVraagAsync(int id)
        {
            await _vraagRepository.DeleteVraagAsync(id);
        }

        public bool VraagExists(int id)
        {
            return _vraagRepository.VraagExists(id);
        }

        public async Task<VraagKandidaat> GetVraagCatSubOptiesByIdAsync(int id)
        {
            return await _vraagRepository.GetVraagCatSubOptiesByIdAsync(id);
        }
    }
}
