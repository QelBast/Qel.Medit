using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Qel.Medit.Common;

/// <summary>
/// Расширения для конфигурации сервисов
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Конфигурирует базу данных PostgreSQL в виде сервиса
    /// </summary>
    /// <typeparam name="T">Контекст базы данных</typeparam>
    /// <param name="collection">Коллекция сервисов</param>
    /// <param name="hostingcontext">Контекст настройщика приложения</param>
    /// <returns></returns>
    public static IServiceCollection ConfigureNpgsqlDatabase<T>(
        this IServiceCollection collection,
        HostBuilderContext hostingcontext) where T : DbContext
    {
        collection.AddDbContext<T, T>((options) =>
        {
            var connectionString = hostingcontext.Configuration.GetConnectionString(typeof(T).Name);
            ArgumentException.ThrowIfNullOrEmpty(connectionString, $"Строка соединения {typeof(T).Name} не задана в конфигурации");
            options.UseNpgsql(
                connectionString,
                server => server.MigrationsAssembly(typeof(T).Assembly.FullName));
        }
        , ServiceLifetime.Transient
        , ServiceLifetime.Transient);
        return collection;
    }

    /// <summary>
    /// Конфигурирует базу данных PostgreSQL в виде сервиса
    /// </summary>
    /// <typeparam name="T">Контекст базы данных</typeparam>
    /// <param name="collection">Коллекция сервисов</param>
    /// <param name="hostingcontext">Контекст настройщика приложения</param>
    /// <returns></returns>
    public static IServiceCollection ConfigureNpgsqlDatabase<T>(
        this IServiceCollection collection,
        IConfigurationManager configurationManager) where T : DbContext
    {
        collection.AddDbContext<T, T>((options) =>
        {
            var connectionString = configurationManager.GetConnectionString(typeof(T).Name);
            ArgumentException.ThrowIfNullOrEmpty(connectionString, $"Строка соединения {typeof(T).Name} не задана в конфигурации");
            options.UseNpgsql(
                connectionString,
                server => server.MigrationsAssembly(typeof(T).Assembly.FullName));
        }
        , ServiceLifetime.Transient
        , ServiceLifetime.Transient);
        return collection;
    }
}
