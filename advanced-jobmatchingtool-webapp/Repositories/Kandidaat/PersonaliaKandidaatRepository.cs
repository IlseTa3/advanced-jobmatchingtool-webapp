using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Repositories.Kandidaat
{
    public class PersonaliaKandidaatRepository : IPersonaliaKandidaatRepository
    {

        private readonly ApplicationDbContext _context;

        public PersonaliaKandidaatRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task CreatePersonaliaForKandidaatAsync(PersonaliaKandidaat personalia)
        {
            _context.Personalia.Add(personalia);
            await _context.SaveChangesAsync();
        }
        

        public async Task<IEnumerable<PersonaliaKandidaat>> GetAllPersonaliaFromKandidatenAsync()
        {
            return await _context.Personalia.ToListAsync();
        }

        public async Task<PersonaliaKandidaat> GetPersonaliaByUserIdAsync(string userId)
        {
            return await _context.Personalia
                .FirstOrDefaultAsync(p => p.ApplicationUserId == userId);
        }

        public async Task<PersonaliaKandidaat> GetPersonaliaFromKandidaatByIdAsync(int id)
        {
            return await _context.Personalia.FindAsync(id);
        }

        public async Task UpdatePersonaliaForKandidaatAsync(PersonaliaKandidaat personalia)
        {
            _context.Personalia.Update(personalia);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePersonaliaFromKandidaatAsync(int id)
        {
            var personalia = await GetPersonaliaFromKandidaatByIdAsync(id);
            if(personalia != null)
            {
                _context.Personalia.Remove(personalia);
                await _context.SaveChangesAsync();
            }
        }

        public bool PersonaliaExists(int id)
        {
            return _context.Personalia.Any(p => p.Id == id);
        }
    }
}
