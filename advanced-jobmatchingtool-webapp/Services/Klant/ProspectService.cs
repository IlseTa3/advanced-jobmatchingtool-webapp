using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Repositories.Klant;

namespace advanced_jobmatchingtool_webapp.Services.Klant
{
    public class ProspectService : IProspectService
    {
        private readonly IProspectRepository _prospectRepo;
        public ProspectService(IProspectRepository prospectRepo)
        {
            _prospectRepo = prospectRepo;
        }

        public async Task CreateProspectAsync(Prospect prospect)
        {
            await _prospectRepo.CreateProspectAsync(prospect);
        }
        

        public async Task<IEnumerable<Prospect>> GetAllProspectsAsync()
        {
            return await _prospectRepo.GetAllProspectsAsync();
        }

        public async Task<Prospect> GetProspectByIdAsync(int id)
        {
            return await _prospectRepo.GetProspectByIdAsync(id);
        }

        public async Task UpdateProspectAsync(Prospect prospect)
        {
            await _prospectRepo.UpdateProspectAsync(prospect);
        }

        public async Task DeleteProspectByIdAsync(int id)
        {
            await _prospectRepo.DeleteProspectByIdAsync(id);
        }

        public bool ProspectExists(int id)
        {
            return _prospectRepo.ProspectExists(id);
        }
    }
}
