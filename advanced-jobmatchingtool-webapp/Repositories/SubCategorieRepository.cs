using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Repositories
{
    public class SubCategorieRepository : ISubCategorieRepository
    {
        private readonly ApplicationDbContext _context;

        public SubCategorieRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task CreateSubCategorieAsync(SubCategorie subCategorie)
        {
            _context.SubCategorieën.Add(subCategorie);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<SubCategorie>> GetAllSubCategoriesAsync()
        {
            return await _context.SubCategorieën.Include(s => s.Categorie).ToListAsync();
        }

        public async Task<SubCategorie> GetSubCategorieByIdAsync(int id)
        {
            return await _context.SubCategorieën.Include(s => s.Categorie)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public Task<SubCategorie> GetSubCategorieByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateSubCategorieAsync(SubCategorie subCategorie)
        {
            _context.SubCategorieën.Update(subCategorie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubCategorieAsync(int id)
        {
            var subCategorie = await _context.SubCategorieën.FindAsync(id);
            if (subCategorie != null)
            {
                _context.SubCategorieën.Remove(subCategorie);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Categorie>> GetAllCategoriesAsync()
        {
            return await _context.Categorieën.ToListAsync();
        }

        public bool SubCategorieExists(int id)
        {
            return _context.SubCategorieën.Any(sc => sc.Id == id);
        }
    }
}
