using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using nic_api.Domain;

namespace nic_api.DataAccess
{
    public abstract class DbContextBase : DbContext
    {
        protected DbContextBase(DbContextOptions options) : base(options)
        {
        }

        public override int SaveChanges()
        {
            Save();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            Save();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            Save();
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            Save();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void Save()
        {
            const string defaultUser = "System";
            var defaultDateTime = DateTime.Now;

            var added = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added && e.Entity is IAuditable)
                .Select(e => (IAuditable) e.Entity);

            foreach (var e in added)
            {
                if (string.IsNullOrEmpty(e.CreatedBy)) e.CreatedBy = defaultUser;
                if (e.CreatedAt == default) e.CreatedAt = defaultDateTime;
            }

            var modified = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Entity is IAuditable)
                .Select(e => (IAuditable) e.Entity);

            foreach (var e in modified)
            {
                if (string.IsNullOrEmpty(e.UpdatedBy)) e.UpdatedBy = defaultUser;
                if (e.UpdatedAt == null || e.UpdatedAt == default) e.UpdatedAt = defaultDateTime;
            }
        }
    }
}
