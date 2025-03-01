using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Repositories;
using advanced_jobmatchingtool_webapp.ViewModels.Vraag;

namespace advanced_jobmatchingtool_webapp.Services
{
    public class VraagKlantService: IVraagKlantService
    {
        private readonly IVraagKlantRepository _vraagKlantRepo;
        public VraagKlantService(IVraagKlantRepository vraagKlantRepo)
        {
            _vraagKlantRepo = vraagKlantRepo;
        }

        public async Task CreateVraagAsync(VraagKlant vraag)
        {
            await _vraagKlantRepo.CreateVraagAsync(vraag);
        }


        public async Task<List<AntwoordOptie>> GetAllAntwoordOptiesAsync()
        {
            return await _vraagKlantRepo.GetAllAntwoordOptiesAsync();
        }

        public async Task<List<CategorieSubCat>> GetAllCategoriesAsync()
        {
            return await _vraagKlantRepo.GetAllCategoriesAsync();
        }

        public async Task<IEnumerable<VraagKlantIndexViewModel>> GetAllVragenAsync()
        {
            var vraagKlant = await _vraagKlantRepo.GetAllVragenAsync();
            return vraagKlant.Select(v => new VraagKlantIndexViewModel
            {
                Id = v.Id,
                VraagTekst = v.VraagText != null ? v.VraagText : "Geen Vraag",
                Categorie = v.Categorie != null ? v.Categorie.NaamCategorie : "Geen Categorie",
                SubCategorie = v.Categorie != null ? v.Categorie.NaamCategorie : "Geen SubCategorie",
                AntwoordOptie = v.AntwoordOptie != null ? v.AntwoordOptie.OptieTekst : "Geen antwoordoptie",
                SoortAntwoord = v.SoortAntwoord
            }).ToList();
        }

        public async Task<VraagKlant> GetVraagByIdAsync(int id)
        {
            return await _vraagKlantRepo.GetVraagByIdAsync(id);
        }

        public Task<VraagKlant> GetVraagByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<VraagKlant> GetVraagCatSubOptiesByIdAsync(int id)
        {
            return await _vraagKlantRepo.GetVraagCatSubOptiesByIdAsync(id);
        }

        public async Task UpdateVraagAsync(VraagKlant vraag)
        {
            await _vraagKlantRepo.UpdateVraagAsync(vraag);
        }

        public async Task DeleteVraagAsync(int id)
        {
            await _vraagKlantRepo.DeleteVraagAsync(id);
        }

        public bool VraagExists(int id)
        {
            return _vraagKlantRepo.VraagExists(id);
        }
    }
}
