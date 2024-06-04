using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GECF.Interfaces;
using GECF.Models;
using GECF.Utility;

namespace GECF.ViewModel
{
    public class GECFOutlooksPageViewModel : BaseViewModel
    {
        INavigation navigation;
        public GECFOutlooksPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            DoAction();

        }

        public async Task DoAction()
        {
            IsProgressVisible = true;
            IsListVisible = false;
            var apicall = await GECFAPI.Instance.GetGECFOutlooksAsync();
            var group = apicall.OrderByDescending(e => e.date)
                 .ToList();
            var collection = new ObservableCollection<DocLink>(group);
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
    }
}

