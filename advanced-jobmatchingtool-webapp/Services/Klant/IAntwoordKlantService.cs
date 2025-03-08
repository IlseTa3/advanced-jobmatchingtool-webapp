using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Services.Klant
{
    public interface IAntwoordKlantService
    {
        Task<List<VraagKlant>> GetVragenByCategorieAsync(int categorieId);
        Task<List<VraagKlant>> GetVragenByNaamCategorieAsync(string categorie);
        Task<List<VraagKlant>> GetVragenByClusteredCategorieAsync(string categorie);
        Task<VraagKlant> GetVraagKlantByIdAsync(int id);
    }
}
