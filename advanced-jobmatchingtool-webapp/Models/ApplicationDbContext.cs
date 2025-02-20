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

        public DbSet<Categorie> Categorieën { get; set; }
        public DbSet<SubCategorie> SubCategorieën { get; set; }
        public DbSet<Vraag> Vragen { get; set; }
        public DbSet<AntwoordOptie> AntwoordOpties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-many: Categorie → Vragen
            modelBuilder.Entity<Vraag>()
                .HasOne(v => v.Categorie)
                .WithMany(c => c.Vragen)
                .HasForeignKey(v => v.CategorieId);

            // One-to-many: Categorie → SubCategorieën
            modelBuilder.Entity<SubCategorie>()
                .HasOne(sc => sc.Categorie)
                .WithMany()
                .HasForeignKey(sc => sc.CategorieId);

            // One-to-many: Vraag → AntwoordOpties
            modelBuilder.Entity<AntwoordOptie>()
                .HasOne(ao => ao.Vraag)
                .WithMany(v => v.AntwoordOpties)
                .HasForeignKey(ao => ao.VraagId);
        }

    }
}
