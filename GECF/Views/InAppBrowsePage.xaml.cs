using GECF.Resources;

namespace GECF.Views;

public partial class InAppBrowsePage : ContentPage
{
    private WebView webView;
    private Button backButton;
    public InAppBrowsePage(string URL)
    {
        InitializeComponent();
        this.Title = AppResources.GECF;
        var layout = new StackLayout();
        layout.Orientation = StackOrientation.Vertical;
        if (Device.RuntimePlatform == Device.Android)
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }
        //WebView needs to be given a height and width request within layouts to render
        backButton = new Button() { Text = "Back", TextColor = Colors.Red, WidthRequest = 80, HeightRequest = 50, BackgroundColor = Colors.Transparent };
        webView = new WebView() { Margin = 100, WidthRequest = 1000, HeightRequest = 1000, Source = URL };
        layout.Children.Add(backButton);
        layout.Children.Add(webView);
        Content = layout;


    }
    public void Eval(String js)
    {
        webView.Navigated += (o, s) =>
        {
            webView.Eval(js);
        };
    }
}

