using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.ViewModels.Vraag;

namespace advanced_jobmatchingtool_webapp.Services
{
    public interface IVraagKandidaatService
    {
        Task<IEnumerable<VraagKandidaatIndexViewModel>> GetAllVragenAsync();
        Task<VraagKandidaat> GetVraagByIdAsync(int id);
        Task<VraagKandidaat> GetVraagCatSubOptiesByIdAsync(int id);
        Task<VraagKandidaat> GetVraagByIdentityAsync(string id);
        Task CreateVraagAsync(VraagKandidaat vraag);
        Task UpdateVraagAsync(VraagKandidaat vraag);
        Task DeleteVraagAsync(int id);
        bool VraagExists(int id);
        Task<List<CategorieSubCat>> GetAllCategoriesAsync();
        Task<List<AntwoordOptie>> GetAllAntwoordOptiesAsync();
    }
}
