using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Repositories.Beheer
{
    public class BeheerAntwoordKandidaatRepository : IBeheerAntwoordKandidaatRepository
    {
        
        private readonly ApplicationDbContext _context;
        public BeheerAntwoordKandidaatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AntwoordExists(int id)
        {
            return _context.AntwoordenKandidaten.Any(x => x.Id == id);
        }

        public async Task<IEnumerable<AntwoordKandidaat>> GetAllAntwoordenKandidaatAsync()
        {
            return await _context.AntwoordenKandidaten
                .Include(a => a.VraagKandidaat)
                .Include(a => a.User)
                .ToListAsync();
        }

        

        public async Task<AntwoordKandidaat> GetAntwoordKandidaatByIdAsync(int id)
        {
            return await _context.AntwoordenKandidaten
                .Include(a => a.User)
                .Include(a => a.VraagKandidaat)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<AntwoordKandidaat> GetAntwoordKandidaatByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAntwoordKandidaatAsync(AntwoordKandidaat antwoordKandidaat)
        {
            _context.AntwoordenKandidaten.Update(antwoordKandidaat);
            await _context.SaveChangesAsync();
        }
    }
}
