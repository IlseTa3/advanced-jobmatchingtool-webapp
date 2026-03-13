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

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    //Migraties
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    //Rollen en admin user aanmaken
    
    var config = services.GetRequiredService<IConfiguration>();
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();

    // Rollen aanmaken indien ze nog niet bestaan
    string[] roles = { "Kandidaat", "Voorlopige kandidaat", "Beheerder", "Klant" };

    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Admin gegevens uit environment variables
    var adminEmail = config["Admin:Email"];
    var adminPassword = config["Admin:Password"];
    var adminVoornaam = config["Admin:Voornaam"];
    var adminFamilienaam = config["Admin:Familienaam"];

    // Admin user aanmaken indien hij nog niet bestaat
    var adminUser = await userManager.FindByEmailAsync(adminEmail);

    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            Voornaam = adminVoornaam,
            Familienaam = adminFamilienaam,
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true,
            TermsCond = true
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Beheerder");
        }
        else
        {
            // Optioneel: log errors
        }
    }
}


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
