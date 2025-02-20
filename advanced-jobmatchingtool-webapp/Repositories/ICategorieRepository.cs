using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Repositories
{
    public interface ICategorieRepository
    {
        Task<IEnumerable<Categorie>> GetAllCategoriesAsync();
        Task<Categorie> GetCategorieByIdAsync(int id);
        Task<Categorie> GetCategorieByIdentityAsync(string id);
        Task CreateCategorieAsync(Categorie categorie);
        Task UpdateCategorieAsync(Categorie categorie);
        Task DeleteCategorieAsync(int id);
    }
}
