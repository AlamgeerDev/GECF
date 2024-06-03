using System;
using System.Windows.Input;
using GECF.Interfaces;
using GECF.Models;
using GECF.Utility;
using GECF.Views;

namespace GECF.ViewModel
{

    public class AssetsAndInvestmentPageViewModel : BaseViewModel
    {

        INavigation navigation;
        public AssetsAndInvestmentPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            DoAction();
            var count = Preferences.Get("numberoflines", 03);
            No_Of_Lines_Count = count;

        }

        public async Task DoAction()
        {
            IsProgressVisible = true;
            var apicall = await GECFAPI.Instance.GetCatNewsAsync(10);
            CurrentNewsList = apicall;
            var group = apicall.GroupBy(e => e.date.ToString())
                 .Select(e => new ObsevablecollectionforGrouping<string, News>(e))
                 .ToList();
            if (group.Count != 0)
            {
                CatNewsList = group;
                IsListVisible = true;
                IsProgressVisible = false;
            }
            else
            {
                IsListVisible = false;
                IsProgressVisible = false;
                DependencyService.Get<IDialogService>().ShowErrorAlert("No News to display", "Sorry", "Ok");
            }


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

        private List<ObsevablecollectionforGrouping<string, News>> _catNewsList;
        public List<ObsevablecollectionforGrouping<string, News>> CatNewsList
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

        public ICommand NavCommand
        {
            get
            {
                return new Command(HandleNavCommand, NavCommandcanExecute);
            }
        }

        private async void HandleNavCommand(object obj)
        {
            CanExecuteCommands = false;
            await NavAction();
            CanExecuteCommands = true;
        }

        private bool NavCommandcanExecute(object arg)
        {
            return CanExecuteCommands;
        }

        public async Task NavAction()
        {

            int index = CurrentNewsList.FindIndex(a => a.title == CurrentNewsItem);

            await navigation.PushModalAsync(new NewsDetailsPage(index, CurrentNewsList));
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
    }
}

