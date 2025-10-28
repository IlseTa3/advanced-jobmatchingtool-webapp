using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Repositories.Klant
{
    public class ProspectRepository : IProspectRepository
    {
        private readonly ApplicationDbContext _context;
        public ProspectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateProspectAsync(Prospect prospect)
        {
            _context.Prospecten.Add(prospect);
            await _context.SaveChangesAsync();
        }

        
        public async Task<IEnumerable<Prospect>> GetAllProspectsAsync()
        {
            return await _context.Prospecten.ToListAsync();
        }

        public async Task<Prospect> GetProspectByIdAsync(int id)
        {
            return await _context.Prospecten.FindAsync(id);
        }

        

        public async Task UpdateProspectAsync(Prospect prospect)
        {
            _context.Prospecten.Update(prospect);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProspectByIdAsync(int id)
        {
            var prospect = await GetProspectByIdAsync(id);
            if(prospect != null)
            {
                _context.Prospecten.Remove(prospect);
                await _context.SaveChangesAsync();
            }
        }

        public bool ProspectExists(int id)
        {
            return _context.Prospecten.Any(p => p.Id == id);
        }
    }
}
