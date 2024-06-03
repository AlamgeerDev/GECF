using GECF.Models;
using GECF.ViewModel;

namespace GECF.Views;

public partial class GasMarketReportsPage : ContentPage
{
    GasMarketReportsPageViewModel vm;
    public GasMarketReportsPage()
    {
        InitializeComponent();
        BindingContext = vm = new GasMarketReportsPageViewModel(Navigation);
        vm.CurrentSelectedItem = "Daily";
    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;

        vm.CurrentNewsItemPos = e.SelectedItemIndex;
        var newsString = (DocLink)e.SelectedItem;
        MonthlyListView.SelectedItem = null;
        OpenBrowser(newsString.url);
        //InAppBrowsePage webBrowser = new InAppBrowsePage(newsString.url);
        //await Navigation.PushModalAsync(webBrowser);
    }

    private async void SpinnerOnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

        if (e.SelectedItem == null) return;
        var newsString = (PickerClass)e.SelectedItem;
        vm.SelectedItem = newsString.value;
    }

    public async Task OpenBrowser(string url)
    {
        await Browser.OpenAsync(url, BrowserLaunchMode.SystemPreferred);

    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        MonthlyListView.BackgroundColor = Colors.White;
        MonthlyListView.SelectedItem = null;
    }


}
