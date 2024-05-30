using System;
//using AndroidX.Lifecycle;

namespace GECF.Models
{
    public class MacroeconomicsResponse
    {
        public List<Datum> data { get; set; }
        public int status { get; set; }

    }

    public class Filter
    {
        public string country { get; set; }
        public bool IsChecked { get; set; }

        public Filter(string country, bool IsChecked)
        {
            this.country = country;
            this.IsChecked = IsChecked;

        }
    }



    public class DatumArray
    {

        //public List<ViewModel.YearData> yearData { get; set; }
        public List<int> color { get; set; }
        public string country { get; set; }
        public string country_code { get; set; }
        public string country_type { get; set; }
    }




    public class Datum
    {
        public string country { get; set; }
        public string country_code { get; set; }
        public string country_type { get; set; }
        public string type { get; set; }
        public string type_code { get; set; }
        public List<int> color { get; set; }
        public string year1 { get; set; }
        public string value1 { get; set; }
        public string year2 { get; set; }
        public string value2 { get; set; }
        public string year3 { get; set; }
        public string value3 { get; set; }
        public string year4 { get; set; }
        public string value4 { get; set; }
        public string year5 { get; set; }
        public string value5 { get; set; }
        public string year6 { get; set; }
        public string value6 { get; set; }
        public string year7 { get; set; }
        public string value7 { get; set; }
        public string year8 { get; set; }
        public string value8 { get; set; }
    }
}

