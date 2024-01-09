using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Qel.Medit.Dal.Entities.Common;

namespace Qel.Medit.Dal;

public abstract class BaseDbContext : DbContext
{
    /// <summary>
    /// Возвращает или задает флаг отмены удаления сущностей реализующих интерфейс <see cref="ISoftDelete"/>
    /// </summary>
    /// <remarks>
    /// При значение true, сущность удаляется физически из таблицы,
    /// При значение false, устанавливается влаг IsDeleted = true,
    /// </remarks>
    public bool IgnoreSoftDelete { get; set; }

    protected BaseDbContext(DbContextOptions options) : base(options) { }

    /// <summary>
    /// Сохраняет все изменения, сделанные в этом контексте, в базу данных.
    /// </summary>
    public override int SaveChanges()
    {
        UpdateSoftDeleteStatuses();
        UpdateDateTimeProperty();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        UpdateSoftDeleteStatuses();
        UpdateDateTimeProperty();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void UpdateSoftDeleteStatuses()
    {
        if (IgnoreSoftDelete)
        {
            return;
        }

        foreach (var entry in ChangeTracker.Entries())
        {
            var interfaces = entry.Entity.GetType()
                                  .GetInterfaces();

            if (interfaces.Contains(typeof(ISoftDelete)))
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsDeleted"] = false;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsDeleted"] = true;
                        break;
                }
            }
        }
    }
    private void UpdateDateTimeProperty()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            var interfaces = entry.Entity.GetType()
                                  .GetInterfaces();

            if (interfaces.Contains(typeof(ICreateAndModifyProperties)))
            {
                var entity = (ICreateAndModifyProperties)entry.Entity;
                var timePoint = DateTime.UtcNow;
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreationDateTime = timePoint;
                        entity.ModifyDateTime = timePoint;
                        break;
                    case EntityState.Modified:
                        entity.ModifyDateTime = timePoint;
                        break;
                }
            }
        }
    }
}
