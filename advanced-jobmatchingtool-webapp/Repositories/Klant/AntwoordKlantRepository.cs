using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Repositories.Klant
{
    public class AntwoordKlantRepository : IAntwoordKlantRepository
    {
        public readonly ApplicationDbContext _context;
        public AntwoordKlantRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        //Om vragen op te halen en te laten beantwoorden per categorie
        public async Task<List<VraagKlant>> GetVragenByCategorieAsync(int categorieId)
        {
            return await _context.VragenKlanten
                .Where(vk => vk.CategorieSubCatId == categorieId)
                .Include(vk => vk.Categorie)
                .Include(vk => vk.AntwoordOptie)
                .ToListAsync();
        }

        //Om vragen op te halen en te laten antwoorden maar geclusterd per categoriedeel
        //Voorbeeld: Werk - Werkomstandigheden - Werkervaring
        public async Task<List<VraagKlant>> GetVragenByClusteredCategorieAsync(string categorie)
        {
            return await _context.VragenKlanten
                .Where(vk => vk.Categorie.NaamCategorie.Contains(categorie) ||
                        vk.Categorie.NaamSubCategorie.Contains(categorie))
                .Include(vk => vk.Categorie)
                .Include(vk => vk.AntwoordOptie)
                .ToListAsync();
        }
    }
}
