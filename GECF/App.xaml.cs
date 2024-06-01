
using GECF.Utility;
using GECF.Views;

namespace GECF;

public partial class App : Application
{
    bool ct;

    private static async Task<Tuple<bool, string>> tResult()
    {

        // Task.Run(() => GECFAPI.RefreshToken(Preferences.Get("userName", string.Empty), Preferences.Get("password", string.Empty)).Wait());



        //Tuple<bool, string> apicall= new GECFAPI.RefreshToken(Preferences.Get("userName", string.Empty), Preferences.Get("password", string.Empty));
        var apicall = await GECFAPI.RefreshToken(Preferences.Get("userName", string.Empty), Preferences.Get("password", string.Empty));

        return apicall;
    }
    public App()
	{
        //DevExpress.XamarinForms.DataGrid.Initializer.Init();
        InitializeComponent();
        bool IsUserLoggedIn = false;

        if (Preferences.Get("LoggedIn",false))
        {

            tResult();

             IsUserLoggedIn = Preferences.Get("LoggedIn",false);

            //IsUserLoggedIn = ct;
        }

        if (IsUserLoggedIn)
        {

            //MainPage = new HomePage();
        }
        else
        {
            MainPage = new LoginPage();
        }

    }
}

