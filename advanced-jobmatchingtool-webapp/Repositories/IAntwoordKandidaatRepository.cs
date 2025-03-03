using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Repositories
{
    public interface IAntwoordKandidaatRepository
    {
        Task<List<VraagKandidaat>> GetVragenByCategorieAsync(int categorieId);
        Task<List<VraagKandidaat>> GetVragenByClusteredCategorieAsync(string categorie);
    }
}
