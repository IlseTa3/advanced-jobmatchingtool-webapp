using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.ViewModels.Vraag;

namespace advanced_jobmatchingtool_webapp.Services
{
    public interface IVraagKlantService
    {
        Task<IEnumerable<VraagKlantIndexViewModel>> GetAllVragenAsync();
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
