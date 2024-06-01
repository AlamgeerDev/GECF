using System;
//using Android.Webkit;
using GECF.Interfaces;
using GECF.Resources;
using System.ComponentModel;
using System.Windows.Input;
using System.Diagnostics;

namespace GECF.ViewModel
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// no  internet bool 
        /// </summary>

        private bool _NoInternetVisibility;
        public bool NoInternetVisibility
        {
            get
            {
                return _NoInternetVisibility;
            }
            set
            {
                _NoInternetVisibility = value;
                if (_NoInternetVisibility)
                    NoRecordsVisibility = false;
                RaisePropertyChanged("NoInternetVisibility");
            }
        }

        private bool _NoRecordsVisibility;
        public bool NoRecordsVisibility
        {
            get
            {
                return _NoRecordsVisibility;
            }
            set
            {
                _NoRecordsVisibility = value;
                RaisePropertyChanged("NoRecordsVisibility");
            }
        }





        /// <summary>
        /// Used for the network connectivity 
        /// </summary>
        /// <param name="ShowAlert"></param>
        /// <returns></returns>

        public bool CheckNetworkConnectivity(bool ShowAlert = false)
        {
            var networkAccess=Connectivity.Current.NetworkAccess;
            if (networkAccess==NetworkAccess.Internet)
            {
                return true;
            }
            else
            {
                if (ShowAlert)
                {
                    // Show Alert
                    DependencyService.Get<IDialogService>().ShowAlertAsync(AppResources.checknetwork, AppResources.error, AppResources.ok);
                }
                return false;
            }
        }


        /// <summary>
        /// used to navigation between the viewmodels and views
        /// </summary>



        public BaseViewModel()
        {

        }


        /// <summary>
        /// used to hold the system 
        /// </summary>

        private bool _IsBusy;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                _IsBusy = value;
                if (IsBusy)
                {
                    DependencyService.Get<IDialogService>().ShowLoading();

                }
                else
                {
                    DependencyService.Get<IDialogService>().HideLoading();

                }
            }
        }


        /// <summary>
        /// Used to Navigate browser with the specified url's
        /// </summary>
        /// <param name="url"></param>

        public void NavigateToBrowser(string url)
        {
            try
            {
                //Device.OpenUri(new Uri(url));

                Launcher.TryOpenAsync(new Uri(url));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(AppResources.exception + ex);
            }
        }


        protected bool CanExecuteCommands { get; set; } = true;
        public ICommand NavBackCommand
        {

            get
            {
                return new Command(HandleNavBackCommand, CanNavBackCommandExecute);
            }
        }

        private bool CanNavBackCommandExecute(object arg)
        {
            return CanExecuteCommands;
        }

        public void HandleNavBackCommand(object obj)
        {
            CanExecuteCommands = false;
            CanExecuteCommands = true;
        }

        public ICommand NavBackforNotifCommand
        {

            get
            {
                return new Command<bool>(HandleNavBackforNotifCommand, CanNavBackforNotifCommandExecute);
            }
        }

        private bool CanNavBackforNotifCommandExecute(bool arg)
        {
            return CanExecuteCommands;
        }

        public void HandleNavBackforNotifCommand(bool sendRefreshParametersBack = false)
        {
            CanExecuteCommands = false;
            if (sendRefreshParametersBack)
            {

            }
            else
            {
            }

            CanExecuteCommands = true;

        }

        public ICommand NavBackforNotifCommandAndroid
        {

            get
            {
                return new Command(HandleNavBackforNotifAndroidCommand, CanNavBackforNotifAndroidCommandExecute);
            }
        }

        private bool CanNavBackforNotifAndroidCommandExecute()
        {
            return CanExecuteCommands;
        }

        public void HandleNavBackforNotifAndroidCommand()
        {
            CanExecuteCommands = false;
            //ICUtility.NavigatetoInnerPages = false;
            //ICUtility.NotificationObject = null;
            //ICConstants.IsFeedRefresh = true;
            CanExecuteCommands = true;
        }





    }
}

