
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class AuditableEntityInterceptor:SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            updateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }

        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            updateEntities(eventData.Context);
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }

        public void updateEntities(DbContext? context)
        {
            if (context == null) return;

            foreach (var entry in context.ChangeTracker.Entries<IEntity>())
            {
                if(entry.State == EntityState.Added || entry.State ==EntityState.Unchanged)
                {
                    entry.Entity.CreatedBy = "kaz";
                    entry.Entity.Created = DateTime.UtcNow;
                };
                if (entry.State == EntityState.Added || entry.State ==EntityState.Modified ||entry.HasChangedOwnEntities())
                {
                    entry.Entity.LastModifiedBy = "kaz";
                    entry.Entity.LastModified = DateTime.UtcNow;
                }
                
            }
        }

     
    }
    public static class Extensions
    {
        public static bool HasChangedOwnEntities(this EntityEntry entry) =>
            entry.References.Any(r =>
            r.TargetEntry != null && r.TargetEntry.Metadata.IsOwned() &&
            (r.TargetEntry.State == EntityState.Added || r.TargetEntry.State == EntityState.Modified));

    }

}
