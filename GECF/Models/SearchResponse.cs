using System;
namespace GECF.Models
{
	public class SearchResponse
	{
        public List<string> countries { get; set; }
        public List<string> regions { get; set; }
        public List<string> categories { get; set; }
        public int status { get; set; }
    }
}

