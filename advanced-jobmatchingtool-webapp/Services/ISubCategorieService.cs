using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Services
{
    public interface ISubCategorieService
    {
        Task<IEnumerable<SubCategorie>> GetAllSubCategoriesAsync();
        Task<SubCategorie> GetSubCategorieByIdAsync(int id);
        Task<SubCategorie> GetSubCategorieByIdentityAsync(string id);
        Task CreateSubCategorieAsync(SubCategorie subCategorie);
        Task UpdateSubCategorieAsync(SubCategorie subCategorie);
        Task DeleteSubCategorieAsync(int id);
    }
}
