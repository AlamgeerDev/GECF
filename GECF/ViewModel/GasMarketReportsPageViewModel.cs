using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GECF.Interfaces;
using GECF.Models;
using GECF.Services;
using GECF.Utility;

namespace GECF.ViewModel
{
    public class GasMarketReportsPageViewModel : BaseViewModel
    {

        public ObservableCollection<PickerClass> ListPicker
        {
            get;
            set;
        }

        INavigation navigation;
        public GasMarketReportsPageViewModel(INavigation navigation)
        {

            ListPicker = PickerService.GetGasMarketReportsPickers();
            this.navigation = navigation;
            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    {
                        IsAndroidSpinnerVisible = false;
                        IsiOSSpinnerVisible = false;
                    }
                    break;

                case Device.iOS:
                    {
                        IsAndroidSpinnerVisible = false;
                        IsAndroidSpinnerButtonVisible = false;
                        IsiOSSpinnerVisible = true;
                    }
                    break;


            }
            DoAction();
            var count = Preferences.Get("numberoflines", 03);
            No_Of_Lines_Count = count;

        }

        public async Task DoAction()
        {
            IsProgressVisible = true;
            IsListVisible = false;
            var apicall = await GECFAPI.Instance.GetGasMarketRepotsAsync(SelectedNumber);
            //var group = apicall.(e => e.date)
            //     .ToList();
            var collection = new ObservableCollection<DocLink>(apicall);
            if (collection.Count != 0)
            {
                CatNewsList = collection;
                IsListVisible = true;
                IsProgressVisible = false;
            }
            else
            {
                IsListVisible = false;
                IsProgressVisible = false;
                DependencyService.Get<IDialogService>().ShowErrorAlert("No Item to display", "Sorry", "Ok");
            }

        }

        /// <summary>
        /// Picker selection
        /// </summary>
        private string _selectedItem = "Daily";
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


        /// <summary>
        /// PickerIndex
        /// </summary>
        private int _selectedNumber = 1;
        public int SelectedNumber
        {
            get
            {
                return _selectedNumber;
            }
            set
            {
                _selectedNumber = value;
                RaisePropertyChanged("SelectedNumber");
            }
        }

        private PickerClass _selectedPicker;
        public PickerClass SelectedPicker
        {
            get
            {
                return _selectedPicker;
            }
            set
            {
                _selectedPicker = value;
                RaisePropertyChanged("SelectedPicker");
                DoSelection(_selectedPicker.value);
                SelectedItem = _selectedPicker.value;
                CurrentSelectedItem = _selectedPicker.value;
            }
        }

        private async Task DoSelection(string selectedPicker)
        {

            if (selectedPicker == "Daily")
            {
                SelectedNumber = 1;

            }
            else if (selectedPicker == "Weekly")
            {
                SelectedNumber = 2;

            }
            else if (selectedPicker == "Monthly")
            {
                SelectedNumber = 3;

            }

            await DoAction();
        }



        /// <summary>
        /// is there is any error
        /// </summary>
        private bool _IsProgressVisible = false;
        public bool IsProgressVisible
        {
            get { return _IsProgressVisible; }
            set
            {
                _IsProgressVisible = value;
                RaisePropertyChanged("IsProgressVisible");
            }
        }

        /// <summary>
        /// isf there is anty error
        /// </summary>
        private bool _IsListVisible = false;
        public bool IsListVisible
        {
            get { return _IsListVisible; }
            set
            {
                _IsListVisible = value;
                RaisePropertyChanged("IsListVisible");
            }
        }

        /// <summary>
        /// Current News Item
        /// </summary>

        private string _currentNewsItem;
        public string CurrentNewsItem
        {
            get
            {
                return _currentNewsItem;
            }

            set
            {
                _currentNewsItem = value;
                RaisePropertyChanged("CurrentNewsItem");
            }
        }

        /// <summary>
        /// Current News Item position
        /// </summary>

        private int _currentNewsItemPos;
        public int CurrentNewsItemPos
        {
            get
            {
                return _currentNewsItemPos;
            }

            set
            {
                _currentNewsItemPos = value;
                RaisePropertyChanged("CurrentNewsItemPos");
            }
        }



        /// <summary>
        /// Current News List
        /// </summary>

        private List<News> _currentNewsList;
        public List<News> CurrentNewsList
        {
            get
            {
                return _currentNewsList;
            }

            set
            {
                _currentNewsList = value;
                RaisePropertyChanged("CurrentNewsList");
            }
        }


        private int _no_Of_Lines_Count = 05;
        public int No_Of_Lines_Count
        {
            get
            {
                return _no_Of_Lines_Count;
            }

            set
            {
                _no_Of_Lines_Count = value;
                RaisePropertyChanged("No_Of_Lines_Count");
            }
        }

        private ObservableCollection<DocLink> _catNewsList;
        public ObservableCollection<DocLink> CatNewsList
        {
            get
            {
                return _catNewsList;
            }
            set
            {
                _catNewsList = value;
                RaisePropertyChanged("CatNewsList");
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
            await navigation.PopModalAsync();
            CanExecuteCommands = true;
        }

        private bool BackCommandcanExecute(object arg)
        {
            return CanExecuteCommands;
        }



        public ICommand SpinnerCommand
        {
            get
            {
                return new Command(HandleSpinnerCommand, SpinnerCommandcanExecute);
            }
        }

        private async void HandleSpinnerCommand(object obj)
        {
            CanExecuteCommands = false;
            await SpinnerAction();
            CanExecuteCommands = true;
        }

        private bool SpinnerCommandcanExecute(object arg)
        {
            return CanExecuteCommands;
        }

        public async Task SpinnerAction()
        {
            IsAndroidSpinnerVisible = !IsAndroidSpinnerVisible;
            if (IsAndroidSpinnerVisible)
            {
                Thickness = new Thickness(0, 15, 0, 0);

            }
            else
            {
                Thickness = new Thickness(0, 10, 0, 0);
            }

        }

        public ICommand CancelCommand
        {
            get
            {
                return new Command(HandleCancelCommand, CancelCommandcanExecute);
            }
        }


        private async void HandleCancelCommand(object obj)
        {
            CanExecuteCommands = false;
            await CancelAction();
            CanExecuteCommands = true;
        }

        private bool CancelCommandcanExecute(object arg)
        {
            return CanExecuteCommands;
        }

        public async Task CancelAction()
        {

            IsAndroidSpinnerVisible = !IsAndroidSpinnerVisible;
            if (IsAndroidSpinnerVisible)
            {
                Thickness = new Thickness(0, 15, 0, 0);

            }
            else
            {
                Thickness = new Thickness(0, 10, 0, 0);
            }

        }


        public ICommand OkCommand
        {
            get
            {
                return new Command(HandleOkCommand, OkCommandcanExecute);
            }
        }

        private async void HandleOkCommand(object obj)
        {
            CanExecuteCommands = false;
            await OkAction();
            CanExecuteCommands = true;
        }

        private bool OkCommandcanExecute(object arg)
        {
            return CanExecuteCommands;
        }

        public async Task OkAction()
        {

            IsAndroidSpinnerVisible = !IsAndroidSpinnerVisible;
            if (IsAndroidSpinnerVisible)
            {
                Thickness = new Thickness(0, 5, 0, 0);

            }
            else
            {
                Thickness = new Thickness(0, 5, 0, 0);
            }
            try
            {
                IsProgressVisible = true;

                CurrentSelectedItem = SelectedItem;
                await DoSpinnerSelection(SelectedItem);
                IsProgressVisible = false;

            }
            catch
            {

            }


        }


        public async Task DoSpinnerSelection(string Item)
        {
            if (IsListVisible)
            {
                await DoSelection(Item);
            }

        }


        /// <summary>
        /// Selected Item
        /// </summary>
        private string _currentSelectedItem = "Production";
        public string CurrentSelectedItem
        {
            get
            {
                return _currentSelectedItem;
            }

            set
            {
                _currentSelectedItem = value;
                RaisePropertyChanged("CurrentSelectedItem");

            }
        }

        /// <summary>
        /// Tickness variation
        /// </summary>
        private Thickness _thickness = new Thickness(0, 10, 0, 0);
        public Thickness Thickness
        {
            get { return _thickness; }
            set
            {
                _thickness = value;
                RaisePropertyChanged("Thickness");
            }
        }


        /// <summary>
        /// For android Spinner
        /// </summary>
        private bool _isAndroidSpinnerButtonVisible = true;
        public bool IsAndroidSpinnerButtonVisible
        {
            get
            {
                return _isAndroidSpinnerButtonVisible;
            }
            set
            {
                _isAndroidSpinnerButtonVisible = value;
                RaisePropertyChanged("IsAndroidSpinnerButtonVisible");
            }
        }

        /// <summary>
        /// For android Spinner
        /// </summary>
        private bool _isAndroidSpinnerVisible = false;
        public bool IsAndroidSpinnerVisible
        {
            get
            {
                return _isAndroidSpinnerVisible;
            }
            set
            {
                _isAndroidSpinnerVisible = value;
                RaisePropertyChanged("IsAndroidSpinnerVisible");
            }
        }

        private Color _backgrounColor = Colors.Transparent;
        public Color BackgrounColor
        {
            get
            {
                return _backgrounColor;
            }
            set
            {
                _backgrounColor = value;
                RaisePropertyChanged("BackgrounColor");
            }
        }


        /// <summary>
        /// For iOS Spinner
        /// </summary>
        private bool _isiOSSpinnerVisible = false;
        public bool IsiOSSpinnerVisible
        {
            get
            {
                return _isiOSSpinnerVisible;
            }
            set
            {
                _isiOSSpinnerVisible = value;
                RaisePropertyChanged("IsiOSSpinnerVisible");
            }
        }



    }
}

