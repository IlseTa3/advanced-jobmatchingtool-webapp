using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Repositories;
using advanced_jobmatchingtool_webapp.ViewModels.AntwoordOpties;

namespace advanced_jobmatchingtool_webapp.Services
{
    public class AntwoordOptieService: IAntwoordOptieService
    {
        private readonly IAntwoordOptieRepository _antwoordOptieRepository;

        public AntwoordOptieService(IAntwoordOptieRepository antwoordOptieRepository)
        {
            _antwoordOptieRepository = antwoordOptieRepository;
        }

        public async Task CreateAntwoordOptieAsync(AntwoordOptie optieAntwoord)
        {
            await _antwoordOptieRepository.CreateAntwoordOptieAsync(optieAntwoord);
        }

        public async Task<IEnumerable<AntwoordOptieIndexViewModel>> GetAllAntwoordOptiesAsync()
        {
            var antwoordOptie = await _antwoordOptieRepository.GetAllAntwoordOptiesAsync();

            return antwoordOptie.Select(ao => new AntwoordOptieIndexViewModel
            {
                Id = ao.Id,
                OptieTekst = ao.OptieTekst,
            }).ToList();

        }

        public async Task<AntwoordOptie> GetAntwoordOptieByIdAsync(int id)
        {
            return await _antwoordOptieRepository.GetAntwoordOptieByIdAsync(id);
        }

        public Task<AntwoordOptie> GetAntwoordOptieByIdentityAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAntwoordOptieAsync(AntwoordOptie optieAntwoord)
        {
            await _antwoordOptieRepository.UpdateAntwoordOptieAsync(optieAntwoord);
        }

        public async Task DeleteAntwoordOptieAsync(int id)
        {
            await _antwoordOptieRepository.DeleteAntwoordOptieAsync(id);
        }
    }
}
