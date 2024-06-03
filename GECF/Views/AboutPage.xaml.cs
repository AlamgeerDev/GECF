using GECF.ViewModel;

namespace GECF.Views;

public partial class AboutPage : ContentPage
{
    AboutPageViewModel vm;
    public AboutPage()
	{
		InitializeComponent();
        BindingContext=vm = new AboutPageViewModel();
    }
}
