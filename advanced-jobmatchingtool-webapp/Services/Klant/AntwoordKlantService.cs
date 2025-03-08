using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Repositories.Klant;

namespace advanced_jobmatchingtool_webapp.Services.Klant
{
    public class AntwoordKlantService : IAntwoordKlantService
    {
        private readonly IAntwoordKlantRepository _repository;
        public AntwoordKlantService(IAntwoordKlantRepository repository) 
        { 
            _repository = repository;
        }
        
        public async Task<VraagKlant> GetVraagKlantByIdAsync(int id)
        {
            return await _repository.GetVraagKlantByIdAsync(id);
        }

        public async Task<List<VraagKlant>> GetVragenByCategorieAsync(int categorieId)
        {
            return await _repository.GetVragenByCategorieAsync(categorieId);
        }

        public async Task<List<VraagKlant>> GetVragenByClusteredCategorieAsync(string categorie)
        {
            return await _repository.GetVragenByClusteredCategorieAsync(categorie);
        }

        public async Task<List<VraagKlant>> GetVragenByNaamCategorieAsync(string categorie)
        {
            return await _repository.GetVragenByNaamCategorieAsync(categorie);
        }
    }
}
