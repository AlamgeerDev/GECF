using GECF.ViewModel;

namespace GECF.Views;

public partial class ContactUsPage : ContentPage
{
    ContactUsPageViewModel vm;
    public ContactUsPage()
	{
		InitializeComponent();
        vm = new ContactUsPageViewModel();
        this.BindingContext = vm;
    }
}
