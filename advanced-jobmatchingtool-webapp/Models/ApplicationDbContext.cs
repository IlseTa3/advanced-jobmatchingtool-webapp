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
        public DbSet<AntwoordKandidaat> AntwoordenKandidaten { get; set; }
        public DbSet<AntwoordKlant> AntwoordenKlanten { get; set; }

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

            // Relatie VraagKandidaat → AntwoordKandidaat (1-N)
            modelBuilder.Entity<AntwoordKandidaat>()
                .HasOne(a => a.VraagKandidaat)
                .WithMany(v => v.AntwoordenKandidaten)
                .HasForeignKey(a => a.VraagKandidaatId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relatie VraagKlant → AntwoordKlant (1-N)
            modelBuilder.Entity<AntwoordKlant>()
                .HasOne(a => a.VraagKlant)
                .WithMany(v => v.AntwoordenKlanten)
                .HasForeignKey(a => a.VraagKlantId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relatie AntwoordKandidaat → ApplicationUser (1-N)
            modelBuilder.Entity<AntwoordKandidaat>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relatie AntwoordKlant → ApplicationUser (1-N)
            modelBuilder.Entity<AntwoordKlant>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);

        }

    }
}
