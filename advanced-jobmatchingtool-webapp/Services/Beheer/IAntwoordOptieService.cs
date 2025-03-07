using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.ViewModels.AntwoordOpties;

namespace advanced_jobmatchingtool_webapp.Services.Beheer
{
    public interface IAntwoordOptieService
    {
        Task<IEnumerable<AntwoordOptieIndexViewModel>> GetAllAntwoordOptiesAsync();
        Task<AntwoordOptie> GetAntwoordOptieByIdAsync(int id);
        Task<AntwoordOptie> GetAntwoordOptieByIdentityAsync(string id);
        Task CreateAntwoordOptieAsync(AntwoordOptie optieAntwoord);
        Task UpdateAntwoordOptieAsync(AntwoordOptie optieAntwoord);
        Task DeleteAntwoordOptieAsync(int id);
    }
}
