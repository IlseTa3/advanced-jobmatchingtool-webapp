using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using advanced_jobmatchingtool_webapp.Models;
using advanced_jobmatchingtool_webapp.Services; // Zorg dat EmailService namespace klopt
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace AdvancedJobmatchingTool.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserStore<ApplicationUser> _userStore;
        private readonly IUserEmailStore<ApplicationUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly EmailService _emailService;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            IUserStore<ApplicationUser> userStore,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            EmailService emailService,
            IWebHostEnvironment hostingEnvironment)
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailService = emailService;
            _hostingEnvironment = hostingEnvironment;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "Het wachtwoord moet minstens {2} en maximaal {1} tekens bevatten.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Wachtwoord")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Bevestig wachtwoord")]
            [Compare("Password", ErrorMessage = "De wachtwoorden komen niet overeen.")]
            public string ConfirmPassword { get; set; }

            [Required]
            [Display(Name = "Voornaam")]
            public string Firstname { get; set; }

            [Required]
            [Display(Name = "Familienaam")]
            public string Lastname { get; set; }

            [Required(ErrorMessage = "Je moet akkoord gaan met de voorwaarden.")]
            [Display(Name = "Ik ga akkoord met de algemene voorwaarden en privacyverklaring")]
            public bool TermsCond { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                user.Voornaam = Input.Firstname;
                user.Familienaam = Input.Lastname;
                user.TermsCond = Input.TermsCond;
                user.ProfileComplete = false;
                user.Role = "Voorlopige kandidaat";

                _logger.LogWarning("Registratie poging voor e-mail: {Email}", Input.Email);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Nieuwe gebruiker geregistreerd.");

                    var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    var naam = $"{Input.Firstname} {Input.Lastname}";
                    var onderwerp = "Bevestig je e-mailadres";
                    var bericht = $@"
                            Beste {naam},<br/><br/>
                            Bedankt voor je registratie bij Opus Aptus. Het enige wat je nu nog moet doen is je e-mailadres bevestigen.<br/><br/>
                            Klik op onderstaande link om je e-mailadres te bevestigen:<br/>
                            <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Bevestig je e-mailadres</a><br/><br/>
                            Met vriendelijke groet,<br/>
                            Het Opus Aptus team.
";

                    await _emailService.SendEmailAsync(naam, Input.Email, onderwerp, bericht);

                    await _userManager.AddToRoleAsync(user, "Voorlopige kandidaat");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return Page();
        }

        private ApplicationUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ApplicationUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Kan geen instantie maken van '{nameof(ApplicationUser)}'. Zorg dat deze klasse een parameterloze constructor heeft.");
            }
        }

        private IUserEmailStore<ApplicationUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("Deze UI vereist een user store met e-mailondersteuning.");
            }
            return (IUserEmailStore<ApplicationUser>)_userStore;
        }
    }
}