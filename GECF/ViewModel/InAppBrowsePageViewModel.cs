using System;
using System.Windows.Input;

namespace GECF.ViewModel
{
	public class InAppBrowsePageViewModel: BaseViewModel
    {
        INavigation navigation;
        public InAppBrowsePageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
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
            CanExecuteCommands = true;
        }

        private bool BackCommandcanExecute(object arg)
        {
            return CanExecuteCommands;
        }
    }
}

