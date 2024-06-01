using GECF.Resources;
using GECF.ViewModel;

namespace GECF.Views;

public partial class InAppBrowsePage : ContentPage
{
    private WebView webView1;
    InAppBrowsePageViewModel vm;
    public InAppBrowsePage(string url)
    {
        InitializeComponent();
        BindingContext = vm = new InAppBrowsePageViewModel(Navigation);
        webView1 = new WebView() { WidthRequest = 800, HeightRequest = 800, Source = url };
        stack.Children.Add(webView1);
        activity_Indicator.IsVisible = true;
        webView1.IsVisible = false;
        webView1.Navigated += (sender, e) => {
            activity_Indicator.IsVisible = false;
            webView1.IsVisible = true;
        };
    }

    public void Eval(String js)
    {
        webView1.Navigated += (o, s) => {
            webView1.Eval(js);
        };
    }
}

