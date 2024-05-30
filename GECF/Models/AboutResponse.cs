using System;
namespace GECF.Models
{
    public class AboutResponse
    {
        public List<About> about { get; set; }
        public int status { get; set; }
    }

    public class About
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }
}

