using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Repositories.Kandidaat
{
    public class StatuutKandidaatRepository : IStatuutKandidaatRepository
    {
        private readonly ApplicationDbContext _context;

        public StatuutKandidaatRepository(ApplicationDbContext context) { _context = context; }
        
        public async Task CreateStatuutForKandidaatAsync(StatuutKandidaat statuut)
        {
            _context.Statuten.Add(statuut);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<StatuutKandidaat>> GetAllStatutenFromKandidatenAsync()
        {
            return await _context.Statuten.ToListAsync();
        }

        public async Task<StatuutKandidaat> GetStatuutFromKandidaatByIdAsync(int id)
        {
            return await _context.Statuten.FindAsync(id);
        }

        public async Task<StatuutKandidaat> GetStatuutFromKandidaatByUserIdAsync(string userId)
        {
            return await _context.Statuten
                .FirstOrDefaultAsync(s => s.ApplicationUserId == userId);
        }


        public async Task UpdateStatuutForKandidaatAsync(StatuutKandidaat statuut)
        {
            _context.Statuten.Update(statuut);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteStatuutFromKandidaatByIdAsync(int id)
        {
            var statuut = await GetStatuutFromKandidaatByIdAsync(id);
            if(statuut != null)
            {
                _context.Statuten.Remove(statuut);
                await _context.SaveChangesAsync();
            }
        }

        public bool StatuutExists(int id)
        {
            return _context.Statuten.Any(s => s.Id == id);
        }
    }
}
