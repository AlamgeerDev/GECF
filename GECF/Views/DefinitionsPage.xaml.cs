using GECF.ViewModel;

namespace GECF.Views;

public partial class DefinitionsPage : ContentPage
{
    DefinitionsPageViewModel vm;
    public DefinitionsPage()
	{
		InitializeComponent();
        BindingContext = vm = new DefinitionsPageViewModel(Navigation);
    }

    private void OnLabelSizeChanged(object sender, EventArgs e)
    {
        var content_Label = (sender as Label);
        content_Label.MaxLines = vm.No_Of_Lines_Count;

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CatListView.SelectedItem = null;
    }
}
