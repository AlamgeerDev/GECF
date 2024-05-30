using System;
using System.Diagnostics;
using System.Text;
using GECF.Models;
using Newtonsoft.Json;

namespace GECF.Utility
{
	public class GECFAPI
	{
        public string token;
        private static GECFAPI mInstance = null;
        public static GECFAPI Instance
        {
            get
            {
                if (mInstance == null)
                    mInstance = new GECFAPI();
                return mInstance;
            }


        }

        public static async Task<Tuple<bool, string>> RefreshToken(string SuserName, string Spassword)
        {
            try
            {

                var resultApi = await Instance.DoLogin(SuserName, Spassword);



                return resultApi;



            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, null);
            }

            //return new Tuple<bool, string>(false, null);

        }

        /// <summary>
        /// Gets the Token
        /// </summary>
        /// <returns>The Token,which will be passed for every API call.</returns>
        /// <param name="email">Email.</param>
        ///

        public async Task<Tuple<bool, string>> DoLogin(string userName, string password)
        {

            string accessToken = "";
            bool isSuccess = false;
            int statusCode = 0;
            string responseString = string.Empty;
            string errorLog = string.Empty;
            try
            {

                var client = new HttpClient();
                var loginModel = new LoginModel(userName, password);
                var jsonData = JsonConvert.SerializeObject(loginModel);
                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var loginResponse = await client.PostAsync(IConstants.BaseURL + IConstants.APIForLogin, content);
                responseString = await loginResponse.Content.ReadAsStringAsync();


                if (responseString.Contains("Invalid Credentials"))
                {
                    accessToken = "Incorrect Email or Password entered";
                    isSuccess = false;

                }
                else
                {
                    LoginResponse tokenResponse = JsonConvert.DeserializeObject<LoginResponse>(responseString);
                    if (tokenResponse != null)
                    {
                        //ICUtility.mUser.AccessToken = tokenResponse.token;
                        Preferences.Set("AccessToken", tokenResponse.token);

                        isSuccess = true;
                        statusCode = tokenResponse.status;
                        //Application.Current.Properties[IConstants.KeyPropertyPassword] = password;
                        //Application.Current.Properties[IConstants.KeyPropertyAccessToken] = tokenResponse.token;
                        Preferences.Set(IConstants.KeyPropertyPassword, password);
                        Preferences.Set(IConstants.KeyPropertyPassword, password);
                        accessToken = tokenResponse.token;
                    }
                    else
                    {
                        Debug.WriteLine(tokenResponse, "Empty Response,  Null response");
                        accessToken = "Something went wrong. Please try again";
                        isSuccess = false;
                    }
                }


            }
            catch (Exception ex)
            {
                accessToken = "Something went wrong. Please try again";
                isSuccess = false;
                Debug.WriteLine(ex, ex.Message);
                var logParameters = new Dictionary<string, string>();
                logParameters.Add(IConstants.emailID, userName);
                logParameters.Add("Response String", responseString);
                //Analytics.TrackEvent(errorLog, logParameters);
            }

            return new Tuple<bool, string>(isSuccess, accessToken);


        }
        /// <summary>
        /// webnews api
        /// </summary>
        /// <returns></returns>

        public async Task<List<NewsListing>> GetWebNewsAsync()
        {
            List<NewsListing> newsItems = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var client = new HttpClient();

            token = Preferences.Get("token", string.Empty);
            //var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.webNewsUrl + token;
            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var webnews = JsonConvert.DeserializeObject<WebNews>(content);
                        newsItems = webnews.news_listing;


                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
                return newsItems;
            }
            return null;

        }

        /// <summary>
        /// Search Screen
        /// </summary>
        /// <returns></returns>
        public async Task<SearchResponse> GetSearchAsync()
        {
            SearchResponse search = new SearchResponse();
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.SearchUrl + token;

            try
            {

                if (token != null)
                {
                    var uri = new Uri(totalUrl);
                    Console.WriteLine(totalUrl);
                    try
                    {
                        var response = await client.GetAsync(uri);
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            search = JsonConvert.DeserializeObject<SearchResponse>(content);


                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(@"ERROR {0}", ex.Message);
                    }
                }

                Preferences.Set("search", JsonConvert.SerializeObject(search));
                return search;

            }
            catch (Exception e)
            {

            }

            return null;
        }




        public async Task<RegionsResponse> GetRegionsSearchAsync(string region)
        {
            RegionsResponse regionssearch = new RegionsResponse();
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.RegionSearchUrl + "region=" + region + "&" + "token=" + token;

            try
            {

                if (token != null)
                {
                    var uri = new Uri(totalUrl);
                    try
                    {
                        var response = await client.GetAsync(uri);
                        if (response.IsSuccessStatusCode)
                        {
                            var content = await response.Content.ReadAsStringAsync();
                            regionssearch = JsonConvert.DeserializeObject<RegionsResponse>(content);


                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(@"ERROR {0}", ex.Message);
                    }
                }

                Preferences.Set("regionssearch", JsonConvert.SerializeObject(regionssearch));
                return regionssearch;

            }
            catch (Exception e)
            {

            }

            return null;
        }



        /// <summary>
        /// serachNews
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task<List<News>> GetSearchNewsAsync(SearchModel searchModel)
        {

            List<CatNewsListing> cat1List = null;
            List<News> news = new List<News>();

            var fromdate = (long)searchModel.FromDate.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;
            var todate = (long)searchModel.ToDate.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds;


            //       (Int32)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;



            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);

            var datePart = "&from=  & to= ";
            if (fromdate > 0)
            {
                datePart = "&from=" + fromdate;
            }
            if (todate > 0 && fromdate > 0)
            {
                datePart = datePart + "&to=" + todate;
            }
            if (todate > 0 && fromdate < 0)
            {
                datePart = "&from = & to=" + todate;
            }

            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            //   var totalUrl = IConstants.SearchNewsUrl + "?" + "cat=" + searchModel.Category + "&country=" + searchModel.Country + "&region=" + searchModel.Region + "&from="+fromdate+ "&to=" +todate+"&s=" + searchModel.Keyword + "&count=" + searchModel.Count + "&token=" + token;

            var totalUrl = IConstants.SearchNewsUrl + "?" + "cat=" + searchModel.Category + "&country=" + searchModel.Country + "&region=" + searchModel.Region + datePart + "&s=" + searchModel.Keyword + "&count=" + searchModel.Count + "&token=" + token;

            Debug.WriteLine(totalUrl);
            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var newsListResponse = JsonConvert.DeserializeObject<NewsListResponse>(content);
                        cat1List = newsListResponse.news_listing;


                        if (cat1List != null)
                        {
                            foreach (var item in cat1List)
                            {
                                news.AddRange(item.news);
                            }
                        }
                        Preferences.Set("cat1List", news.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
                return news;
            }
            return null;
        }

        /// <summary>
        /// catagery 1
        /// </summary>
        /// <returns></returns>

        public async Task<List<News>> GetCatNewsAsync(int tag)
        {
            NewsListResponse newsListResponse = new NewsListResponse();
            List<CatNewsListing> cat1List = null;
            List<News> news = new List<News>();
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = string.Empty;

            if (tag == 1)
            {
                totalUrl = IConstants.cat1NewsUrl + token;
            }
            else if (tag == 2)
            {
                totalUrl = IConstants.cat2NewsUrl + token;
            }
            else if (tag == 3)
            {
                totalUrl = IConstants.cat3NewsUrl + token;
            }
            else if (tag == 4)
            {
                totalUrl = IConstants.cat4NewsUrl + token;
            }
            else if (tag == 5)
            {
                totalUrl = IConstants.cat5NewsUrl + token;
            }
            else if (tag == 7)
            {
                totalUrl = IConstants.cat7NewsUrl + token;
            }
            else if (tag == 8)
            {
                totalUrl = IConstants.cat8NewsUrl + token;
            }
            else if (tag == 9)
            {
                totalUrl = IConstants.cat9NewsUrl + token;
            }
            else if (tag == 10)
            {
                totalUrl = IConstants.cat10NewsUrl + token;
            }

            else if (tag == 12)
            {
                totalUrl = IConstants.cat12NewsUrl + token;
            }
            else if (tag == 13)
            {
                totalUrl = IConstants.catTodayNewsUrl + token;
            }
            else if (tag == 14)
            {
                totalUrl = IConstants.catLatestNewsUrl + token;
            }
            else if (tag == 15)
            {
                totalUrl = IConstants.cat13NewsUrl + token;
            }

            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        newsListResponse = JsonConvert.DeserializeObject<NewsListResponse>(content);
                        cat1List = newsListResponse.news_listing;


                        if (cat1List != null)
                        {
                            foreach (var item in cat1List)
                            {
                                news.AddRange(item.news);
                            }
                        }
                        Preferences.Set("cat1List", news.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
                return news;
            }
            return null;

        }


        /// <summary>
        /// supply and demand news
        /// </summary>
        /// <returns></returns>

        public async Task<List<DefinitionList>> GetDefinitionsNewsAsync()
        {
            List<DDatum> cat1List = null;
            List<DefinitionList> news = new List<DefinitionList>();
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.DefinitionsFUrl + token;
            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var newsListResponse = JsonConvert.DeserializeObject<DefinitionsResponse>(content);
                        cat1List = newsListResponse.data;


                        if (cat1List != null)
                        {
                            foreach (var item in cat1List)
                            {
                                news.AddRange(item.definition_list);
                            }
                        }
                        Preferences.Set("cat1List", news.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
                return news;
            }
            return null;

        }






        /// <summary>
        /// transport page news
        /// </summary>
        /// <returns></returns>

        public async Task<List<News>> GetCat3NewsAsync()
        {
            NewsListResponse newsListResponse = new NewsListResponse();
            List<CatNewsListing> cat1List = null;
            List<News> news = new List<News>();
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.cat3NewsUrl + token;
            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        newsListResponse = JsonConvert.DeserializeObject<NewsListResponse>(content);
                        cat1List = newsListResponse.news_listing;


                        if (cat1List != null)
                        {
                            foreach (var item in cat1List)
                            {
                                news.AddRange(item.news);
                            }
                        }
                        Preferences.Set("cat1List", news.ToString());
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
                return news;
            }
            return null;

        }


        /// <summary>
        /// Prices in Home Page
        /// </summary>
        /// <returns></returns>

        public async Task<List<Price>> GetPricesAsync()
        {
            List<Price> prices = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.PricesUrl + token;
            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Prices_Response = JsonConvert.DeserializeObject<Prices>(content);
                        prices = Prices_Response.price;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
                return prices;
            }
            return null;

        }

        public async Task<List<Price2>> GetPrices2Async()
        {
            List<Price2> prices = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.PricesUrl2 + token;
            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Prices_Response = JsonConvert.DeserializeObject<Prices2>(content);
                        prices = Prices_Response.price;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
                return prices;
            }
            return null;

        }


        public async Task<List<WPrice>> GetWeeklyPricesAsync()
        {
            List<WPrice> prices = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.PricesWeeklyUrl + token;
            if (token != null)
            {
                var uri = new Uri(totalUrl);
                Debug.WriteLine(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Prices_Response = JsonConvert.DeserializeObject<WeeklyPrices>(content);
                        prices = Prices_Response.price;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
                return prices;
            }
            return null;

        }

        public async Task<List<WPrice2>> GetWeeklyPrices2Async()
        {
            List<WPrice2> prices = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.PricesWeeklyUrl2 + token;
            if (token != null)
            {
                var uri = new Uri(totalUrl);
                Debug.WriteLine(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Prices_Response = JsonConvert.DeserializeObject<WeeklyPrices2>(content);
                        prices = Prices_Response.price;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
                return prices;
            }
            return null;

        }

        public async Task<List<MPrice>> GetMonthlyPricesAsync()
        {
            List<MPrice> prices = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.PricesMonthlyUrl + token;
            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Prices_Response = JsonConvert.DeserializeObject<MonthlyPrices>(content);
                        prices = Prices_Response.price;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
                return prices;
            }
            return null;

        }

        public async Task<List<MPrice2>> GetMonthlyPrices2Async()
        {
            List<MPrice2> prices = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.PricesMonthlyUrl2 + token;
            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Prices_Response = JsonConvert.DeserializeObject<MonthlyPrices2>(content);
                        prices = Prices_Response.price;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
                return prices;
            }
            return null;

        }


        public async Task<List<MDatum>> GetChartMacroeconomicsAsync(int tag)
        {
            string type = "population";
            List<MDatum> data = null;
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.MacroeconomicsChartPopulationUrl + token;
            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var newsListResponse = JsonConvert.DeserializeObject<MRootResponse>(content);
                        data = newsListResponse.data;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }


                return data;
            }
            return null;

        }


        /// <summary>
        /// Macroeconomics API
        /// </summary>
        /// <param name="country"></param>
        /// <returns></returns>
        public async Task<List<Datum>> GetMacroeconomicsAsync(int tag)
        {
            string type = "populationf";
            List<Datum> data = null;
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = string.Empty;

            if (tag == 1)
            {
                totalUrl = IConstants.MacroeconomicsPopulationUrl + token;
            }
            else if (tag == 2)
            {
                totalUrl = IConstants.MacroeconomicsGDPUrl + token;
            }
            else if (tag == 3)
            {
                totalUrl = IConstants.MacroeconomicsExpGoodsandSerUrl + token;
            }
            else if (tag == 4)
            {
                totalUrl = IConstants.MacroeconomicsImpGoodsandSerUrl + token;
            }
            else if (tag == 5)
            {
                totalUrl = IConstants.MacroeconomicsInflationUrl + token;
            }
            else if (tag == 6)
            {
                totalUrl = IConstants.MacroeconomicsEnergyIntensityUrl + token;
            }
            else if (tag == 7)
            {
                totalUrl = IConstants.MacroeconomicsEnergyUseUrl + token;
            }
            else if (tag == 8)
            {
                totalUrl = IConstants.MacroeconomicsGasUseUrl + token;
            }
            else if (tag == 9)
            {
                totalUrl = IConstants.MacroeconomicsLabourForceTotalUrl + token;
            }


            if (token != null)
            {
                var uri = new Uri(totalUrl);
                Debug.WriteLine(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var newsListResponse = JsonConvert.DeserializeObject<MacroeconomicsResponse>(content);
                        data = newsListResponse.data;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }


                return data;
            }
            return null;

        }

        /// <summary>
        /// Statistical Reports Api's
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>

        public async Task<List<DocLink>> GetStaticalRepotsAsync(int tag)
        {
            List<DocLink> docLinks = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = string.Empty;
            if (tag == 1)
            {
                totalUrl = IConstants.StatisticalMSBUrl + token;
            }
            else if (tag == 2)
            {
                totalUrl = IConstants.StatisticalQSBUrl + token;
            }
            else if (tag == 3)
            {
                totalUrl = IConstants.StatisticalASBUrl + token;
            }
            else if (tag == 4)
            {
                totalUrl = IConstants.StatisticalRegUrl + token;
            }
            else if (tag == 5)
            {
                totalUrl = IConstants.StatisticalColUrl + token;
            }


            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Prices_Response = JsonConvert.DeserializeObject<StatisticalResponse>(content);
                        docLinks = Prices_Response.doc_links;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
                return docLinks;
            }
            return null;

        }



        /// <summary>
        /// Gas Market Reports Api's
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>

        public async Task<List<DocLink>> GetGasMarketRepotsAsync(int tag)
        {
            List<DocLink> docLinks = null;
            // RestUrl = http://developer.xamarin.com:8081/api/todoitems
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = string.Empty;
            if (tag == 1)
            {
                totalUrl = IConstants.GasMarketDaillyUrl + token;
            }
            else if (tag == 2)
            {
                totalUrl = IConstants.GasMarketWeeklyUrl + token;
            }
            else
            {
                totalUrl = IConstants.GasMarketMonthlyUrl + token;
            }


            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var Prices_Response = JsonConvert.DeserializeObject<StatisticalResponse>(content);
                        docLinks = Prices_Response.doc_links;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }
                return docLinks;
            }
            return null;

        }



        /// <summary>
        /// In The World API's
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task<List<Datum>> GetIntheWorldAsync(int tag)
        {
            List<Datum> data = null;
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = string.Empty;

            if (tag == 1)
            {
                totalUrl = IConstants.IntheWorldGasProvenReservesUrl + token;
            }
            else if (tag == 2)
            {
                totalUrl = IConstants.IntheWorldGasGrossProductionUrl + token;
            }
            else if (tag == 3)
            {
                totalUrl = IConstants.IntheWorldMarketProductionUrl + token;
            }
            else if (tag == 4)
            {
                totalUrl = IConstants.IntheWorldPipelineExportsUrl + token;
            }
            else if (tag == 5)
            {
                totalUrl = IConstants.IntheWorldLNGExportsUrl + token;
            }
            else if (tag == 6)
            {
                totalUrl = IConstants.IntheWorldGasDomesticConsumptionUrl + token;
            }


            if (token != null)
            {
                var uri = new Uri(totalUrl);
                Debug.WriteLine(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var newsListResponse = JsonConvert.DeserializeObject<MacroeconomicsResponse>(content);
                        data = newsListResponse.data;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }


                return data;
            }
            return null;

        }


        /// <summary>
        /// Member Countries
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task<List<Datum>> GetMemberCountriesAsync(int tag, string countryCode)
        {

            List<Datum> data = null;
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = string.Empty;

            if (tag == 1)
            {
                totalUrl = IConstants.MemberCountriesMacroUrl + countryCode + "?token=" + token;
            }
            else if (tag == 2)
            {
                totalUrl = IConstants.MemberCountriesProductionUrl + countryCode + "?token=" + token;
            }
            else if (tag == 3)
            {
                totalUrl = IConstants.MemberCountriesDomesticConsumptionUrl + countryCode + "?token=" + token;
            }
            else if (tag == 4)
            {
                totalUrl = IConstants.MemberCountriesTradeUrl + countryCode + "?token=" + token;
            }


            if (token != null)
            {
                var uri = new Uri(totalUrl);
                Debug.WriteLine(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var newsListResponse = JsonConvert.DeserializeObject<MacroeconomicsResponse>(content);
                        data = newsListResponse.data;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }


                return data;
            }
            return null;

        }

        public async Task<string> GetHelpAsync()
        {
            token = Preferences.Get("token", string.Empty);
            var totalUrl = IConstants.HelpUrl + token;

            if (token != null)
            {
                var uri = new Uri(totalUrl);
                var client = new HttpClient();
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var newsListResponse = JsonConvert.DeserializeObject<HelpResponse>(content);
                        var item = newsListResponse.help[0];


                        return item.content;
                    }
                }

                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }

            }
            return null;
        }


        public async Task<string> GetAboutAsync()
        {
            token = Preferences.Get("token", string.Empty);
            var totalUrl = IConstants.AboutUrl + token;

            if (token != null)
            {
                var uri = new Uri(totalUrl);
                var client = new HttpClient();
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var newsListResponse = JsonConvert.DeserializeObject<AboutResponse>(content);
                        var item = newsListResponse.about[0];
                        return item.content;
                    }
                }

                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }

            }
            return null;
        }

        /// <summary>
        /// Non GECF Countries
        /// </summary>
        /// <returns></returns>
        public async Task<List<CountryList>> GetNonGECFAsync()
        {
            List<CountryList> data = null;
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.NonGECFUrl + token;

            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    Debug.WriteLine(totalUrl);
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var newsListResponse = JsonConvert.DeserializeObject<NonGECFCountriesResponse>(content);
                        data = newsListResponse.country_list;
                    }
                }

                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }


                return data;

            }

            return null;

        }

        /// <summary>
        /// Non GECF Countries
        /// </summary>
        /// <returns></returns>
        public async Task<List<DocLink>> GetGECFOutlooksAsync()
        {
            List<DocLink> data = null;
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.GECOutlooksFUrl + token;

            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var newsListResponse = JsonConvert.DeserializeObject<StatisticalResponse>(content);
                        data = newsListResponse.doc_links;
                    }
                }

                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }


                return data;

            }

            return null;

        }




        /// <summary>
        /// ExpertCommentaries
        /// </summary>
        /// <returns></returns>
        public async Task<List<NewsListing>> GetExpertCommentariesAsync()
        {
            List<NewsListing> data = null;
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = IConstants.ExpertCommentariesUrl + token;

            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {
                    var response = await client.GetAsync(uri);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var newsListResponse = JsonConvert.DeserializeObject<WebNews>(content);
                        data = newsListResponse.news_listing;
                    }
                }

                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }


                return data;

            }

            return null;

        }




        /// <summary>
        /// NonGECF Countries Data
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public async Task<List<NDatum>> GetNonGECFCountriesDataAsync(int tag, string countryCode)
        {

            List<NDatum> data = null;
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = string.Empty;

            if (tag == 1)
            {
                totalUrl = IConstants.NonGECFCountriesProductionUrl + countryCode + "?token=" + token;
            }
            else if (tag == 2)
            {
                totalUrl = IConstants.NonGECFCountriesDomesticConsumptionUrl + countryCode + "?token=" + token;
            }
            else if (tag == 3)
            {
                totalUrl = IConstants.NonGECFCountriesTradeUrl + countryCode + "?token=" + token;
            }


            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {

                    var response = await client.GetAsync(uri);
                    Debug.WriteLine(totalUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var newsListResponse = JsonConvert.DeserializeObject<NonGECFCountriesDataResponse>(content);
                        data = newsListResponse.data;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }

                return data;
            }
            return null;

        }

        public static async Task<List<NDatumV>> GetNonGECFCountriesDataAsyncV2(int tag, string countryCode)
        {

            List<NDatumV> data = null;
            var client = new HttpClient();
            var token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = string.Empty;

            if (tag == 1)
            {
                totalUrl = IConstants.NonGECFCountriesProductionUrlV + countryCode + "?token=" + token;
            }
            else if (tag == 2)
            {
                totalUrl = IConstants.NonGECFCountriesDomesticConsumptionUrlV + countryCode + "?token=" + token;
            }
            else if (tag == 3)
            {
                totalUrl = IConstants.NonGECFCountriesTradeUrlV + countryCode + "?token=" + token;
            }


            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {

                    var response = await client.GetAsync(uri);
                    Debug.WriteLine(totalUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var newsListResponse = JsonConvert.DeserializeObject<NonGECFCountriesDataResponseV>(content);
                        data = newsListResponse.data;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }

                return data;
            }
            return null;

        }

        public async Task<List<NDatumV>> GetNonGECFCountriesDataAsyncV(int tag, string countryCode)
        {

            List<NDatumV> data = null;
            var client = new HttpClient();
            token = Preferences.Get("token", string.Empty);
            // var token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiIxRjEyMDE3QS1BNjAxLTREM0YtQTM2Qy03MzQ3MDNCQTdGRkIiLCJpc3MiOiJodHRwOlwvXC9kYWlseWFwcC5nZWNmLm9yZ1wvYXBpXC9sb2dpbiIsImlhdCI6MTYwOTM5MjEzMSwiZXhwIjoxNjE0NTc2MTMxLCJuYmYiOjE2MDkzOTIxMzEsImp0aSI6ImE5ZjU5NWY0MTcwMmMwMTEzNzlmODk4Yjc5NDljMWE5In0.F_5WbhcGFqcenxMAnuF-t-0yA-cswQjYERJv3K-d36Q";//Preferences.Get("AccessToken",string.Empty);
            var totalUrl = string.Empty;

            if (tag == 1)
            {
                totalUrl = IConstants.NonGECFCountriesProductionUrlV + countryCode + "?token=" + token;
            }
            else if (tag == 2)
            {
                totalUrl = IConstants.NonGECFCountriesDomesticConsumptionUrlV + countryCode + "?token=" + token;
            }
            else if (tag == 3)
            {
                totalUrl = IConstants.NonGECFCountriesTradeUrlV + countryCode + "?token=" + token;
            }


            if (token != null)
            {
                var uri = new Uri(totalUrl);
                try
                {

                    var response = await client.GetAsync(uri);
                    Debug.WriteLine(totalUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var newsListResponse = JsonConvert.DeserializeObject<NonGECFCountriesDataResponseV>(content);
                        data = newsListResponse.data;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(@"ERROR {0}", ex.Message);
                }

                return data;
            }
            return null;

        }
    }
}

