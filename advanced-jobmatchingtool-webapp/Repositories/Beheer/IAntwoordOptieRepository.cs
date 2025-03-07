using advanced_jobmatchingtool_webapp.Models;

namespace advanced_jobmatchingtool_webapp.Repositories.Beheer
{
    public interface IAntwoordOptieRepository
    {
        Task<IEnumerable<AntwoordOptie>> GetAllAntwoordOptiesAsync();
        Task<AntwoordOptie> GetAntwoordOptieByIdAsync(int id);
        Task<AntwoordOptie> GetAntwoordOptieByIdentityAsync(string id);
        Task CreateAntwoordOptieAsync(AntwoordOptie optieAntwoord);
        Task UpdateAntwoordOptieAsync(AntwoordOptie optieAntwoord);
        Task DeleteAntwoordOptieAsync(int id);
    }
}
