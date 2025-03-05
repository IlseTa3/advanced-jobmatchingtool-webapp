using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Repositories;
using advanced_jobmatchingtool_webapp.ViewModels.Antwoorden;

namespace advanced_jobmatchingtool_webapp.Services
{
    public class AntwoordKandidaatService : IAntwoordKandidaatService
    {
        private readonly IAntwoordKandidaatRepository _repository;

        public AntwoordKandidaatService(IAntwoordKandidaatRepository repository) 
        { 
            _repository = repository; 
        }

        public async Task<VraagKandidaat> GetVraagKandidaatByIdAsync(int id)
        {
            return await _repository.GetVraagKandidaatByIdAsync(id);
        }

        public async Task<List<VraagKandidaat>> GetVragenByCategorieAsync(int categorieId)
        {
            return await _repository.GetVragenByCategorieAsync(categorieId);
        }

        public Task<List<VraagKandidaat>> GetVragenByClusteredCategorieAsync(string categorie)
        {
            throw new NotImplementedException();
        }

        public async Task<List<VraagKandidaat>> GetVragenByNaamCategorieAsync(string categorie)
        {
            return await _repository.GetVragenByNaamCategorieAsync(categorie);
        }
    }
}
