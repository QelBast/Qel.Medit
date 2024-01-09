using Microsoft.EntityFrameworkCore.Design;

namespace Qel.Medit.Dal;

/// <summary>
/// Реализует фабрику для создания производных экземпляров <see cref="DbContextMain" />.
/// Во время разработки могут быть созданы производные экземпляры <see cref="DbContextMain" />,
/// чтобы включить во время разработки возможности, такие как миграции.
/// </summary>
public class DbContextMainFactory : IDesignTimeDbContextFactory<DbContextMain>
{
    /// <summary>Создает новый экземпляр производного контекста.</summary>
    /// <param name="args">Аргументы, предоставляемые службой времени разработки.</param>
    /// <returns></returns>
    public DbContextMain CreateDbContext(string[] args)
    {
        var dbContext = DbContextMain.CreateContext();
        return dbContext;
    }
}
