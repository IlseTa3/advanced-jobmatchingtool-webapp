using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Services
{
    public class SubCategorieService : ISubCategorieService
    {
        private readonly ApplicationDbContext _context;
        public SubCategorieService(ApplicationDbContext context)
        {
            _context = context;
        }

        //CREATE
        public async Task CreateSubCategorieAsync(SubCategorie subCategorie)
        {
            _context.Add(subCategorie);
            await _context.SaveChangesAsync();
        }

        //READ ALL
        public async Task<IEnumerable<SubCategorie>> GetAllSubCategoriesAsync()
        {
            return await _context.SubCategorieLijst.ToListAsync();
        }

        //READ by ID
        public async Task<SubCategorie> GetSubCategorieByIdAsync(int id)
        {
            return await _context.SubCategorieLijst.FindAsync(id);
        }

        //READ by ID string
        public async Task<SubCategorie> GetSubCategorieByIdentityAsync(string id)
        {
            return await _context.SubCategorieLijst.FirstOrDefaultAsync(sc => sc.Id.ToString() == id);
        }

        public async Task UpdateSubCategorieAsync(SubCategorie subCategorie)
        {
            _context.Update(subCategorie);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubCategorieAsync(int id)
        {
            var subCategorie = await _context.SubCategorieLijst.FindAsync(id);
            if (subCategorie != null)
            {
                _context.SubCategorieLijst.Remove(subCategorie);
                await _context.SaveChangesAsync();
            }
        }
    }
}
