using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.ViewModels.Antwoorden;

namespace advanced_jobmatchingtool_webapp.Services.Kandidaat
{
    public interface IAntwoordKandidaatService
    {
        Task<List<VraagKandidaat>> GetVragenByCategorieAsync(int categorieId);
        Task<List<VraagKandidaat>> GetVragenByNaamCategorieAsync(string categorie);
        Task<List<VraagKandidaat>> GetVragenByClusteredCategorieAsync(string categorie);
        Task<VraagKandidaat> GetVraagKandidaatByIdAsync(int id);
    }
}
