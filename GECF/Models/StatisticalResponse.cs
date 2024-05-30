using System;
namespace GECF.Models
{
	public class StatisticalResponse
    {
        public List<DocLink> doc_links { get; set; }
        public int status { get; set; }

    }

    public class DocLink
    {
        public int id { get; set; }
        public string number { get; set; }
        public string Name { get; set; }
        public string extension { get; set; }
        public string url { get; set; }
        public string date { get; set; }
    }
}

