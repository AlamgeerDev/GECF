using GECF.Interfaces;
using GECF.Utility;
using GECF.ViewModel;

namespace GECF.Views;

public partial class LoginPage : ContentPage
{
	LoginPageViewModel vm;
    public LoginPage()
	{
		InitializeComponent();
		BindingContext=vm = new LoginPageViewModel();
        vm.IsRememberpass = Preferences.Get("IsRememberPassword", false);
        if (vm.IsRememberpass)
        {
            UserName.Text = Preferences.Get("userName", string.Empty);
            Password.Text = Preferences.Get("password", string.Empty);
        }
        vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");

        UserName.Completed += (object sender, EventArgs e) =>
        {
            Password.Focus();
        };

        Password.Completed += (object sender, EventArgs e) =>
        {
            vm.LoginCommand.Execute(null);
        };
    }
    void imgPwdEye_Tapped(object sender, System.EventArgs e)
    {
        //txtPassword.IsPassword = txtPassword.IsPassword ? false : true;
        vm.IsRememberpass = !vm.IsRememberpass;
        //remeberpassimg.Source = vm.IsRememberpass ? "remember_off.png" : "remember_on.png";
    }


    void Reset_Tapped(object sender, System.EventArgs e)
    {
        InAppBrowsePage webBrowser = new InAppBrowsePage(IConstants.urlForresetPwd);
        Navigation.PushModalAsync(webBrowser);

    }
    protected override bool OnBackButtonPressed()
    {
        if (Device.RuntimePlatform == Device.Android)
            DependencyService.Get<ICloseApplication>().closeApplication();

        return base.OnBackButtonPressed();
    }
}
