using System;
namespace GECF.Models
{
    public class NewsListResponse
    {
        public List<CatNewsListing> news_listing { get; set; }
        public int status { get; set; }

    }
    public class News
    {
        public object id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string category { get; set; }
        public string date { get; set; }
    }

    public class CatNewsListing
    {
        public string date { get; set; }
        public List<News> news { get; set; }
    }

    public class SNewsListResponse
    {
        public List<SCatNewsListing> news_listing { get; set; }
        public int status { get; set; }

    }
    public class SNews
    {
        public object id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string category { get; set; }
        public string date { get; set; }
    }
    public class SCatNewsListing
    {
        public string date { get; set; }
        public List<SNews> news { get; set; }
    }
}

