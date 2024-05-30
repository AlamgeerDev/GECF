using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace GECF.Platforms.Android
{
    [Activity(Theme = "@style/spalshTheme", Label = "GECF", Icon = "@drawable/appicon", MainLauncher = true, NoHistory = true, Exported = true, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity: Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        { 
            base.OnCreate(savedInstanceState);

            // Create your application here
            InvokeMainActivity();
        }

        protected override void OnResume()
        {
            base.OnResume();
            SimulateStartup();
        }

        // Simulates background work that happens behind the splash screen
        void SimulateStartup()
        {
            StartActivity(new Intent(this, typeof(MainActivity)));
        }

        private void InvokeMainActivity()
        {
            var mainActivityIntent = new Intent(this, typeof(MainActivity));
            if (Intent.Extras != null)
            {
                mainActivityIntent.PutExtras(Intent.Extras);
            }
            mainActivityIntent.AddFlags(ActivityFlags.NoAnimation); //Add this line
            StartActivity(mainActivityIntent);
        }
    }
}

