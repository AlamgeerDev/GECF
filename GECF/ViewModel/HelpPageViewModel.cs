using System;
using System.Windows.Input;
using GECF.Utility;

namespace GECF.ViewModel
{
    public class HelpPageViewModel : BaseViewModel
    {
        public HelpPageViewModel()
        {
            DoAction();
        }


        private string _selectedItem;
        public string SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }

        public async Task DoAction()
        {


            try
            {
                var apicall = await GECFAPI.Instance.GetHelpAsync();


                SelectedItem = apicall;
            }
            catch (Exception e)
            {

            }


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

