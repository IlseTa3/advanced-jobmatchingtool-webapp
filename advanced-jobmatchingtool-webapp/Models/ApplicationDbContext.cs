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

        public DbSet<Categorie> CategorieLijst {  get; set; }
        public DbSet<SubCategorie> SubCategorieLijst { get; set; }
        public DbSet<Vragenlijst> Vragenlijsten { get; set; }
        public DbSet<SoortAntwoord> SoortAntwoorden { get; set; }
        public DbSet<OptieAntwoord> AntwoordOpties { get; set; }
        public DbSet<AntwoordKandidaat> AntwoordKandidaten { get; set; }
        public DbSet<AntwoordKlant> AntwoordKlanten { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Vragenlijst>()
                .HasOne(v => v.Categorie)
                .WithMany(c => c.Vragenlijst)
                .HasForeignKey(v => v.CategorieId);

            modelBuilder.Entity<Vragenlijst>()
                .HasOne(v => v.SubCategorie)
                .WithMany(sc => sc.Vragenlijst)
                .HasForeignKey(v => v.SubCategorieId);

            modelBuilder.Entity<Vragenlijst>()
            .HasOne(v => v.SoortAntwoord)
            .WithMany()
            .HasForeignKey(v => v.SoortAntwoordId);

            modelBuilder.Entity<Vragenlijst>()
                .HasMany(v => v.AntwoordOpties)
                .WithOne(ao => ao.Vragenlijst)
                .HasForeignKey(ao => ao.VragenlijstId);

            modelBuilder.Entity<OptieAntwoord>()
                .HasOne(v => v.SoortAntwoord)
                .WithMany(sa => sa.AntwoordOpties)
                .HasForeignKey(v => v.SoortAntwoordId);

            modelBuilder.Entity<AntwoordKandidaat>()
                .HasOne(ak => ak.Vragenlijst)
                .WithMany()
                .HasForeignKey(ak => ak.VragenlijstId);

            modelBuilder.Entity<AntwoordKandidaat>()
                .HasOne(ak => ak.User)
                .WithMany()
                .HasForeignKey(ak => ak.UserId);

            modelBuilder.Entity<AntwoordKlant>()
                .HasOne(ak => ak.Vragenlijst)
                .WithMany()
                .HasForeignKey(ak => ak.VragenlijstId);

            modelBuilder.Entity<AntwoordKlant>()
                .HasOne(ak => ak.User)
                .WithMany()
                .HasForeignKey(ak => ak.UserId);
        }





    }
}
