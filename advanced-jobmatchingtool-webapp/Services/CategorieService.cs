using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Services
{
    public class CategorieService : ICategorieService
    {
        private readonly ApplicationDbContext _context;
        public CategorieService(ApplicationDbContext context)
        {
            _context = context;
        }


        //CREATE
        public async Task CreateCategorieAsync(Categorie categorie)
        {
            _context.Add(categorie);
            await _context.SaveChangesAsync();
        }

        //READ ALL
        public async Task<IEnumerable<Categorie>> GetAllCategoriesAsync()
        {
            return await _context.CategorieLijst.ToListAsync();
        }

        //READ by ID
        public async Task<Categorie> GetCategorieByIdAsync(int id)
        {
            return await _context.CategorieLijst.FindAsync(id);
        }

        //READ by ID string
        public async Task<Categorie> GetCategorieByIdentityAsync(string id)
        {
            return await _context.CategorieLijst.FirstOrDefaultAsync(c => c.Id.ToString() == id);
        }


        //UPDATE
        public async Task UpdateCategorieAsync(Categorie categorie)
        {
            _context.Update(categorie);
            await _context.SaveChangesAsync();
        }


        //DELETE
        public async Task DeleteCategorieAsync(int id)
        {
            var categorie = await _context.CategorieLijst.FindAsync(id);
            if (categorie != null)
            {
                _context.CategorieLijst.Remove(categorie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
