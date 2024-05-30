using System;
namespace GECF.Utility
{
	public class IConstants
	{
        public IConstants()
        {
        }

        public const string KeyPropertyPassword = "Password";
        public const string KeyPropertyAccessToken = "Token";
        public const string emailID = "emailID";
        public const string iStream = "iStream";
        public const string pwd = "pwd";
        public const int RequestTimeout = 40;
        private static bool _isUserLoggedIn = false;
        public static readonly Int32[] ValidYears = { 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020 };


        #region API Constants

        //PRODUCTION AND DEVELOPMENT SWITCH
        static bool _isProductionEnvironment = true;
        public static bool IsProductionEnvironment
        {
            get
            {
                return _isProductionEnvironment;
            }
            set
            {
                _isProductionEnvironment = value;
                RootURL = value ? "http://dailyapp.gecf.org/" : "http://apidev.gecf.local/";
            }
        }
        public static string RootURL = IsProductionEnvironment ? "http://dailyapp.gecf.org/" : "http://apidev.gecf.local/";
        public static string BaseURL = RootURL + "api/";

        public static string APIForLogin = "login";
        public static string APIForToken = "Token";
        #endregion



        #region API URL's


        #region Login Page Url's
        public static string urlForresetPwd = RootURL + "password/reset";

        public static string webNewsUrl = RootURL + "api/webnews?token=";
        #endregion

        public static string SearchNewsUrl = RootURL + "api/news/search";
        #region CatNews Url's

        public static string SearchUrl = RootURL + "api/news/search/populate?token=";

        public static string RegionSearchUrl = RootURL + "api/news/search/populate?";


        public static string cat1NewsUrl = RootURL + "api/news-group/category/1?token=";

        public static string cat2NewsUrl = RootURL + "api/news-group/category/2?token=";

        public static string cat3NewsUrl = RootURL + "api/news-group/category/3?token=";

        public static string cat4NewsUrl = RootURL + "api/news-group/category/4?token=";

        public static string cat5NewsUrl = RootURL + "api/news-group/category/5?token=";

        public static string cat7NewsUrl = RootURL + "api/news-group/category/7?token=";

        public static string cat8NewsUrl = RootURL + "api/news-group/category/8?token=";

        public static string cat9NewsUrl = RootURL + "api/news-group/category/9?token=";

        public static string cat10NewsUrl = RootURL + "api/news-group/category/10?token=";

        public static string cat12NewsUrl = RootURL + "api/news-group/category/11?token=";

        public static string cat13NewsUrl = RootURL + "api/news-group/category/14?token=";

        public static string catTodayNewsUrl = RootURL + "api/news/today?token=";

        public static string catLatestNewsUrl = RootURL + "api/news/latest?token=";


        #endregion



        #region Prices news Url's


        public static string PricesUrl = RootURL + "api/price?token=";

        public static string PricesLatestUrl = RootURL + "api/price?token=";

        public static string PricesWeeklyUrl = RootURL + "api/price/weekly?token=";

        public static string PricesMonthlyUrl = RootURL + "api/price/monthly?token=";


        /* RJMU New Prices 2022 */

        public static string PricesUrl2 = RootURL + "api/price2?token=";

        public static string PricesWeeklyUrl2 = RootURL + "api/price2/weekly?token=";

        public static string PricesMonthlyUrl2 = RootURL + "api/price2/monthly?token=";


        #endregion



        #region MacroEconomics Url's

        public static string MacroeconomicsPopulationUrl = RootURL + "api/populationf?token=";

        public static string MacroeconomicsGDPUrl = RootURL + "api/gdpf?token=";

        public static string MacroeconomicsExpGoodsandSerUrl = RootURL + "api/exportsf?token=";

        public static string MacroeconomicsImpGoodsandSerUrl = RootURL + "api/importsf?token=";

        public static string MacroeconomicsInflationUrl = RootURL + "api/inflationf?token=";

        public static string MacroeconomicsEnergyIntensityUrl = RootURL + "api/energy/intensityf?token=";

        public static string MacroeconomicsEnergyUseUrl = RootURL + "api/energy/usef?token=";

        public static string MacroeconomicsGasUseUrl = RootURL + "api/gasusef?token=";

        public static string MacroeconomicsLabourForceTotalUrl = RootURL + "api/laborforcef?token=";

        public static string MacroeconomicsChartPopulationUrl = RootURL + "api/population?token=";


        #endregion


        #region Statistical Reports Url's

        public static string IntheWorldGasProvenReservesUrl = RootURL + "api/gas/proven-reservesf?token=";

        public static string IntheWorldGasGrossProductionUrl = RootURL + "api/gas/gross-productionf?token=";

        public static string IntheWorldMarketProductionUrl = RootURL + "api/gas/market-productionf?token=";

        public static string IntheWorldPipelineExportsUrl = RootURL + "api/gas/pipeline-exportsf?token=";

        public static string IntheWorldLNGExportsUrl = RootURL + "api/gas/lng-exportsf?token=";

        public static string IntheWorldGasDomesticConsumptionUrl = RootURL + "api/gas/domestic-consumptionf?token=";


        #endregion


        #region Member Countries Url's

        public static string MemberCountriesMacroUrl = RootURL + "api/macrof/";

        public static string MemberCountriesProductionUrl = RootURL + "api/productionf/";

        public static string MemberCountriesDomesticConsumptionUrl = RootURL + "api/domestic-consumptionf/";

        public static string MemberCountriesTradeUrl = RootURL + "api/tradef/";


        #endregion


        #region NonGECFCountries Url's


        public static string NonGECFCountriesProductionUrl = RootURL + "api/productionNGf/";

        public static string NonGECFCountriesDomesticConsumptionUrl = RootURL + "api/domestic-consumptionNGf/";

        public static string NonGECFCountriesTradeUrl = RootURL + "api/tradeNGf/";


        public static string NonGECFCountriesProductionUrlV = RootURL + "api/productionNG/";

        public static string NonGECFCountriesDomesticConsumptionUrlV = RootURL + "api/domestic-consumptionNG/";

        public static string NonGECFCountriesTradeUrlV = RootURL + "api/tradeNG/";



        #endregion


        #region Statistical Reports Url's

        public static string StatisticalMSBUrl = RootURL + "api/msb?token=";

        public static string StatisticalQSBUrl = RootURL + "api/qsb?token=";

        public static string StatisticalASBUrl = RootURL + "api/asb?token=";

        public static string StatisticalRegUrl = RootURL + "api/gsr?token=";

        public static string StatisticalColUrl = RootURL + "api/sbc?token=";

        #endregion


        #region Gas Market Reports Url's

        public static string GasMarketDaillyUrl = RootURL + "api/market/daily?token=";

        public static string GasMarketWeeklyUrl = RootURL + "api/market/weekly?token=";

        public static string GasMarketMonthlyUrl = RootURL + "api/market/monthly?token=";

        #endregion


        #region GECFhelps Url's

        public static string HelpUrl = RootURL + "api/help?token=";

        #endregion

        #region GECFhelps Url's

        public static string AboutUrl = RootURL + "api/about?token=";

        #endregion

        #region NonGecf Countries Url's

        public static string NonGECFUrl = RootURL + "api/countries?token=";

        #endregion

        #region Gecf Outlooks Url's

        public static string GECOutlooksFUrl = RootURL + "api/outlooks?token=";

        #endregion

        #region Gecf Outlooks Url's

        public static string ExpertCommentariesUrl = RootURL + "api/webexpert?token=";

        #endregion


        #region NonGecf Countries Url's

        public static string DefinitionsFUrl = RootURL + "api/definition?token=";

        #endregion


        #endregion


        public static bool IsAPIResponseLogginNeeded = false;
    }
}

