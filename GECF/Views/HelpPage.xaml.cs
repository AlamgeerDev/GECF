using GECF.ViewModel;

namespace GECF.Views;

public partial class HelpPage : ContentPage
{
    HelpPageViewModel vm;
    List<string> Emails;
    public HelpPage()
    {
        InitializeComponent();
        vm= new HelpPageViewModel();
        this.BindingContext = vm;
        Emails = new List<string>();

    }

    private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        //if (e.SelectedItem == null) return;
        InAppBrowsePage webBrowser = new InAppBrowsePage("");
        await Navigation.PushModalAsync(webBrowser);
    }
    async void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
    {

        //if (e.SelectedItem == null) return;
        InAppBrowsePage webBrowser = new InAppBrowsePage("https://www.gecf.org/about/faqs.aspx");
        await Navigation.PushModalAsync(webBrowser);
    }

    async void TapEmailGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
    {


        Emails.Add("IT.Helpdesk@gecf.org");
        await SendEmail(Emails);
    }

    public async Task SendEmail(List<string> to)
    {
        try
        {
            var message = new EmailMessage
            {
                To = to,

            };
            await Email.ComposeAsync(message);
        }
        catch (FeatureNotSupportedException fbsEx)
        {
            // Email is not supported on this device
        }
        catch (Exception ex)
        {
            // Some other exception occurred
        }
    }
}
