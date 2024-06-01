using System;
using Foundation;
using System.Runtime.InteropServices;
using GECF.Interfaces;
using UIKit;

namespace GECF.Platforms.iOS.DependencyServices
{
	public class ScreenshotService:IScreenShotService
    {
        public byte[] Capture()
        {
            var capture = UIScreen.MainScreen.Capture();
            using (NSData data = capture.AsPNG())
            {
                var bytes = new byte[data.Length];
                Marshal.Copy(data.Bytes, bytes, 0, Convert.ToInt32(data.Length));
                return bytes;
            }
        }
    }
}

