using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Repositories.Kandidaat
{
    public interface IStatuutKandidaatRepository
    {
        Task<IEnumerable<StatuutKandidaat>> GetAllStatutenFromKandidatenAsync();

        Task<StatuutKandidaat> GetStatuutFromKandidaatByIdAsync(int id);

        Task<StatuutKandidaat> GetStatuutFromKandidaatByUserIdAsync(string userId);

        Task CreateStatuutForKandidaatAsync(StatuutKandidaat statuut);

        Task UpdateStatuutForKandidaatAsync(StatuutKandidaat statuut);

        Task DeleteStatuutFromKandidaatByIdAsync(int id);

        bool StatuutExists(int id);
    }
}
