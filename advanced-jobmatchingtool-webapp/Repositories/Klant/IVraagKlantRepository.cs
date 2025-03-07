using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Repositories.Klant
{
    public interface IVraagKlantRepository
    {
        Task<IEnumerable<VraagKlant>> GetAllVragenAsync();
        Task<VraagKlant> GetVraagByIdAsync(int id);
        Task<VraagKlant> GetVraagCatSubOptiesByIdAsync(int id);
        Task<VraagKlant> GetVraagByIdentityAsync(string id);
        Task CreateVraagAsync(VraagKlant vraag);
        Task UpdateVraagAsync(VraagKlant vraag);
        Task DeleteVraagAsync(int id);
        bool VraagExists(int id);
        Task<List<CategorieSubCat>> GetAllCategoriesAsync();
        Task<List<AntwoordOptie>> GetAllAntwoordOptiesAsync();
    }
}
