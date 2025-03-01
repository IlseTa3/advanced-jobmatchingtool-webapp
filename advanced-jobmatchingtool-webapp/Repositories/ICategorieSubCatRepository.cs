using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Repositories
{
    public interface ICategorieSubCatRepository
    {
        Task<IEnumerable<CategorieSubCat>> GetAllCategoriesAsync();
        Task<CategorieSubCat> GetCategorieByIdAsync(int id);
        Task<CategorieSubCat> GetCategorieByIdentityAsync(string id);
        Task CreateCategorieAsync(CategorieSubCat categorie);
        Task UpdateCategorieAsync(CategorieSubCat categorie);
        Task DeleteCategorieAsync(int id);
    }
}
