using GECF.Models;
using GECF.ViewModel;

namespace GECF.Views;

public partial class EnvironmentPage : ContentPage
{
    EnvironmentPageViewModel vm;

    public EnvironmentPage()
    {
        InitializeComponent();
        BindingContext = vm = new EnvironmentPageViewModel(Navigation);


    }
    private void OnLabelSizeChanged(object sender, EventArgs e)
    {
        var content_Label = (sender as Label);
        content_Label.MaxLines = vm.No_Of_Lines_Count;

    }


    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        if (e.SelectedItem == null) return;

        vm.CurrentNewsItemPos = e.SelectedItemIndex;
        var newsString = (News)e.SelectedItem;
        vm.CurrentNewsItem = newsString.title;
        CatListView.SelectedItem = null;
        vm.NavCommand.Execute(null);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CatListView.BackgroundColor = Colors.White;
        CatListView.SelectedItem = null;
    }
}
