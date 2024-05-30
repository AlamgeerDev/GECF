using System;
namespace GECF.Models
{
    public class ExpertCommentariesResponse
    {
        public List<ENewsListing> news_listing { get; set; }
        public int status { get; set; }
    }

    public class ENewsListing
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public string category { get; set; }
        public string Path { get; set; }
        public string date { get; set; }
    }
}

