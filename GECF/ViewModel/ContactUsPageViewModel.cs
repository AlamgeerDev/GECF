using System;
using System.Windows.Input;

namespace GECF.ViewModel
{
    public class ContactUsPageViewModel : BaseViewModel
    {
        public ContactUsPageViewModel()
        {
        }


        public ICommand BackCommand
        {
            get
            {
                return new Command(HandleBackCommand, BackCommandcanExecute);
            }
        }

        private async void HandleBackCommand(object obj)
        {
            CanExecuteCommands = false;
            await Application.Current.MainPage.Navigation.PopModalAsync();
            // await BackAction();
            CanExecuteCommands = true;
        }

        private bool BackCommandcanExecute(object arg)
        {
            return CanExecuteCommands;
        }

        private async Task BackAction()
        {
            //await Application.Current.MainPage.Navigation.PushModalAsync(Home);
        }


    }
}

