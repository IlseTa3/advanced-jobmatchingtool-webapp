using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using advanced_jobmatchingtool_webapp.Repositories.Kandidaat;
using advanced_jobmatchingtool_webapp.Repositories.Klant;
using advanced_jobmatchingtool_webapp.Repositories.Beheer;
using advanced_jobmatchingtool_webapp.Services.Beheer;
using advanced_jobmatchingtool_webapp.Services.Kandidaat;
using advanced_jobmatchingtool_webapp.Services.Klant;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequiredKandidaat", policy => policy.RequireRole("Kandidaat"));
    options.AddPolicy("RequiredKlant", policy => policy.RequireRole("Klant"));
    options.AddPolicy("RequiredBeheerder", policy => policy.RequireRole("Beheerder"));
});


builder.Services.AddRazorPages();

builder.Services.AddScoped<ICategorieSubCatRepository, CategorieSubCatRepository>();
builder.Services.AddScoped<ICategorieSubCatService, CategorieSubCatService>();
builder.Services.AddScoped<IAntwoordOptieRepository, AntwoordOptieRepository>();
builder.Services.AddScoped<IAntwoordOptieService, AntwoordOptieService>();
builder.Services.AddScoped<IVraagKandidaatRepository, VraagKandidaatRepository>();
builder.Services.AddScoped<IVraagKandidaatService, VraagKandidaatService>();
builder.Services.AddScoped<IVraagKlantRepository, VraagKlantRepository>();
builder.Services.AddScoped<IVraagKlantService, VraagKlantService>();
builder.Services.AddScoped<IAntwoordKandidaatRepository, AntwoordKandidaatRepository>();
builder.Services.AddScoped<IAntwoordKandidaatService, AntwoordKandidaatService>();
builder.Services.AddScoped<IAntwoordKlantRepository, AntwoordKlantRepository>();
builder.Services.AddScoped<IAntwoordKlantService,  AntwoordKlantService>();
builder.Services.AddScoped<IBeheerAntwoordKandidaatRepository, BeheerAntwoordKandidaatRepository>();
builder.Services.AddScoped<IBeheerAntwoordKandidaatService, BeheerAntwoordKandidaatService>();
builder.Services.AddScoped<IBeheerAntwoordKlantRepository, BeheerAntwoordKlantRepository>();
builder.Services.AddScoped<IBeheerAntwoordKlantService, BeheerAntwoordKlantService>();
builder.Services.AddScoped<IProspectRepository, ProspectRepository>();
builder.Services.AddScoped<IProspectService, ProspectService>();
builder.Services.AddScoped<IPersonaliaKandidaatRepository, PersonaliaKandidaatRepository>();
builder.Services.AddScoped<IStatuutKandidaatRepository,  StatuutKandidaatRepository>();
builder.Services.AddScoped<IKandidaatService, KandidaatService>();
builder.Services.AddSingleton<EmailService>();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var serviceProvider = app.Services.CreateScope().ServiceProvider;
var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

await roleManager.CreateAsync(new IdentityRole("Kandidaat"));
await roleManager.CreateAsync(new IdentityRole("Voorlopige kandidaat"));
await roleManager.CreateAsync(new IdentityRole("Beheerder"));
await roleManager.CreateAsync(new IdentityRole("Klant"));

var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

var beheerUser = new ApplicationUser
{
    Voornaam = "Marijke",
    Familienaam = "Van Aken",
    UserName = "marijke@opusaptus.be",
    Email = "marijke@opusaptus.be",
    EmailConfirmed = true,
    Role = "Beheerder",
    TermsCond = true
};

var result2 = await userManager.CreateAsync(beheerUser, "OpusAptus123!");
if (result2.Succeeded)
{
    await userManager.AddToRoleAsync(beheerUser, "Beheerder");
}

//Kandidaat 1
var kandidaatUser = new ApplicationUser
{
    Voornaam = "Thomas",
    Familienaam = "Everaert",
    UserName = "thomas.everaert@kinsley.com",
    Email = "thomas.everaert@kinsley.com",
    EmailConfirmed = true,
    Role = "Kandidaat",
};

var result = await userManager.CreateAsync(kandidaatUser, "OpusAptusWelcome!123");
if(result.Succeeded)
{
    await userManager.AddToRoleAsync(kandidaatUser, "Kandidaat");
}

//klant 2
/*var kandidaat2User = new ApplicationUser
{
    Voornaam = "Maaike",
    Familienaam = "Goossens",
    UserName = "maaike.goossens@gmail.com",
    Email = "maaike.goossens@gmail.com",
    EmailConfirmed = true,
    Role = "Kandidaat",
    PhoneNumber = "0102058874"
};

var result2 = await userManager.CreateAsync(kandidaat2User, "OpusAptusWelcome!123!");
if (result.Succeeded)
{
    await userManager.AddToRoleAsync(kandidaat2User, "Kandidaat");
}*/


//var kandidaatUser = await userManager.FindByEmailAsync("ilse_tastenhoye@msn.com");
//await userManager.AddToRoleAsync(kandidaatUser, "Beheerder");


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();



app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
