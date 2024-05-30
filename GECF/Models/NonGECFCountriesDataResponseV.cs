using System;
namespace GECF.Models
{
    public class NonGECFCountriesDataResponseV
    {
        public List<NDatumV> data { get; set; }
        public int status { get; set; }

    }


    public class NDatumV
    {

        public string country;
        public string country_code;
        public string type;
        public string type_code;
        public List<int> color;
        public List<NonYear> year_data;


    }
}

