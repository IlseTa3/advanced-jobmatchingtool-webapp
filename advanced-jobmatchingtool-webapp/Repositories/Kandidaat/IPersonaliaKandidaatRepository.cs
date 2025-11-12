using advanced_jobmatchingtool_webapp.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace advanced_jobmatchingtool_webapp.Repositories.Kandidaat
{
    public interface IPersonaliaKandidaatRepository
    {
        Task<IEnumerable<PersonaliaKandidaat>> GetAllPersonaliaFromKandidatenAsync();
        Task<PersonaliaKandidaat> GetPersonaliaFromKandidaatByIdAsync(int id);

        Task<PersonaliaKandidaat> GetPersonaliaByUserIdAsync(string userId);

        Task CreatePersonaliaForKandidaatAsync(PersonaliaKandidaat personalia);
        Task UpdatePersonaliaForKandidaatAsync(PersonaliaKandidaat personalia);

        Task DeletePersonaliaFromKandidaatAsync(int id);


        bool PersonaliaExists(int id);
    }
}
