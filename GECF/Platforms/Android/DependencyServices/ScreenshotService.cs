using System;
using Android.App;
using Android.Graphics;
using GECF.Interfaces;

namespace GECF.Platforms.Android.DependencyServices
{
	public class ScreenshotService: IScreenShotService
    {
        protected Activity _currentActivity;
        public void SetActivity(Activity activity) => _currentActivity = activity;

        public byte[] Capture()
        {
            var rootView = _currentActivity.Window.DecorView.RootView;

            using (var screenshot = Bitmap.CreateBitmap(
                                    rootView.Width,
                                    rootView.Height,
                                    Bitmap.Config.Argb8888))
            {
                var canvas = new Canvas(screenshot);
                rootView.Draw(canvas);

                using (var stream = new MemoryStream())
                {
                    screenshot.Compress(Bitmap.CompressFormat.Png, 90, stream);
                    return stream.ToArray();
                }
            }
        }
    }
}

