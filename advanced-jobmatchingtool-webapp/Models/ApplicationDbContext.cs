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


        public DbSet<PersonaliaKandidaat> PersonaliaKandidaten { get; set; }
        public DbSet<StudiesKandidaat> StudiesKandidaten { get; set; }
        public DbSet<BehoeftesKandidaat> BehoeftesKandidaten { get; set; }
        public DbSet<TriggersKandidaat> TriggersKandidaten { get; set; }
        public DbSet<WerkKandidaat> WerkKandidaten { get; set; }
        public DbSet<StressKandidaat> StressKandidaten { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuratie voor PersonaliaKandidaat
            modelBuilder.Entity<PersonaliaKandidaat>()
                .Property(p => p.Antwoord)
                .IsRequired(false); // Sta null-waarden toe

            modelBuilder.Entity<StudiesKandidaat>()
                .Property(p => p.Antwoord)
                .IsRequired(false);

            modelBuilder.Entity<BehoeftesKandidaat>()
                .Property(p => p.Antwoord)
                .IsRequired(false);

            modelBuilder.Entity<TriggersKandidaat>()
                .Property(p => p.Antwoord)
                .IsRequired(false);

            modelBuilder.Entity<WerkKandidaat>()
                .Property(p => p.Antwoord)
                .IsRequired(false);

            modelBuilder.Entity<StressKandidaat>()
               .Property(p => p.Antwoord)
               .IsRequired(false);

            // Configuratie voor Identity
            modelBuilder.Entity<IdentityUserLogin<string>>().HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>().HasKey(r => new { r.UserId, r.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>().HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
        }



    }
}
