using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Repositories.Beheer
{
    public interface IBeheerAntwoordKlantRepository
    {
        Task<IEnumerable<AntwoordKlant>> GetAllAntwoordenKlantAsync();
        Task<AntwoordKlant> GetAntwoordKlantByIdAsync(int id);
        Task<AntwoordKlant> GetAntwoordKlantByIdentityAsync(string id);
        Task UpdateAntwoordKlantAsync(AntwoordKlant antwoordKlant);
        bool AntwoordExists(int id);
    }
}
