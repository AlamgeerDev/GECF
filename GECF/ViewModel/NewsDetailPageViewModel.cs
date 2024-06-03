using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GECF.Models;

namespace GECF.ViewModel
{
	public class NewsDetailPageViewModel:BaseViewModel
    {
        INavigation navigation;
        public NewsDetailPageViewModel(INavigation navigation, int currentItemIndex, List<News> CatNewsList)
		{
            this.navigation = navigation;
            this.CurrentNewsItemPos = currentItemIndex;

            if (CatNewsList != null)
            {
                foreach (var item in CatNewsList)
                {
                    item.content = item.content.Replace("                ", "");
                    item.content = item.content.Replace("  ", "");
                }
            }
            this.CatNewsList = new ObservableCollection<News>(CatNewsList);
        }

        private News _news;
        public News News
        {
            get
            {
                return _news;
            }

            set
            {
                _news = value;
                RaisePropertyChanged("News");
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


        private ObservableCollection<News> _catNewsList;
        public ObservableCollection<News> CatNewsList
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


        private int _no_Of_Lines_Count;
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

        /// <summary>
        /// Increment Command
        /// </summary>
        public ICommand IncrementCommand
        {
            get
            {
                return new Command(HandleIncrementCommand, IncrementCommandExcute);
            }
        }

        private async void HandleIncrementCommand(Object obj)
        {
            CanExecuteCommands = false;
            await DoIncrementAction();
            CanExecuteCommands = true;


        }

        private bool IncrementCommandExcute(object args)
        {
            return CanExecuteCommands;
        }

        private async Task DoIncrementAction()
        {
            if (CurrentNewsItemPos == CatNewsList.Count)
            {
                return;

            }

            else if (CurrentNewsItemPos <= CatNewsList.Count)
            {
                CurrentNewsItemPos++;

            }

        }



        /// <summary>
        /// Decrement Command
        /// </summary>
        public ICommand DecrementCommand
        {
            get
            {
                return new Command(HandleDecrementCommand, DecrementCommandExcute);
            }
        }

        private async void HandleDecrementCommand(Object obj)
        {
            CanExecuteCommands = false;
            await DoDecrementAction();
            CanExecuteCommands = true;


        }

        private bool DecrementCommandExcute(object args)
        {
            return CanExecuteCommands;
        }

        private async Task DoDecrementAction()
        {
            if (CatNewsList.Count == 0)
            {
                return;
            }

            else if (CatNewsList.Count >= 1)
            {
                CurrentNewsItemPos--;
            }

        }
    }
}

