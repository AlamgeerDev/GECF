using System;
namespace GECF.Models
{
    public class HelpResponse
    {
        public List<Help> help { get; set; }
        public int status { get; set; }
    }
    public class Help
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
    }
}

