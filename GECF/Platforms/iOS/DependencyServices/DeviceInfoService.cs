using System;
using GECF.Platforms.iOS.DependencyServices;
using UIKit;

[assembly:Dependency(typeof(DeviceInfoService))]
namespace GECF.Platforms.iOS.DependencyServices
{
    public class DeviceInfoService : GECF.Interfaces.IDeviceInfo
    {
        public DeviceInfoService()
        { }
        public bool IsIphoneXDevice()
        {
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone)
            {
                if ((UIScreen.MainScreen.Bounds.Height * UIScreen.MainScreen.Scale) == 2436)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

