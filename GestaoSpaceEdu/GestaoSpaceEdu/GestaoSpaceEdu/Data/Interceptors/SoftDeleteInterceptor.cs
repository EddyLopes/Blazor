using GestaoSpaceEdu.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GestaoSpaceEdu.Data.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        //return base.SavingChanges(eventData, result);

        return SoftDeleteAlgorithm(eventData, result);
    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        //return base.SavingChangesAsync(eventData, result, cancellationToken);
        return SoftDeleteAlgorithm(eventData, result);
    }

    private InterceptionResult<int> SoftDeleteAlgorithm(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null) return result;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Deleted && entry.Entity is ISoftDelete softDelete)
            {
                entry.State = EntityState.Modified;
                //softDelete.IsDeleted = true;
                softDelete.DeletedAt = DateTimeOffset.UtcNow;
            }
        }

        return result;
    }
}
