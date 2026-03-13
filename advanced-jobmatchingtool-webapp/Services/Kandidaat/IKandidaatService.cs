using advanced_jobmatchingtool_webapp.ViewModels.Kandidaat;

namespace advanced_jobmatchingtool_webapp.Services.Kandidaat
{
    public interface IKandidaatService
    {
        Task<KandidaatProfielViewModel> GetProfielAsync(string userId);
        Task<bool> SaveProfielAsync(KandidaatProfielViewModel model, string userId);
    }
}
