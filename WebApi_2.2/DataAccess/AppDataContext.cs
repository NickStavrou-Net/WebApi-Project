using System.Data.Entity;
using WebApi.DataAccess.Models;

namespace WebApi.DataAccess
{
    public class AppDataContext : DbContext
    {
        public AppDataContext() : base("AppDataContext")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<AuthorizedApp> AuthorizedApps { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // AuthorizedApp
            modelBuilder.Entity<AuthorizedApp>()
                .Property(a => a.AppToken)
                .IsUnicode(false);

            modelBuilder.Entity<AuthorizedApp>()
                .Property(a => a.AppSecret)
                .IsUnicode(false);

            // Tour
            modelBuilder.Entity<Tour>()
                .Property(t => t.Price)
                .HasPrecision(19, 4);
        }
    }
}