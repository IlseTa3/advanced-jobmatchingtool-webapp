using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Repositories.Klant
{
    public interface IAntwoordKlantRepository
    {
        Task<List<VraagKlant>> GetVragenByCategorieAsync(int categorieId);
        Task<List<VraagKlant>> GetVragenByClusteredCategorieAsync(string categorie);
    }
}
