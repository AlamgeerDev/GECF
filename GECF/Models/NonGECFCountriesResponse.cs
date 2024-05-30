using System;
namespace GECF.Models
{
    public class NonGECFCountriesResponse
    {
        public List<CountryList> country_list { get; set; }
        public int status { get; set; }
    }

    public class CountryList
    {
        public int id { get; set; }
        public string ISOA3 { get; set; }
        public string ISOA2 { get; set; }
        public string name { get; set; }
        public string nameLong { get; set; }
        public string GECF { get; set; }
        public string RegionUN { get; set; }

    }
}

