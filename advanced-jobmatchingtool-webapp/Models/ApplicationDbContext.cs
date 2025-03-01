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

        public DbSet<CategorieSubCat> CategorieSubCats { get; set; }
        public DbSet<VraagKandidaat> VragenKandidaten { get; set; }
        public DbSet<VraagKlant> VragenKlanten { get; set; }
        public DbSet<AntwoordOptie> AntwoordOpties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-many: Categorie → Vragen
            modelBuilder.Entity<VraagKandidaat>()
                .HasOne(v => v.Categorie)
                .WithMany(c => c.VragenKandidaten)
                .HasForeignKey(v => v.CategorieSubCatId);

            // One-to-many: Opties -> vraag
            modelBuilder.Entity<VraagKandidaat>()
            .HasOne(v => v.AntwoordOptie)
            .WithMany(a => a.VragenKandidaten)
            .HasForeignKey(v => v.AntwoordOptieId)
            .OnDelete(DeleteBehavior.SetNull);  // Optioneel: voorkomt cascade delete

            // One-to-many: Categorie → Vragen
            modelBuilder.Entity<VraagKlant>()
                .HasOne(v => v.Categorie)
                .WithMany(c => c.VragenKlanten)
                .HasForeignKey(v => v.CategorieSubCatId);

            // One-to-many: Opties -> vraag
            modelBuilder.Entity<VraagKlant>()
            .HasOne(v => v.AntwoordOptie)
            .WithMany(a => a.VragenKlanten)
            .HasForeignKey(v => v.AntwoordOptieId)
            .OnDelete(DeleteBehavior.SetNull);  // Optioneel: voorkomt cascade delete

        }

    }
}
