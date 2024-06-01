using System;
using System.Windows.Input;
using GECF.Interfaces;
using GECF.Utility;

namespace GECF.ViewModel
{

    public class LoginPageViewModel : BaseViewModel
    {
        public Action DisplayInvalidLoginPrompt;

        INavigation navigation;

        public LoginPageViewModel(INavigation navigation)
        {
            //this.navigationService = navigationService;
            UserName = Preferences.Get("userName", string.Empty);
            Password = Preferences.Get("password", string.Empty);
            IsRememberpass = Preferences.Get("IsRememberPassword", false);
            TestCommand = new Command(TestAction);
            SubmitCommand = new Command(OnSubmit);

        }

        public ICommand SubmitCommand { protected set; get; }

        public void OnSubmit()
        {
            if (UserName != "macoratti@yahoo.com" || Password != "secret")
            {
                DisplayInvalidLoginPrompt();
            }
        }
        /// <summary>
        /// test command for action
        /// </summary>
        public Command TestCommand { get; set; }
        public void TestAction()
        {

        }

        public LoginPageViewModel()
        {
        }

        /// <summary>
        /// For Excuting login
        /// </summary>
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        /// <summary>
        /// getting username value 
        /// </summary>
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                if (_userName == null || Password == null || _userName?.Length == 0 || Password?.Length == 0)
                    IsSubmitEnable = false;
                else
                    IsSubmitEnable = true;

                if (_userName.Length > 0)
                {
                    //IsErrorVisible = false;
                }
                RaisePropertyChanged("UserName");
            }
        }

        /// <summary>
        /// getting password
        /// </summary>
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                if (UserName == null || _password == null || _password?.Length == 0 || UserName?.Length == 0)
                    IsSubmitEnable = false;
                else
                    IsSubmitEnable = true;

                if (_password.Length > 0)
                {
                    //IsErrorVisible = false;
                }
                RaisePropertyChanged("Password");
            }
        }

        /// <summary>
        /// ready to login
        /// </summary>
        private bool _isSubmitEnable = false;
        public bool IsSubmitEnable
        {
            get { return _isSubmitEnable; }
            set
            {
                _isSubmitEnable = value;
                RaisePropertyChanged("IsSubmitEnable");
            }
        }

        /// <summary>
        /// Error String
        /// </summary>
        private string _error;
        public string Error
        {
            get { return _error; }
            set
            {
                _error = value;
                if (!string.IsNullOrEmpty(_error))
                {
                    //IsErrorVisible = true;
                }
                RaisePropertyChanged("Error");
            }
        }

        /// <summary>
        /// isf there is anty error
        /// </summary>
        private bool _IsRememberpass = false;
        public bool IsRememberpass
        {
            get { return _IsRememberpass; }
            set
            {
                _IsRememberpass = value;
                RaisePropertyChanged("IsRememberpass");
            }
        }

        //public ICommand LoginCommand
        //{
        //    get
        //    {
        //        return new Command(HandleLoginCommand, LoginCommandcanExecute);
        //    }
        //}

        public Command LoginCommand =>
            new Command(async() =>
            {
                await LoginAction();
            });

        private async void HandleLoginCommand(object obj)
        {
            CanExecuteCommands = false;
            await LoginAction();
            CanExecuteCommands = true;
        }

        private bool LoginCommandcanExecute(object arg)
        {
            return CanExecuteCommands;
        }

        private async Task LoginAction()
        {
            try
            {
                if (IsSubmitEnable)
                {
                    DependencyService.Get<IDialogService>().ShowLoading();
                    var result = await GECFAPI.Instance.DoLogin(UserName.Trim(), Password.Trim());

                    if (result.Item1)
                    {


                        if (IsRememberpass)
                        {
                            Preferences.Set("userName", UserName);
                            Preferences.Set("password", Password);
                            Preferences.Set("IsRememberPassword", IsRememberpass);

                        }


                    }
                    else
                    {
                        Error = result.Item2;
                        DependencyService.Get<IDialogService>().HideLoading();
                        await DependencyService.Get<IDialogService>().ShowAlertAsync("Incorrect username or password", "Warning", "OK");
                        return;
                    }
                    DependencyService.Get<IDialogService>().HideLoading();
                    Preferences.Set("IsRememberPassword", IsRememberpass);
                    var token = Preferences.Get("AccessToken", string.Empty);
                    Preferences.Set("token", token);
                    Preferences.Set("LoggedIn", "true");
                    //await Application.Current.MainPage.Navigation.PushModalAsync(new HomePage());

                }
                else
                {
                    await DependencyService.Get<IDialogService>().ShowAlertAsync("Please enter username and password", "Warning", "OK");

                }
            }
            catch(Exception ex)
            {

            }
            

        }


    }
}

