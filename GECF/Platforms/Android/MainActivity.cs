using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;

namespace GECF;

[Activity(Theme = "@style/Maui.SplashTheme", ScreenOrientation = ScreenOrientation.Portrait,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        LaunchMode = LaunchMode.SingleTask,
        Exported = false)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        // TabLayoutResource = Resource.Layout.Tabbar;
        // ToolbarResource = Resource.Layout.Toolbar;
        Window.AddFlags(WindowManagerFlags.Fullscreen);
        Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);


        base.OnCreate(savedInstanceState);
        UserDialogs.Init(this);
        //DependencyService.Register<ScreenshotService>();
        //CachedImageRenderer.Init(true);
        //SideMenuViewRenderer.Preserve();
        //ScreenshotManager.Activity=this;
        //global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
        //Xamarin.Essentials.Platform.Init(this, savedInstanceState);
        //global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
       // DependencyService.Get<ScreenshotService>().SetActivity(this);

       // LoadApplication(new App());
    }
    public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
    {
       OnRequestPermissionsResult(requestCode, permissions, grantResults);

        base.OnRequestPermissionsResult(requestCode, permissions, grantResults);


    }
}

