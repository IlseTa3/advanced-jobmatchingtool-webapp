using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Repositories
{

    public class CategorieSubCatRepository : ICategorieSubCatRepository
    {
        private readonly ApplicationDbContext _context;

        public CategorieSubCatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateCategorieAsync(CategorieSubCat categorie)
        {
            _context.CategorieSubCats.Add(categorie);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategorieSubCat>> GetAllCategoriesAsync()
        {
            return await _context.CategorieSubCats.ToListAsync();
        }

        public async Task<CategorieSubCat> GetCategorieByIdAsync(int id)
        {
            return await _context.CategorieSubCats.FindAsync(id);
        }

        public Task<CategorieSubCat> GetCategorieByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCategorieAsync(CategorieSubCat categorie)
        {
            _context.CategorieSubCats.Update(categorie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategorieAsync(int id)
        {
            var catSubCat = await GetCategorieByIdAsync(id);
            if (catSubCat != null)
            {
                _context.CategorieSubCats.Remove(catSubCat);
                await _context.SaveChangesAsync();
            }
        }
    }
}
