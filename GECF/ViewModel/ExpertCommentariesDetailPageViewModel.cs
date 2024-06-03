using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using GECF.Models;
using GECF.Views;

namespace GECF.ViewModel
{
    public class ExpertCommentariesDetailPageViewModel : BaseViewModel
    {
        INavigation navigation;
        public ExpertCommentariesDetailPageViewModel(INavigation navigation, int currentItemIndex, List<NewsListing> CatNewsList)
        {
            this.navigation = navigation;
            this.CurrentNewsItemPos = currentItemIndex;

            if (CatNewsList != null)
            {

                if (CatNewsList.Count == 5)
                {
                    foreach (var item in CatNewsList)
                    {
                        item.category = "GECF News";
                    }
                }
                this.CatNewsList = new ObservableCollection<NewsListing>(CatNewsList);
            }

        }

        private NewsListing _news;
        public NewsListing News
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


        private ObservableCollection<NewsListing> _catNewsList;
        public ObservableCollection<NewsListing> CatNewsList
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

        public int _position = 0;
        public int Position
        {
            get
            {
                return _position;
            }
            set
            {
                _position = value;
                RaisePropertyChanged("Position");
            }
        }
        public ICommand NavCommand
        {
            get
            {
                //return new Command<string>((x) => HandleNavCommand(x), NavCommandcanExecute);

                return new Command(HandleNavCommand, NavCommandcanExecute);
            }
        }

        private async void HandleNavCommand(Object obj)
        {
            CanExecuteCommands = false;
            await NavAction(CatNewsList[Position].Path);
            CanExecuteCommands = true;
        }

        private bool NavCommandcanExecute(object arg)
        {
            return CanExecuteCommands;
        }

        public async Task NavAction(string url)
        {

            //InAppBrowsePage webBrowser = new InAppBrowsePage(newsString.url);

            if (url.Contains("https://www.gecf"))
            {
                url = url;
            }
            else
            {
                url = "https://www.gecf.org/events/" + url;
            }
            await navigation.PushModalAsync(new InAppBrowsePage(url));
        }


    }
}

