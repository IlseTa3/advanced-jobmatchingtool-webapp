using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Repositories.Beheer
{
    public class BeheerAntwoordKlantRepository : IBeheerAntwoordKlantRepository
    {
        public readonly ApplicationDbContext _context;
        public BeheerAntwoordKlantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AntwoordExists(int id)
        {
            return _context.AntwoordenKlanten.Any(x => x.Id == id);
        }

        public async Task<IEnumerable<AntwoordKlant>> GetAllAntwoordenKlantAsync()
        {
            return await _context.AntwoordenKlanten
                .Include(a => a.VraagKlant)
                .Include(a => a.User)
                .ToListAsync();
        }

        public async Task<AntwoordKlant> GetAntwoordKlantByIdAsync(int id)
        {
            return await _context.AntwoordenKlanten
                .Include(a => a.VraagKlant)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<AntwoordKlant> GetAntwoordKlantByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAntwoordKlantAsync(AntwoordKlant antwoordKlant)
        {
            _context.AntwoordenKlanten.Update(antwoordKlant);
            await _context.SaveChangesAsync();
        }
    }
}
