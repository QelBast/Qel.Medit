using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Qel.Medit.Common;
using Qel.Medit.Dal;

namespace Qel.Medit.ApplicationService;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitMediaElement()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .Services.ConfigureNpgsqlDatabase<DbContextMain>(builder.Configuration);
            
#if DEBUG
		builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
