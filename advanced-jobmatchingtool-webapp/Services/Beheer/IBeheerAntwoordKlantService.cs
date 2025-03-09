using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.ViewModels.Antwoorden;

namespace advanced_jobmatchingtool_webapp.Services.Beheer
{
    public interface IBeheerAntwoordKlantService
    {
        Task<IEnumerable<BeheerAntwoordKlantIndexViewModel>> GetAllAntwoordenKlantAsync();
        Task<AntwoordKlant> GetAntwoordKlantByIdAsync(int id);
        Task<AntwoordKlant> GetAntwoordKlantByIdentityAsync(string id);
        Task UpdateAntwoordKlantAsync(AntwoordKlant antwoordKlant);
        bool AntwoordExists(int id);
    }
}
