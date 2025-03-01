using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Repositories
{
    public class VraagKandidaatRepository: IVraagKandidaatRepository
    {
        private readonly ApplicationDbContext _context;

        public VraagKandidaatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateVraagAsync(VraagKandidaat vraag)
        {
            _context.VragenKandidaten.Add(vraag);
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

        public async Task<IEnumerable<VraagKandidaat>> GetAllVragenAsync()
        {
            return await _context.VragenKandidaten
                .Include(v => v.Categorie)
                .Include(v => v.AntwoordOptie)
                .ToListAsync();
        }

        public async Task<VraagKandidaat> GetVraagByIdAsync(int id)
        {
            return await _context.VragenKandidaten.FindAsync(id);
        }

        public Task<VraagKandidaat> GetVraagByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateVraagAsync(VraagKandidaat vraag)
        {
            _context.VragenKandidaten.Update(vraag);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteVraagAsync(int id)
        {
            var vraag = await GetVraagByIdAsync(id);

            if (vraag != null)
            {
                _context.VragenKandidaten.Remove(vraag);
                await _context.SaveChangesAsync();
            }
        }


        public bool VraagExists(int id)
        {
            return _context.VragenKandidaten.Any(v => v.Id == id);
        }

        public async Task<VraagKandidaat> GetVraagCatSubOptiesByIdAsync(int id)
        {
            return await _context.VragenKandidaten
                .Include(vkl => vkl.Categorie)
                .Include(vkl => vkl.AntwoordOptie)
                .FirstOrDefaultAsync(vkl => vkl.Id == id);
        }
    }
}
