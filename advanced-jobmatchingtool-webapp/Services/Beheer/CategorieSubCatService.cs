using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Repositories.Beheer;
using advanced_jobmatchingtool_webapp.ViewModels.CatSubCats;

namespace advanced_jobmatchingtool_webapp.Services.Beheer
{
    public class CategorieSubCatService : ICategorieSubCatService
    {
        private readonly ICategorieSubCatRepository _categorieRepository;

        public CategorieSubCatService(ICategorieSubCatRepository categorieRepository)
        {
            _categorieRepository = categorieRepository;
        }

        //CREATE
        public async Task CreateCategorieAsync(CategorieSubCat categorie)
        {
            await _categorieRepository.CreateCategorieAsync(categorie);
        }

        //READ ALL
        public async Task<IEnumerable<CatSubCatIndexViewModel>> GetAllCategoriesAsync()
        {
            var categorieSubCats = await _categorieRepository.GetAllCategoriesAsync();

            return categorieSubCats.Select(c => new CatSubCatIndexViewModel
            {
                Id = c.Id,
                NaamCategorie = c.NaamCategorie,
                NaamSubCategorie = c.NaamSubCategorie
            }).ToList();
        }


        //READ by ID
        public async Task<CategorieSubCat> GetCategorieByIdAsync(int id)
        {
            return await _categorieRepository.GetCategorieByIdAsync(id);
        }


        //READ by string ID
        public Task<CategorieSubCat> GetCategorieByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }


        //UPDATE
        public async Task UpdateCategorieAsync(CategorieSubCat categorie)
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
