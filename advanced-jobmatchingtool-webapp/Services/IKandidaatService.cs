using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Services
{
    public interface IKandidaatService
    {
        Task<IEnumerable<Kandidaat>> GetAllKandidatenAsync();
        Task<Kandidaat> GetKandidaatByIdAsync(int id);
        Task<Kandidaat> GetKandidaatByIdentityAsync(string id);
        Task CreateKandidaatAsync(Kandidaat kandidaat);
        Task UpdateKandidaatAsync(Kandidaat kandidaat);
        Task DeleteKandidaatAsync(int id);
    }
}
