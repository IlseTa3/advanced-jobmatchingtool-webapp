using advanced_jobmatchingtool_webapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using advanced_jobmatchingtool_webapp.Services;
using Microsoft.AspNetCore.Identity.UI.Services;

using advanced_jobmatchingtool_webapp.Repositories;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 36))));

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
builder.Services.AddTransient<IEmailSender,EmailService>();

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
await roleManager.CreateAsync(new IdentityRole("Beheerder"));
await roleManager.CreateAsync(new IdentityRole("Klant"));

var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

//Klant 1
/*var klant1User = new ApplicationUser
{
    Voornaam = "Thomas",
    Familienaam = "Everaert",
    UserName = "thomas.everaert@kinsley.com",
    Email = "thomas.everaert@kinsley.com",
    EmailConfirmed = true,
    Role = "Klant",
    PhoneNumber = "0164507812"
};

var result = await userManager.CreateAsync(klant1User, "Welcome123!");
if(result.Succeeded)
{
    await userManager.AddToRoleAsync(klant1User, "Klant");
}

//klant 2
var klant2User = new ApplicationUser
{
    Voornaam = "Maaike",
    Familienaam = "Goossens",
    UserName = "maaike.goossens@delaware.be",
    Email = "maaike.goossens@delaware.be",
    EmailConfirmed = true,
    Role = "Klant",
    PhoneNumber = "0102058874"
};

var result2 = await userManager.CreateAsync(klant2User, "Welcome123!");
if (result.Succeeded)
{
    await userManager.AddToRoleAsync(klant2User, "Klant");
}
*/

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
