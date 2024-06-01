using System;
using Acr.UserDialogs;

namespace GECF.ViewModel
{
	public class LoginViewModel : BaseViewModel
    {
        public LoginViewModel()
        {

            TestCommand = new Command(TestAction);

        }
        /// <summary>
        /// test command for action
        /// </summary>
        public Command TestCommand { get; set; }
        public void TestAction()
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
                    IsErrorVisible = false;
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
                    IsErrorVisible = false;
                }
                RaisePropertyChanged("Password");
            }
        }

        /// <summary>
        /// ready to login
        /// </summary>
        private bool _isSubmitEnable = true;
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
                    IsErrorVisible = true;
                }
                RaisePropertyChanged("Error");
            }
        }

        /// <summary>
        /// isf there is anty error
        /// </summary>
        private bool _isErrorVisible;
        public bool IsErrorVisible
        {
            get { return _isErrorVisible; }
            set
            {
                _isErrorVisible = value;
                if (!_isErrorVisible)
                {
                    Error = string.Empty;
                }
                RaisePropertyChanged("IsErrorVisible");
            }
        }

        private string _currentYear = DateTime.Now.Year.ToString();
        public string CurrentYear
        {
            get { return _currentYear; }
            set
            {
                _currentYear = value;

                RaisePropertyChanged("CurrentYear");
            }
        }
    }
}

