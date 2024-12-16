using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Services
{
    public class KandidaatService : IKandidaatService
    {

        private readonly ApplicationDbContext _context;
        public KandidaatService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreateKandidaatAsync(Kandidaat kandidaat)
        {
            _context.Add(kandidaat);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteKandidaatAsync(int id)
        {
            var kandidaat = await _context.Kandidaten.FindAsync(id);
            if (kandidaat != null)
            {
                _context.Kandidaten.Remove(kandidaat);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Kandidaat>> GetAllKandidatenAsync()
        {
            return await _context.Kandidaten.ToListAsync();
        }

        public async Task<Kandidaat> GetKandidaatByIdAsync(int id)
        {
            try
            {
                return await _context.Kandidaten
                    .FirstOrDefaultAsync(k => k.Id == id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<Kandidaat> GetKandidaatByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateKandidaatAsync(Kandidaat kandidaat)
        {
            try
            {
                _context.Update(kandidaat);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
