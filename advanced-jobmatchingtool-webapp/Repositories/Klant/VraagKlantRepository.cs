using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Repositories.Klant
{
    public class VraagKlantRepository : IVraagKlantRepository
    {
        private readonly ApplicationDbContext _context;
        public VraagKlantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateVraagAsync(VraagKlant vraag)
        {
            _context.VragenKlanten.Add(vraag);
            await _context.SaveChangesAsync();
        }


        public async Task<List<AntwoordOptie>> GetAllAntwoordOptiesAsync()
        {
            return await _context.AntwoordOpties.ToListAsync();
        }

        public async Task<List<CategorieSubCat>> GetAllCategoriesAsync()
        {
            return await _context.CategorieSubCats.ToListAsync();
        }

        public async Task<IEnumerable<VraagKlant>> GetAllVragenAsync()
        {
            return await _context.VragenKlanten
                .Include(vkl => vkl.Categorie)
                .Include(vkl => vkl.AntwoordOptie)
                .ToListAsync();
        }

        public async Task<VraagKlant> GetVraagByIdAsync(int id)
        {
            return await _context.VragenKlanten.FindAsync(id);
        }

        public Task<VraagKlant> GetVraagByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateVraagAsync(VraagKlant vraag)
        {
            _context.VragenKlanten.Update(vraag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVraagAsync(int id)
        {
            var vraagKlant = await GetVraagByIdAsync(id);
            if (vraagKlant != null)
            {
                _context.VragenKlanten.Remove(vraagKlant);
                await _context.SaveChangesAsync();
            }
        }

        public bool VraagExists(int id)
        {
            return _context.VragenKlanten.Any(v => v.Id == id);
        }

        public async Task<VraagKlant> GetVraagCatSubOptiesByIdAsync(int id)
        {
            return await _context.VragenKlanten
                .Include(vkl => vkl.Categorie)
                .Include(vkl => vkl.AntwoordOptie)
                .FirstOrDefaultAsync(vkl => vkl.Id == id);
        }
    }
}
