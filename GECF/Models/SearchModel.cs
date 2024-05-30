using System;
namespace GECF.Models
{
	public class SearchModel
	{
        public string Keyword { set; get; }
        public string Region { set; get; }
        public string Country { set; get; }
        public string Category { set; get; }
        public string Count { set; get; }
        public DateTime FromDate { set; get; }
        public DateTime ToDate { set; get; }
    }
}

