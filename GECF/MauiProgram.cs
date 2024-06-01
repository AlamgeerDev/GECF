using CommunityToolkit.Maui;
using GECF.CustomRenderers;
using GECF.Interfaces;
//using GECF.Platforms.Android.DependencyServices;
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
            .UseMauiCommunityToolkit()
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

#if ANDROID
        DependencyService.Register<IDialogService, GECF.Platforms.Android.DependencyServices.DialogServices>();
        DependencyService.Register<ICloseApplication, GECF.Platforms.Android.DependencyServices.CloseApplication>();
        DependencyService.Register<IFileService, GECF.Platforms.Android.DependencyServices.FileService>();
        DependencyService.Register<IScreenShotService, GECF.Platforms.Android.DependencyServices.ScreenshotService>();
        //builder.Services.AddSingleton<IDialogService,GECF.Platforms.Android.DependencyServices. DialogServices>();
#elif IOS
		DependencyService.Register<IDialogService, GECF.Platforms.iOS.DependencyServices.DialogServices>();
		//DependencyService.Register<IScreenshotService, GECF.Platforms.iOS.DependencyServices.ScreenshotService>();
		DependencyService.Register<IFileService, GECF.Platforms.iOS.DependencyServices.FileService>();
		

#endif



#if DEBUG
        builder.Logging.AddDebug();
#endif
		


        return builder.Build();
	}
}

