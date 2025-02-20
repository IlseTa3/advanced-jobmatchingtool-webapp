using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Repositories
{
    public interface ISubCategorieRepository
    {
        Task<IEnumerable<SubCategorie>> GetAllSubCategoriesAsync();
        Task<SubCategorie> GetSubCategorieByIdAsync(int id);
        Task<SubCategorie> GetSubCategorieByIdentityAsync(string id);
        Task CreateSubCategorieAsync(SubCategorie subCategorie);
        Task UpdateSubCategorieAsync(SubCategorie subCategorie);
        Task DeleteSubCategorieAsync(int id);
        Task<List<Categorie>> GetAllCategoriesAsync();
        bool SubCategorieExists(int id);
    }
}
