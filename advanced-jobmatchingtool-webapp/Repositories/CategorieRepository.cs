using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Repositories
{

    public class CategorieRepository : ICategorieRepository
    {
        private readonly ApplicationDbContext _context;

        public CategorieRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //ADD Categorie
        public async Task CreateCategorieAsync(Categorie categorie)
        {
            _context.Categorieën.Add(categorie);
            await _context.SaveChangesAsync();
        }


        //All Categories
        public async Task<IEnumerable<Categorie>> GetAllCategoriesAsync()
        {
            return await _context.Categorieën.ToListAsync();
        }

        public async Task<Categorie> GetCategorieByIdAsync(int id)
        {
            return await _context.Categorieën.FindAsync(id);
        }

        public Task<Categorie> GetCategorieByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateCategorieAsync(Categorie categorie)
        {
            _context.Categorieën.Update(categorie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategorieAsync(int id)
        {
            var categorie = await _context.Categorieën.FindAsync(id);
            if (categorie != null)
            {
                _context.Categorieën.Remove(categorie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
