using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.ViewModels.CatSubCats;

namespace advanced_jobmatchingtool_webapp.Services.Beheer
{
    public interface ICategorieSubCatService
    {
        Task<IEnumerable<CatSubCatIndexViewModel>> GetAllCategoriesAsync();
        Task<CategorieSubCat> GetCategorieByIdAsync(int id);
        Task<CategorieSubCat> GetCategorieByIdentityAsync(string id);
        Task CreateCategorieAsync(CategorieSubCat categorie);
        Task UpdateCategorieAsync(CategorieSubCat categorie);
        Task DeleteCategorieAsync(int id);
    }
}
