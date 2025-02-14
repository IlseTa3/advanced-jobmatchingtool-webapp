using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace advanced_jobmatchingtool_webapp.Models
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        DbSet<Categorie> CategorieLijst {  get; set; }
        DbSet<SubCategorie> SubCategorieLijst { get; set; }

        



    }
}
