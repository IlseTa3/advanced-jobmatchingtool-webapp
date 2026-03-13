using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Repositories.Klant
{
    public interface IProspectRepository
    {
        Task<IEnumerable<Prospect>> GetAllProspectsAsync();
        Task<Prospect> GetProspectByIdAsync(int id);
        Task CreateProspectAsync(Prospect prospect);
        Task UpdateProspectAsync(Prospect prospect);
        bool ProspectExists(int id);
        Task DeleteProspectByIdAsync(int id);
    }
}
