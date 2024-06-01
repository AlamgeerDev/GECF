using Foundation;
using UIKit;

namespace GECF;

[Register("AppDelegate")]
public class AppDelegate : MauiUIApplicationDelegate
{
	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();


    //public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    //{
    //    UIView statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
    //    if (statusBar != null && statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
    //    {
    //        statusBar.BackgroundColor = UIColor.Yellow;
    //    }
    //    return base.FinishedLaunching(application, launchOptions);
    //}
}

