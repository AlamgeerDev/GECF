using GECF.CustomRenderers;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Controls.Compatibility.Hosting;
using Microsoft.Maui.Hosting;

namespace GECF;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCompatibility()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("Museo-300.otf", "museo300");
                fonts.AddFont("Museo-500.otf", "museo300");
                

            })
			.ConfigureMauiHandlers((handlers =>
			{
#if ANDROID
				handlers.AddCompatibilityRenderer(typeof(BorderLessEntry), typeof(Platforms.Android.CustomRenderers.BorderLessEntryRenderer));

#elif IOS
				handlers.AddCompatibilityRenderer(typeof(BorderLessEntry), typeof(Platforms.iOS.CustomRenderers.BorderLessEntryRenderer));
#endif

            }));
		

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

