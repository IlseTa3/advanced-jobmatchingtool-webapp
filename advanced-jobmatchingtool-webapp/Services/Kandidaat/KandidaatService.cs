using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Repositories.Kandidaat;
using advanced_jobmatchingtool_webapp.ViewModels.Kandidaat;
using Microsoft.AspNetCore.Identity;

namespace advanced_jobmatchingtool_webapp.Services.Kandidaat
{
    public class KandidaatService : IKandidaatService
    {
        private readonly IPersonaliaKandidaatRepository _personaliaRepo;
        private readonly IStatuutKandidaatRepository _statuutRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public KandidaatService(IPersonaliaKandidaatRepository personaliaRepo, IStatuutKandidaatRepository statuutRepo, UserManager<ApplicationUser> userManager, IWebHostEnvironment env)
        {
            _personaliaRepo = personaliaRepo;
            _statuutRepo = statuutRepo;
            _userManager = userManager;
            _env = env;
        }

        public async Task<KandidaatProfielViewModel> GetProfielAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
           
            var personalia = await _personaliaRepo.GetPersonaliaByUserIdAsync(userId) ?? new PersonaliaKandidaat();
            var statuut = await _statuutRepo.GetStatuutFromKandidaatByUserIdAsync(userId) ?? new StatuutKandidaat();

            return new KandidaatProfielViewModel
            {
                Voornaam = user.Voornaam,
                Familienaam = user.Familienaam,
                Email = user.Email,
                Personalia = new PersonaliaKandidaatViewModel
                {
                    Gsmnr = personalia.Gsmnr,
                    Telnr = personalia.Telnr,
                    Postcode = personalia.Postcode,
                    Stad = personalia.Stad,
                    Land = personalia.Land
                },
                Statuut = new StatuutKandidaatViewModel
                {
                    Diagnose = statuut.Diagnose,
                    HeeftIMWStatuut = statuut.HeeftIMWStatuut,
                    HulpNodigBijInvullen = statuut.HulpNodigBijInvullen,
                    BestaandeBestanden = statuut.IMWStatuutBestand?
                        .Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(b =>
                        {
                            var delen = b.Split('|');
                            //nieuw formaat
                            if(delen.Length == 2)
                            {
                                return new BestandViewModel
                                {
                                    UniekeNaam = delen[0],
                                    OrigineleNaam = delen[1]
                                };
                            }
                            if(delen.Length == 1)
                            {
                                //oud formaat, alleen unieke naam
                                return new BestandViewModel
                                {
                                    UniekeNaam = delen[0],
                                    OrigineleNaam = delen[0]
                                };
                            }
                            //corrupt formaat, negeren
                            return null;
                        })
                        .Where(b => b != null)
                        .ToList() ?? new List<BestandViewModel>()
                }
            };
        }

        public async Task<bool> SaveProfielAsync(KandidaatProfielViewModel model, string userId)
        {
            //Personalia opslaan
            var personaliaEntity = await _personaliaRepo.GetPersonaliaByUserIdAsync(userId);
                if (personaliaEntity == null)
                {
                    personaliaEntity = new PersonaliaKandidaat { ApplicationUserId = userId };
            }
            
            personaliaEntity.Gsmnr = model.Personalia.Gsmnr;
            personaliaEntity.Telnr = model.Personalia.Telnr;
            personaliaEntity.Postcode = model.Personalia.Postcode;
            personaliaEntity.Stad = model.Personalia.Stad;
            personaliaEntity.Land = model.Personalia.Land;

            //opslaan
            if (personaliaEntity.Id == 0) 
            {
                await _personaliaRepo.CreatePersonaliaForKandidaatAsync(personaliaEntity);
            }
            else
            {
                await _personaliaRepo.UpdatePersonaliaForKandidaatAsync(personaliaEntity);
            }

            //Statuut opslaan

            var statuutEntity = await _statuutRepo.GetStatuutFromKandidaatByUserIdAsync(userId);
            bool isNewStatuut = false;
            if (statuutEntity == null)
            {
                statuutEntity = new StatuutKandidaat { ApplicationUserId = userId };
                isNewStatuut = true;
            }

            
            statuutEntity.Diagnose = model.Statuut.Diagnose;
            statuutEntity.HeeftIMWStatuut = model.Statuut.HeeftIMWStatuut;
            statuutEntity.HulpNodigBijInvullen = model.Statuut.HulpNodigBijInvullen;

            

            //Bestanden verwerken

            var bestandsnamen = new List<string>();

            if(model.Statuut.IMWBestanden != null)
            {
                var toegestaneExtensies = new[] { ".pdf", ".doc", ".docx", ".jpg", ".jpeg", ".png" };
                var uploadPad = Path.Combine(_env.WebRootPath, "uploads");

                foreach (var bestand in model.Statuut.IMWBestanden)
                {
                    var extensie = Path.GetExtension(bestand.FileName).ToLowerInvariant();
                    if (!toegestaneExtensies.Contains(extensie) || bestand.Length > 20 * 1024 * 1024)
                        continue;

                    var origineleNaam = Path.GetFileName(bestand.FileName);
                    var uniekeNaam = Guid.NewGuid().ToString() + extensie;
                    var pad = Path.Combine(uploadPad, uniekeNaam);
                    using (var stream = new FileStream(pad, FileMode.Create))
                    {
                        await bestand.CopyToAsync(stream);
                    }
                    bestandsnamen.Add($"{uniekeNaam}|{origineleNaam}");
                }

            }

            if (bestandsnamen.Any())
            {
                statuutEntity.IMWStatuutBestand = string.Join(",", bestandsnamen);
            }

            //Create/Update statuut
            if (isNewStatuut)
            {
                await _statuutRepo.CreateStatuutForKandidaatAsync(statuutEntity);
            }
            else
            {
                await _statuutRepo.UpdateStatuutForKandidaatAsync(statuutEntity);
            }


                // Profielstatus en rolwijziging
                var user = await _userManager.FindByIdAsync(userId);
            if (IsProfielVolledig(personaliaEntity, statuutEntity))
            {
                user.ProfileComplete = true;
                if (await _userManager.IsInRoleAsync(user, "Voorlopige kandidaat"))
                {
                    await _userManager.RemoveFromRoleAsync(user, "Voorlopige kandidaat");
                    await _userManager.AddToRoleAsync(user, "Kandidaat");
                }
            }
            await _userManager.UpdateAsync(user);

            return true;


        }

        private bool IsProfielVolledig(PersonaliaKandidaat p, StatuutKandidaat s)
        {
            return !string.IsNullOrWhiteSpace(p.Gsmnr)
                && !string.IsNullOrWhiteSpace(p.Postcode)
                && !string.IsNullOrWhiteSpace(p.Stad)
                && !string.IsNullOrWhiteSpace(p.Land)
                && s.HeeftIMWStatuut
                && !string.IsNullOrWhiteSpace(s.IMWStatuutBestand);
        }

    }
}
