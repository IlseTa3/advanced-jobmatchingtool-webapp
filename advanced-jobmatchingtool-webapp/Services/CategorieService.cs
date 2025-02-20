using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Repositories;

namespace advanced_jobmatchingtool_webapp.Services
{
    public class CategorieService: ICategorieService
    {
        private readonly ICategorieRepository _categorieRepository;

        public CategorieService(ICategorieRepository categorieRepository)
        {
            _categorieRepository = categorieRepository;
        }

        //CREATE
        public async Task CreateCategorieAsync(Categorie categorie)
        {
            await _categorieRepository.CreateCategorieAsync(categorie);
        }

        //READ ALL
        public async Task<IEnumerable<Categorie>> GetAllCategoriesAsync()
        {
            return await _categorieRepository.GetAllCategoriesAsync();
        }


        //READ by ID
        public async Task<Categorie> GetCategorieByIdAsync(int id)
        {
            return await _categorieRepository.GetCategorieByIdAsync(id);
        }


        //READ by string ID
        public Task<Categorie> GetCategorieByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }


        //UPDATE
        public async Task UpdateCategorieAsync(Categorie categorie)
        {
            await _categorieRepository.UpdateCategorieAsync(categorie);
        }


        //DELETE
        public async Task DeleteCategorieAsync(int id)
        {
            await _categorieRepository.DeleteCategorieAsync(id);
        }
    }
}
