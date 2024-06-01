using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using GECF.Interfaces;
using GECF.Utility;
using GECF.ViewModel;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Microsoft.Maui.Controls.PlatformConfiguration.iOSSpecific;
using Application = Microsoft.Maui.Controls.Application;

namespace GECF.Views;
[DesignTimeVisible(false)]
public partial class HomePage : ContentPage
{
    HomePageViewModel vm;

    public HomePage()
    {
        InitializeComponent();
        if (Device.RuntimePlatform == Device.iOS)
        {

            if (Device.Idiom == TargetIdiom.Tablet)
            {
                SideMenu.Margin = new Thickness(0, 0, 0, 0);
            }

            if (Device.Idiom == TargetIdiom.Phone)
            {
                var isDeviceIphone = DependencyService.Get<Interfaces.IDeviceInfo>().IsIphoneXDevice();
                if (isDeviceIphone)
                {
                    var safeInsets = On<iOS>().SafeAreaInsets();
                    safeInsets.Bottom = -40;
                    safeInsets.Top = -40;
                    this.Padding = safeInsets;
                }
            }

        }
        TokenCall();
        BindingContext = vm = new HomePageViewModel(Navigation);
        vm.DisplayInvalidLoginPrompt += () => DisplayAlert("Error", "Invalid Login, try again", "OK");

    }

    private void OnSearchButtonClicked(object sender, EventArgs e)
    {
        Navigation.PushModalAsync(new SearchPage());
        //SideMenu.State = SideMenuViewState.Default;
    }


    private void OnLogOutButtonClicked(object sender, EventArgs e)
    {
        Preferences.Set("IsUserLogggedIn", false);
       Preferences.Remove("LoggedIn");

        Application.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
        ////SideMenu.State = SideMenuViewState.Default;
    }

    private void OnPricesButtonClicked(object sender, EventArgs e)

    {
        Application.Current.MainPage.Navigation.PushModalAsync(new PricesPage2());
        //SideMenu.State = SideMenuViewState.Default;
    }

    private void OnLeftButtonClicked(object sender, EventArgs e)
    {
        //if (//SideMenu.State == SideMenuViewState.Default)
            //SideMenu.State = SideMenuViewState.RightMenuShown;
        //else
            //SideMenu.State = SideMenuViewState.Default;
    }

    private void OnHomeButtonClicked(object sender, EventArgs e)
    {
        //SideMenu.State = SideMenuViewState.Default;

    }
    private void OnHelpButtonClicked(object sender, EventArgs e)

    {
        Application.Current.MainPage.Navigation.PushModalAsync(new HelpPage());
        //SideMenu.State = SideMenuViewState.Default;
    }

    private void OnAboutButtonClicked(object sender, EventArgs e)

    {
        Application.Current.MainPage.Navigation.PushModalAsync(new AboutPage());
        //SideMenu.State = SideMenuViewState.Default;
    }

    private void OnContactButtonClicked(object sender, EventArgs e)

    {
        Application.Current.MainPage.Navigation.PushModalAsync(new ContactUsPage());
        //SideMenu.State = SideMenuViewState.Default;
    }
    private void OnNewsButtonClicked(object sender, EventArgs e)

    {
        Application.Current.MainPage.Navigation.PushModalAsync(new NewsIconsPage());
        //SideMenu.State = SideMenuViewState.Default;
    }
    private void OnStatisticsButtonClicked(object sender, EventArgs e)

    {
        Navigation.PushModalAsync(new StatisticsIconsPage());
        //SideMenu.State = SideMenuViewState.Default;
    }
    private void OnResearchButtonClicked(object sender, EventArgs e)

    {
        Application.Current.MainPage.Navigation.PushModalAsync(new ResearchIconsPage());
        //SideMenu.State = SideMenuViewState.Default;
    }

    private void OnDefinitionsButtonClicked(object sender, EventArgs e)

    {
        Application.Current.MainPage.Navigation.PushModalAsync(new DefinitionsPage());
        //SideMenu.State = SideMenuViewState.Default;
    }

    private void OnSettingsButtonClicked(object sender, EventArgs e)

    {
        Application.Current.MainPage.Navigation.PushModalAsync(new SettingsPage());
        //SideMenu.State = SideMenuViewState.Default;
    }

    private void OnClick(object sender, EventArgs e)
    {
        Application.Current.MainPage.DisplayAlert("Error", "Error Test", "Ok");

    }

    //void Handle_PositionSelected(object sender, PositionSelectedEventArgs e)
    //{
    //    Debug.WriteLine("Position " + e.NewValue + " selected.");
    //}

    //void Handle_Scrolled(object sender, ScrolledEventArgs e)
    //{
    //    Debug.WriteLine("Scrolled to " + e.NewValue + " percent.");
    //    Debug.WriteLine("Direction = " + e.Direction);
    //}

    void carouselna_Scrolled(System.Object sender,ItemsViewScrolledEventArgs e)
    {

        //vm.Position = e.FirstVisibleItemIndex;
        DoIndicate(vm.Position);

    }

    private void DoIndicate(int position)
    {
        if (position == 0)
        {
            first.BackgroundColor = Colors.Orange;
            second.BackgroundColor = Colors.White;
            third.BackgroundColor = Colors.White;
            fourth.BackgroundColor = Colors.White;
            fifth.BackgroundColor = Colors.White;
        }
        else if (position == 1)
        {
            first.BackgroundColor = Colors.White;
            second.BackgroundColor = Colors.Orange;
            third.BackgroundColor = Colors.White;
            fourth.BackgroundColor = Colors.White;
            fifth.BackgroundColor = Colors.White;
        }
        else if (position == 2)
        {
            first.BackgroundColor = Colors.White;
            second.BackgroundColor = Colors.White;
            third.BackgroundColor = Colors.Orange;
            fourth.BackgroundColor = Colors.White;
            fifth.BackgroundColor = Colors.White;
        }
        else if (position == 3)
        {
            first.BackgroundColor = Colors.White;
            second.BackgroundColor = Colors.White;
            third.BackgroundColor = Colors.White;
            fourth.BackgroundColor = Colors.Orange;
            fifth.BackgroundColor = Colors.White;
        }
        else if (position == 4)
        {
            first.BackgroundColor = Colors.White;
            second.BackgroundColor = Colors.White;
            third.BackgroundColor = Colors.White;
            fourth.BackgroundColor = Colors.White;
            fifth.BackgroundColor = Colors.Orange;
        }
    }

    void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
    {

        vm.CarouselNavCommand.Execute(null);
    }

    protected override bool OnBackButtonPressed()
    {
        if (Device.RuntimePlatform == Device.Android)
            DependencyService.Get<ICloseApplication>().closeApplication();

        return base.OnBackButtonPressed();
    }

    public void TokenCall()
    {
        Device.StartTimer(TimeSpan.FromMinutes(3), () =>
        {
            Task.Run(async () =>
            {
                var token = await GECFAPI.Instance.DoLogin(Preferences.Get("userName", string.Empty), Preferences.Get("password", string.Empty));
            });
            return true;
        });

    }
}
