using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.ViewModels.Antwoorden;

namespace advanced_jobmatchingtool_webapp.Services.Beheer
{
    public interface IBeheerAntwoordKandidaatService
    {
        Task<IEnumerable<BeheerAntwoordKandidaatIndexViewModel>> GetAllAntwoordenKandidaatAsync();
        Task<AntwoordKandidaat> GetAntwoordKandidaatByIdAsync(int id);
        Task<AntwoordKandidaat> GetAntwoordKandidaatByIdentityAsync(string id);
        Task UpdateAntwoordKandidaatAsync(AntwoordKandidaat antwoordKandidaat);
        bool AntwoordExists(int id);
    }
}
