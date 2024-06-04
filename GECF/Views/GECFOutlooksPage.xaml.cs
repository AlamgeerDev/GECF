using GECF.Models;
using GECF.ViewModel;

namespace GECF.Views;

public partial class GECFOutlooksPage : ContentPage
{
    GECFOutlooksPageViewModel vm;
    public GECFOutlooksPage()
    {
        InitializeComponent();
        BindingContext = vm = new GECFOutlooksPageViewModel(Navigation);
    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;
        var newsString = (DocLink)e.SelectedItem;
        MonthlyListView.SelectedItem = null;

        //Uri olLink = new Uri(newsString.url);
        //Device.OpenUri(olLink);
        InAppBrowsePage webBrowser = new InAppBrowsePage(newsString.url);
        await Navigation.PushModalAsync(webBrowser);
        //var ppag = await Navigation.PopModalAsync();
        // await Navigation.PushModalAsync(webBrowser);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        MonthlyListView.BackgroundColor = Colors.White;
        MonthlyListView.SelectedItem = null;
    }
}
