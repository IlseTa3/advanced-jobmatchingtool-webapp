using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Services.Klant
{
    public interface IProspectService
    {
        Task<IEnumerable<Prospect>> GetAllProspectsAsync();
        Task<Prospect> GetProspectByIdAsync(int id);
        Task CreateProspectAsync(Prospect prospect);
        Task UpdateProspectAsync(Prospect prospect);
        bool ProspectExists(int id);
        Task DeleteProspectByIdAsync(int id);
    }
}
