using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Repositories.Beheer
{
    public interface IBeheerAntwoordKandidaatRepository
    {
        Task<IEnumerable<AntwoordKandidaat>> GetAllAntwoordenKandidaatAsync();
        Task<AntwoordKandidaat> GetAntwoordKandidaatByIdAsync(int id);
        Task<AntwoordKandidaat> GetAntwoordKandidaatByIdentityAsync(string id);
        Task UpdateAntwoordKandidaatAsync(AntwoordKandidaat antwoordKandidaat);
        bool AntwoordExists(int id);
        
    }
}
