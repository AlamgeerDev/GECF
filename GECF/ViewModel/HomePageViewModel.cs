using System;
//using static Android.Icu.Text.Transliterator;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;
using GECF.Models;
using GECF.Views;
using GECF.Utility;
using System.Diagnostics;
using GECF.Interfaces;

namespace GECF.ViewModel
{
    public class HomePageViewModel : BaseViewModel
    {

        public Action DisplayInvalidLoginPrompt;
        public ICommand SubmitCommand { protected set; get; }

        public INavigation navigation { get; set; }

        public HomePageViewModel(INavigation navigation)
        {
            SubmitCommand = new Command(OnSubmit);
            Prices = new ObservableCollection<Price2>();
            DoWebNewsAction();
            Position = 0;
            this.navigation = navigation;
            MyCommand = new Command(() =>
            {
                Debug.WriteLine("Position selected.");
            });
        }

        public async Task DoWebNewsAction()
        {
            var listwebNews = await GECFAPI.Instance.GetWebNewsAsync();
            CurrentNewsList = listwebNews;
            foreach (var item in listwebNews)
            {
                item.date = item.date + item.category;
            }
            var webNews = new ObservableCollection<NewsListing>(listwebNews);
            NewsListing = webNews;


        }

        private List<NewsListing> _currentNewsList;
        public List<NewsListing> CurrentNewsList
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

        public ObservableCollection<NewsListing> _newsListing;
        public ObservableCollection<NewsListing> NewsListing
        {
            get
            {
                return _newsListing;
            }
            set
            {
                _newsListing = value;
                RaisePropertyChanged("NewsListing");
            }
        }



        public async Task DoPriceAction()
        {

            IsProgressBarVisible = true;
            try
            {
                Prices.Clear();
                var listPrices = await GECFAPI.Instance.GetPrices2Async();
                ObservableCollection<Price2> sessions = new ObservableCollection<Price2>(listPrices);

                if (sessions.Count > 0 && sessions != null)
                {
                    int slimit = 0;
                    if (sessions.Count >= 8)
                        slimit = sessions.Count - 8;
                    else
                        slimit = sessions.Count;

                    // for (int i = sessions.Count-1 ; i >= sessions.Count-8; i--)
                    for (int i = sessions.Count - 1; i >= slimit; i--)
                    {
                        Price2 item = new Price2();
                        item = sessions[i];

                        item.Oil_Parity = Roundedvalue(item.Oil_Parity);
                        item.Japan_LT = Roundedvalue(item.Japan_LT);
                        item.NEA = Roundedvalue(item.NEA);
                        item.NBP = Roundedvalue(item.NBP);
                        item.HH = Roundedvalue(item.HH);
                        item.Brent = Roundedvalue(item.Brent);
                        item.SWE = Roundedvalue(item.SWE);
                        item.date = DateConversion(item.date);
                        item.oilindex_1 = Roundedvalue(item.oilindex_1);
                        item.oilindex_2 = Roundedvalue(item.oilindex_2);
                        item.ttf = Roundedvalue(item.ttf);
                        item.jkm = Roundedvalue(item.jkm);
                        item.aeco = Roundedvalue(item.aeco);

                        Prices.Add(item);

                    }

                    Prices = Prices;
                    IsProgressBarVisible = false;
                }
                else
                {
                    IsProgressBarVisible = false;
                    DependencyService.Get<IDialogService>().ShowErrorAlert("No Prices to display", "Sorry", "Ok");

                }
            }
            catch (Exception e)
            {
                IsProgressBarVisible = false;
                DependencyService.Get<IDialogService>().ShowErrorAlert("No Prices Data is Available", "Something Wrong", "Ok");
            }

        }


        /// <summary>
        /// isf there is anty error
        /// </summary>
        private bool _IsProgressBarVisible;
        public bool IsProgressBarVisible
        {
            get
            {
                return _IsProgressBarVisible;
            }
            set
            {
                _IsProgressBarVisible = value;
                RaisePropertyChanged("IsProgressBarVisible");
            }
        }




        private string DateConversion(string value)
        {


            value = value.Replace('-', '/');
            var date = DateTime.ParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            var formatDate = date.ToString("dd/MMM");
            return formatDate.ToString();

        }


        private string Roundedvalue(string value)
        {
            if (value.Length > 6)
            {
                value = value.Substring(0, 5);
            }
            else if (value.Length <= 0 || value.Equals(string.Empty))
            {
                value = "NA";
            }
            else
            {
                return value;
            }

            return value;

        }


        public ObservableCollection<Price2> _prices;
        public ObservableCollection<Price2> Prices
        {
            get
            {
                return _prices;
            }
            set
            {
                _prices = value;
                RaisePropertyChanged("Prices");
            }
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


        /// <summary>
        /// News Stack Visible Property
        /// </summary>
        private bool _isNewsStackVisible = true;
        public bool IsNewsStackVisible
        {
            get
            {

                return _isNewsStackVisible;

            }
            set
            {
                _isNewsStackVisible = value;
                RaisePropertyChanged("IsNewsStackVisible");
            }
        }


        /// <summary>
        /// Price stack Visible Property
        /// </summary>
        private bool _isPricesStackVisible = false;
        public bool IsPricesStackVisible
        {
            get
            {

                return _isPricesStackVisible;
            }
            set
            {
                _isPricesStackVisible = value;
                RaisePropertyChanged("IsPricesStackVisible");
            }
        }


        /// <summary>
        /// Statistics stack Visible Property
        /// </summary>
        private bool _isStatisticsStackVisible = false;
        public bool IsStatisticsStackVisible
        {
            get
            {

                return _isStatisticsStackVisible;

            }
            set
            {
                _isStatisticsStackVisible = value;
                RaisePropertyChanged("IsStatisticsStackVisible");
            }
        }


        /// <summary>
        /// Research stack Visible Property
        /// </summary>
        private bool _isResearchStackVisible = false;
        public bool IsResearchStackVisible
        {
            get
            {

                return _isResearchStackVisible;

            }
            set
            {
                _isResearchStackVisible = value;
                RaisePropertyChanged("IsResearchStackVisible");
            }
        }

        /// <summary>
        /// Button Property
        /// </summary>
        private bool _isEnabled;
        public bool IsEnabled
        {
            get
            {

                return _isEnabled;

            }
            set
            {
                _isEnabled = value;
                RaisePropertyChanged("IsEnabled");
            }
        }


        /// <summary>
        /// PricesNews Command
        /// </summary>

        public ICommand PricesNewsCommand
        {
            get
            {
                return new Command(HandlePricesNewsCommand, PricesNewsCommandcanExecute);
            }
        }

        private async void HandlePricesNewsCommand(object obj)
        {
            CanExecuteCommands = false;
            await PricesNewsAction();
            CanExecuteCommands = true;
        }

        private bool PricesNewsCommandcanExecute(object arg)
        {

            return CanExecuteCommands;
        }

        private async Task PricesNewsAction()
        {

            await navigation.PushModalAsync(new NewsListPage());

        }



        /// <summary>
        /// PolicyAndRegulationNews Command
        /// </summary>

        public ICommand PolicyAndRegulationCommand
        {
            get
            {
                return new Command(HandlePolicyAndRegulationNewsCommand, PolicyAndRegulationNewsCommandcanExecute);
            }
        }

        private async void HandlePolicyAndRegulationNewsCommand(object obj)
        {
            CanExecuteCommands = false;
            await SupplyamdDemandNewsAction();
            CanExecuteCommands = true;
        }

        private bool PolicyAndRegulationNewsCommandcanExecute(object arg)
        {

            return CanExecuteCommands;
        }

        private async Task PolicyAndRegulationNewsAction()
        {

            await navigation.PushModalAsync(new PolicyAndRegulationPage());

        }


        /// <summary>
        /// EconomicsAndGeopoliticsNews Command
        /// </summary>

        public ICommand EconomicsAndGeopoliticsCommand
        {
            get
            {
                return new Command(HandleEconomicsAndGeopoliticsNewsCommand, EconomicsAndGeopoliticsNewsCommandcanExecute);
            }
        }

        private async void HandleEconomicsAndGeopoliticsNewsCommand(object obj)
        {
            CanExecuteCommands = false;
            await EconomicsAndGeopoliticsNewsAction();
            CanExecuteCommands = true;
        }

        private bool EconomicsAndGeopoliticsNewsCommandcanExecute(object arg)
        {

            return CanExecuteCommands;
        }

        private async Task EconomicsAndGeopoliticsNewsAction()
        {

            await navigation.PushModalAsync(new EconomicsAndGeoPoliticsPage());

        }



        /// <summary>
        /// TechnologyNews Command
        /// </summary>

        public ICommand TechnologyCommand
        {
            get
            {
                return new Command(HandleTechnologyNewsCommand, TechnologyCommandNewscanExecute);
            }
        }

        private async void HandleTechnologyNewsCommand(object obj)
        {
            CanExecuteCommands = false;
            await TechnologyNewsAction();
            CanExecuteCommands = true;
        }

        private bool TechnologyCommandNewscanExecute(object arg)
        {

            return CanExecuteCommands;
        }

        private async Task TechnologyNewsAction()
        {

            await navigation.PushModalAsync(new TechnologyPage());

        }



        public ICommand SearchCommand
        {
            get
            {
                return new Command(HandleSearchNewsCommand, SearchCommandNewscanExecute);
            }
        }

        private async void HandleSearchNewsCommand(object obj)
        {
            CanExecuteCommands = false;
            await SearchNewsAction();
            CanExecuteCommands = true;
        }

        private bool SearchCommandNewscanExecute(object arg)
        {

            return CanExecuteCommands;
        }

        private async Task SearchNewsAction()
        {

            await navigation.PushModalAsync(new TechnologyPage());

        }

        /// <summary>
        /// SupplyamdDemandNews Command
        /// </summary>

        public ICommand SupplyamdDemandNewsCommand
        {
            get
            {
                return new Command(HandlePSupplyamdDemandNewsCommand, SupplyamdDemandNewsCommandcanExecute);
            }
        }

        private async void HandlePSupplyamdDemandNewsCommand(object obj)
        {
            CanExecuteCommands = false;
            await SupplyamdDemandNewsAction();
            CanExecuteCommands = true;
        }

        private bool SupplyamdDemandNewsCommandcanExecute(object arg)
        {

            return CanExecuteCommands;
        }

        private async Task SupplyamdDemandNewsAction()
        {

            await navigation.PushModalAsync(new SupplyAndDemandNewsPage());

        }


        /// <summary>
        /// EnvironmentNews Command
        /// </summary>

        public ICommand EnvironmentNewsCommand
        {
            get
            {
                return new Command(HandleEnvironmentNewsCommand, EnvironmentNewsCommandcanExecute);
            }
        }

        private async void HandleEnvironmentNewsCommand(object obj)
        {
            CanExecuteCommands = false;
            await EnvironmentNewsAction();
            CanExecuteCommands = true;
        }

        private bool EnvironmentNewsCommandcanExecute(object arg)
        {

            return CanExecuteCommands;
        }

        private async Task EnvironmentNewsAction()
        {

            await navigation.PushModalAsync(new SupplyAndDemandNewsPage());

        }



        /// <summary>
        /// TradeNews Command
        /// </summary>

        public ICommand TradeNewsCommand
        {
            get
            {
                return new Command(HandleTradeNewsCommand, TradeNewsCommandcanExecute);
            }
        }

        private async void HandleTradeNewsCommand(object obj)
        {
            CanExecuteCommands = false;
            await TradeNewsAction();
            CanExecuteCommands = true;
        }

        private bool TradeNewsCommandcanExecute(object arg)
        {

            return CanExecuteCommands;
        }

        private async Task TradeNewsAction()
        {

            await navigation.PushModalAsync(new TradePage());

        }


        /// <summary>
        /// AssetsAndInvestmentNews Command
        /// </summary>

        public ICommand AssetsAndInvestmentNewsCommand
        {
            get
            {
                return new Command(HandleAssetsAndInvestmentNewsCommand, AssetsAndInvestmentNewsCommandcanExecute);
            }
        }

        private async void HandleAssetsAndInvestmentNewsCommand(object obj)
        {
            CanExecuteCommands = false;
            await AssetsAndInvestmentNewsAction();
            CanExecuteCommands = true;
        }

        private bool AssetsAndInvestmentNewsCommandcanExecute(object arg)
        {

            return CanExecuteCommands;
        }

        private async Task AssetsAndInvestmentNewsAction()
        {

            await navigation.PushModalAsync(new AssetsAndInvestmentPage());

        }



        /// <summary>
        /// PriceCommentarymentNews Command
        /// </summary>

        public ICommand PriceCommentaryNewsCommand
        {
            get
            {
                return new Command(HandlePriceCommentaryNewsCommand, PriceCommentaryNewsCommandcanExecute);
            }
        }

        private async void HandlePriceCommentaryNewsCommand(object obj)
        {
            CanExecuteCommands = false;
            await PriceCommentaryNewsAction();
            CanExecuteCommands = true;
        }

        private bool PriceCommentaryNewsCommandcanExecute(object arg)
        {

            return CanExecuteCommands;
        }

        private async Task PriceCommentaryNewsAction()
        {

            await navigation.PushModalAsync(new PriceCommentaryPage());

        }


        /// <summary>
        /// TodaysNews Command
        /// </summary>

        public ICommand TodaysNewsCommand
        {
            get
            {
                return new Command(HandleTodaysNewsCommand, TodaysNewsCommandcanExecute);
            }
        }

        private async void HandleTodaysNewsCommand(object obj)
        {
            CanExecuteCommands = false;
            await TodaysNewsAction();
            CanExecuteCommands = true;
        }

        private bool TodaysNewsCommandcanExecute(object arg)
        {

            return CanExecuteCommands;
        }

        private async Task TodaysNewsAction()
        {

            await navigation.PushModalAsync(new TodaysPage());

        }


        /// <summary>
        /// ListNews Command
        /// </summary>

        public ICommand ListNewsCommand
        {
            get
            {
                return new Command(HandleListNewsCommand, ListNewsCommandcanExecute);
            }
        }

        private async void HandleListNewsCommand(object obj)
        {
            CanExecuteCommands = false;
            await ListNewsAction();
            CanExecuteCommands = true;
        }

        private bool ListNewsCommandcanExecute(object arg)
        {

            return CanExecuteCommands;
        }

        private async Task ListNewsAction()
        {

            await navigation.PushModalAsync(new LatestPage());

        }





        /// <summary>
        ///  TransportNews Command
        /// </summary>

        public ICommand TransportNewsCommand
        {
            get
            {
                return new Command(HandleTransportNewsCommand, TransportNewsCommandcanExecute);
            }
        }

        private async void HandleTransportNewsCommand(object obj)
        {
            CanExecuteCommands = false;
            await TransportNewsAction();
            CanExecuteCommands = true;
        }

        private bool TransportNewsCommandcanExecute(object arg)
        {

            return CanExecuteCommands;
        }

        private async Task TransportNewsAction()
        {

            await navigation.PushModalAsync(new TransportationPage());

        }


        /// <summary>
        /// News Command
        /// </summary>
        public ICommand NewsCommand
        {
            get
            {
                return new Command(HandleNewsCommand, NewsCommandcanExecute);
            }
        }

        private async void HandleNewsCommand(object obj)
        {
            CanExecuteCommands = false;
            await NewsAction();
            CanExecuteCommands = true;
        }

        private bool NewsCommandcanExecute(object arg)
        {
            return CanExecuteCommands;
        }

        private async Task NewsAction()
        {
            IsNewsStackVisible = true;
            IsPricesStackVisible = false;
            IsStatisticsStackVisible = false;
            IsResearchStackVisible = false;

        }

        public void OnSubmit()
        {
            DisplayInvalidLoginPrompt();
        }

        /// <summary>
        /// Prices Command
        /// </summary>

        public ICommand PricesCommand
        {
            get
            {
                return new Command(HandlePricesCommand, PricesCommandcanExecute);
            }
        }

        private async void HandlePricesCommand(object obj)
        {
            CanExecuteCommands = false;
            await PricesAction();
            CanExecuteCommands = true;
        }

        private bool PricesCommandcanExecute(object arg)
        {

            return CanExecuteCommands;
        }

        private async Task PricesAction()
        {
            IsNewsStackVisible = false;
            IsPricesStackVisible = true;
            IsStatisticsStackVisible = false;
            IsResearchStackVisible = false;
            //if(Prices==null)
            await DoPriceAction();

        }


        /// <summary>
        /// Statistics Command
        /// </summary>

        public ICommand StatisticsCommand
        {
            get
            {
                return new Command(HandleStatisticsCommand, StatisticsCommandcanExecute);
            }
        }

        private async void HandleStatisticsCommand(object obj)
        {
            CanExecuteCommands = false;
            await StatisticsAction();
            CanExecuteCommands = true;
        }

        private bool StatisticsCommandcanExecute(object arg)
        {
            return CanExecuteCommands;
        }

        private async Task StatisticsAction()
        {

            IsNewsStackVisible = false;
            IsPricesStackVisible = false;
            IsStatisticsStackVisible = true;
            IsResearchStackVisible = false;

        }


        /// <summary>
        /// Reserch Command
        /// </summary>
        ///

        public ICommand ResearchCommand
        {
            get
            {
                return new Command(HandleResearchCommand, ResearchCommandcanExecute);
            }
        }

        private async void HandleResearchCommand(object obj)
        {
            CanExecuteCommands = false;
            await ReserchAction();
            CanExecuteCommands = true;
        }

        private bool ResearchCommandcanExecute(object arg)
        {
            return CanExecuteCommands;
        }

        private async Task ReserchAction()
        {
            IsNewsStackVisible = false;
            IsPricesStackVisible = false;
            IsStatisticsStackVisible = false;
            IsResearchStackVisible = true;

        }

        public Command MyCommand { protected set; get; }



        /// <summary>
        /// News Navigation Command
        /// </summary>
        public ICommand NewsNavCommand
        {
            get
            {
                return new Command<string>((x) => HandleNewsNavCommand(x), NewsNavcanExecute);
            }
        }

        private async void HandleNewsNavCommand(string obj)
        {
            CanExecuteCommands = false;
            IsEnabled = false;
            await NewsNavAction(obj);
            IsEnabled = true;
            CanExecuteCommands = true;
        }

        private bool NewsNavcanExecute(string arg)
        {

            return CanExecuteCommands;
        }

        private async Task NewsNavAction(string tag)
        {

            if (tag == "1")
            {
                await navigation.PushModalAsync(new NewsListPage());

            }
            else if (tag == "2")
            {
                await navigation.PushModalAsync(new SupplyAndDemandNewsPage());

            }
            else if (tag == "3")
            {
                await navigation.PushModalAsync(new TransportationPage());

            }
            else if (tag == "4")
            {
                await navigation.PushModalAsync(new PolicyAndRegulationPage());

            }
            else if (tag == "5")
            {
                await navigation.PushModalAsync(new EconomicsAndGeoPoliticsPage());

            }
            else if (tag == "6")
            {
                await navigation.PushModalAsync(new TechnologyPage());

            }
            else if (tag == "7")
            {
                await navigation.PushModalAsync(new EnvironmentPage());

            }
            else if (tag == "8")
            {
                await navigation.PushModalAsync(new TradePage());

            }
            else if (tag == "9")
            {
                await navigation.PushModalAsync(new AssetsAndInvestmentPage());

            }
            else if (tag == "10")
            {
                await navigation.PushModalAsync(new PriceCommentaryPage());

            }
            else if (tag == "11")
            {
                await navigation.PushModalAsync(new TodaysPage());

            }
            else if (tag == "12")
            {
                await navigation.PushModalAsync(new LatestPage());

            }
            else if (tag == "13")
            {
                await navigation.PushModalAsync(new HydrogenPage());

            }
        }


        public ICommand PricesNavCommand
        {
            get
            {
                return new Command(HandlePricesNavCommand, PricesNavCommandcanExecute);
            }
        }

        private async void HandlePricesNavCommand(object obj)
        {
            CanExecuteCommands = false;
            await PricesNavAction();
            CanExecuteCommands = true;
        }

        private bool PricesNavCommandcanExecute(object arg)
        {
            return CanExecuteCommands;
        }


        public async Task PricesNavAction()
        {

            await navigation.PushModalAsync(new PricesPage2());

        }


        /// <summary>
        /// Statistics Navigation Command
        /// </summary>
        public ICommand StatisticsNavCommand
        {
            get
            {
                return new Command<string>((x) => HandleStatisticsNavCommand(x), StatisticsNavcanExecute);

            }
        }

        private async void HandleStatisticsNavCommand(string obj)
        {
            CanExecuteCommands = false;
            IsEnabled = false;
            await StatisticsNavAction(obj);
            IsEnabled = true;
            CanExecuteCommands = true;
        }

        private bool StatisticsNavcanExecute(string arg)
        {

            return CanExecuteCommands;
        }

        private async Task StatisticsNavAction(string tag)
        {

            if (tag == "1")
            {
                await navigation.PushModalAsync(new MacroeconomicsPage());

            }
            else if (tag == "2")
            {
                await navigation.PushModalAsync(new InTheWorldPage());

            }
            else if (tag == "3")
            {
                await navigation.PushModalAsync(new MemberCountriesIconsPage());

            }
            else if (tag == "4")
            {
                await navigation.PushModalAsync(new ObserverCountriesPage());

            }
            else if (tag == "5")
            {
                await navigation.PushModalAsync(new NonGECFCountriesListPage());

            }
            else if (tag == "6")
            {
                await navigation.PushModalAsync(new DefinitionsPage());

            }

        }



        /// <summary>
        /// Research Navigation Command
        /// </summary>
        public ICommand ResearchNavCommand
        {
            get
            {
                return new Command<string>((x) => HandleResearchNavCommand(x), ResearchNavcanExecute);
            }
        }

        private async void HandleResearchNavCommand(string obj)
        {
            CanExecuteCommands = false;
            IsEnabled = false;
            await ResearchNavAction(obj);
            IsEnabled = true;
            CanExecuteCommands = true;
        }

        private bool ResearchNavcanExecute(string arg)
        {

            return CanExecuteCommands;
        }
        private async Task ResearchNavAction(string tag)
        {

            if (tag == "1")
            {
                await navigation.PushModalAsync(new StatisticalReportsPage());

            }
            else if (tag == "2")
            {
                await navigation.PushModalAsync(new GasMarketReportsPage());

            }
            else if (tag == "3")
            {
                await navigation.PushModalAsync(new ExpertCommentriesPage());

            }
            else if (tag == "4")
            {
                await navigation.PushModalAsync(new GECFOutlooksPage());

            }

        }

        public ICommand CarouselNavCommand
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

            //await navigation.PushModalAsync(new ExpertCommentariesDetailPage(Position, CurrentNewsList));
        }


    }
}

