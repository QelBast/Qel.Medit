using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Qel.Medit.Dal.Entities;
using System.Reflection;

namespace Qel.Medit.Dal;

/// <summary>
/// Представляет контекст основной базы данных решения.
/// </summary>
public class DbContextMain(DbContextOptions<DbContextMain> options) : BaseDbContext(options)
{
    public virtual DbSet<User> Users { get; set; }

    /// <summary>
    /// Переопределяет метод для дальнейшей настройки модели, по заданным конфигурациям,
    /// определенных в классах, реализующих интерфейс <see cref="IEntityTypeConfiguration{TEntity}"/>.
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), type =>
        {
            var attribute = type.GetCustomAttributes()
                                .FirstOrDefault(t => t is DbContextAttribute) as DbContextAttribute;

            return attribute?.ContextType == typeof(DbContextMain);
        });
        base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Создает экземпляр объекта <see cref="DbContextMain"/>.
    /// </summary>
    /// <returns>Экземпляр объекта <see cref="DbContextMain"/>.</returns>
    public static DbContextMain CreateContext()
    {
        var config =
            new ConfigurationBuilder().AddJsonFile("appsettings.json")
                                      .Build();

        var connectionString = config.GetConnectionString(nameof(DbContextMain));
        return new DbContextMain(new DbContextOptionsBuilder<DbContextMain>()
            .LogTo(m => System.Diagnostics.Debug.WriteLine(m))
            .EnableSensitiveDataLogging()
            .UseNpgsql(connectionString).Options);
    }
}
