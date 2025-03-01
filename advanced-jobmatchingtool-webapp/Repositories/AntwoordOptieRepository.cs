using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Repositories
{
    public class AntwoordOptieRepository: IAntwoordOptieRepository
    {
        private readonly ApplicationDbContext _context;

        public AntwoordOptieRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task CreateAntwoordOptieAsync(AntwoordOptie optieAntwoord)
        {
            _context.AntwoordOpties.Add(optieAntwoord);
            await _context.SaveChangesAsync();
        }



        public async Task<IEnumerable<AntwoordOptie>> GetAllAntwoordOptiesAsync()
        {
            return await _context.AntwoordOpties.ToListAsync();
        }

        public async Task<AntwoordOptie> GetAntwoordOptieByIdAsync(int id)
        {
            return await _context.AntwoordOpties.FindAsync(id);
        }

        public Task<AntwoordOptie> GetAntwoordOptieByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAntwoordOptieAsync(AntwoordOptie optieAntwoord)
        {
            _context.AntwoordOpties.Update(optieAntwoord);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAntwoordOptieAsync(int id)
        {
            var optieAntwoord = await GetAntwoordOptieByIdAsync(id);
            if (optieAntwoord != null)
            {
                _context.AntwoordOpties.Remove(optieAntwoord);
                await _context.SaveChangesAsync();
            }
        }
    }
}
