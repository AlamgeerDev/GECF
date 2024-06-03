using AndroidX.ConstraintLayout.Helper.Widget;
using GECF.Models;
using GECF.ViewModel;

namespace GECF.Views;

public partial class ExpertCommentariesDetailPage : ContentPage
{
    ExpertCommentariesDetailPageViewModel vm;
    public ExpertCommentariesDetailPage(int CurrentItemIndex, List<NewsListing> news)
    {
        InitializeComponent();
        BindingContext = vm = new ExpertCommentariesDetailPageViewModel(Navigation, CurrentItemIndex, news);

        DoAction(CurrentItemIndex);
    }

    private async Task DoAction(int CurrentItemIndex)
    {
        try
        {
            //carousel.ScrollTo(vm.CurrentNewsItemPos,false); // Position =

            carousel.Position = vm.CurrentNewsItemPos;
        }
        catch (Exception e)
        {

        }

    }

    void TapGestureRecognizer_Tapped(System.Object sender, System.EventArgs e)
    {

        vm.NavCommand.Execute(null);
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
            }


            else
            {
                inc.IsVisible = false;
                dec.IsVisible = true;
            }

        }
        catch (Exception ex)
        {
        }

    }
    void Dec_Clicked(System.Object sender, System.EventArgs e)
    {
        try
        {
            if (vm.CurrentNewsItemPos >= 1)
            {
                vm.CurrentNewsItemPos--;
                carousel.Position = vm.CurrentNewsItemPos;
                inc.IsVisible = true;
            }


            else
            {
                dec.IsVisible = false;
                inc.IsVisible = true;
            }


        }
        catch (Exception ex)
        {
        }
    }

}

