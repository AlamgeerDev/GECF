using AndroidX.ConstraintLayout.Helper.Widget;
using GECF.Models;
using GECF.ViewModel;

namespace GECF.Views;

public partial class NewsDetailsPage : ContentPage
{
    NewsDetailPageViewModel vm;
    public NewsDetailsPage(int CurrentItemIndex, List<News> news)
	{
        InitializeComponent();
        BindingContext = vm = new NewsDetailPageViewModel(Navigation, CurrentItemIndex, news);
        DoAction(CurrentItemIndex);
    }
    void shb_Clicked(System.Object sender, System.EventArgs e)
    {

        Console.WriteLine("Export Clicked");
        string ntext = vm.CatNewsList[vm.CurrentNewsItemPos].title + "\n\n" + vm.CatNewsList[vm.CurrentNewsItemPos].content;

        ShareNews(ntext);


    }
    public async Task ShareNews(string textIn)
    {
        await Share.RequestAsync(new ShareTextRequest
        {
            Text = textIn,
            Title = "GECF News Share"
        }

            );

    }

    private async Task DoAction(int CurrentItemIndex)
    {
        try
        {
            carousel.Position = vm.CurrentNewsItemPos;
        }
        catch (Exception e)
        {

        }

    }

    void Inc_Clicked(System.Object sender, System.EventArgs e)
    {
        try
        {
            if (vm.CurrentNewsItemPos <= vm.CatNewsList.Count - 2)
            {
                vm.CurrentNewsItemPos++;
                carousel.Position = vm.CurrentNewsItemPos;
                dec.IsVisible = true;
                if (vm.CurrentNewsItemPos > vm.CatNewsList.Count - 2)
                {
                    inc.IsVisible = false;
                    dec.IsVisible = true;
                }
            }
        }
        catch (Exception ex)
        {
        }

    }
    async void Dec_Clicked(System.Object sender, System.EventArgs e)
    {
        try
        {
            if (vm.CurrentNewsItemPos > 0)
            {
                vm.CurrentNewsItemPos--;
                carousel.Position = vm.CurrentNewsItemPos;
                inc.IsVisible = true;
                if (vm.CurrentNewsItemPos == 0)
                {
                    dec.IsVisible = false;
                    inc.IsVisible = true;
                }

            }

        }
        catch (Exception ex)
        {
        }
    }
}
