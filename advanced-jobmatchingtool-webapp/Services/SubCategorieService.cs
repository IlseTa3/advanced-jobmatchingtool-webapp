using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Repositories;

namespace advanced_jobmatchingtool_webapp.Services
{
    public class SubCategorieService : ISubCategorieService
    {
        private readonly ISubCategorieRepository _subCategorieRepository;

        public SubCategorieService(ISubCategorieRepository subCategorieRepository)
        {
            _subCategorieRepository = subCategorieRepository;
        }

        public async Task CreateSubCategorieAsync(SubCategorie subCategorie)
        {
            await _subCategorieRepository.CreateSubCategorieAsync(subCategorie);
        }


        public async Task<IEnumerable<SubCategorie>> GetAllSubCategoriesAsync()
        {
            return await _subCategorieRepository.GetAllSubCategoriesAsync();
        }

        public async Task<SubCategorie> GetSubCategorieByIdAsync(int id)
        {
            return await _subCategorieRepository.GetSubCategorieByIdAsync(id);
        }

        public Task<SubCategorie> GetSubCategorieByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSubCategorieAsync(SubCategorie subCategorie)
        {
            await _subCategorieRepository.UpdateSubCategorieAsync(subCategorie);
        }

        public async Task DeleteSubCategorieAsync(int id)
        {
            await _subCategorieRepository.DeleteSubCategorieAsync(id);
        }

        public async Task<List<Categorie>> GetAllCategoriesAsync()
        {
            return await _subCategorieRepository.GetAllCategoriesAsync();
        }

        public bool SubCategorieExists(int id)
        {
            return _subCategorieRepository.SubCategorieExists(id);
        }
    }
}
