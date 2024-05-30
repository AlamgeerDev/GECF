using System;
namespace GECF.Models
{
    public class Prices
    {
        public List<Price> price { get; set; }
        public int status { get; set; }

    }

    public class Prices2
    {
        public List<Price2> price { get; set; }
        public int status { get; set; }

    }

    public class WeeklyPrices
    {
        public List<WPrice> price { get; set; }
        public int status { get; set; }

    }

    public class WeeklyPrices2
    {
        public List<WPrice2> price { get; set; }
        public int status { get; set; }

    }


    public class MonthlyPrices
    {
        public List<MPrice> price { get; set; }
        public int status { get; set; }

    }

    public class MonthlyPrices2
    {
        public List<MPrice2> price { get; set; }
        public int status { get; set; }

    }


    public class Price2
    {
        public string id { get; set; }
        public string date { get; set; }
        public string Oil_Parity { get; set; }
        public string Japan_LT { get; set; }
        public string NEA { get; set; }
        public string OPEC { get; set; }
        public string NBP { get; set; }
        public string SWE { get; set; }
        public string HH { get; set; }
        public string Brent { get; set; }

        public string oilindex_1 { get; set; }
        public string oilindex_2 { get; set; }
        public string ttf { get; set; }
        public string jkm { get; set; }
        public string aeco { get; set; }

    }

    public class Price
    {
        public string id { get; set; }
        public string date { get; set; }
        public string Oil_Parity { get; set; }
        public string Japan_LT { get; set; }
        public string NEA { get; set; }
        public string OPEC { get; set; }
        public string NBP { get; set; }
        public string SWE { get; set; }
        public string HH { get; set; }
        public string Brent { get; set; }

    }

    public class WPrice
    {
        public string Week { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Oil_Parity { get; set; }
        public string Japan_LT { get; set; }
        public string NEA { get; set; }
        public string OPEC { get; set; }
        public string NBP { get; set; }
        public string SWE { get; set; }
        public string HH { get; set; }
        public string Brent { get; set; }
    }


    public class WPrice2
    {
        public string Week { get; set; }
        public string Month { get; set; }
        public string Year { get; set; }
        public string Oil_Parity { get; set; }
        public string Japan_LT { get; set; }
        public string NEA { get; set; }
        public string OPEC { get; set; }
        public string NBP { get; set; }
        public string SWE { get; set; }
        public string HH { get; set; }
        public string Brent { get; set; }

        public string oilindex_1 { get; set; }
        public string oilindex_2 { get; set; }
        public string ttf { get; set; }
        public string jkm { get; set; }
        public string aeco { get; set; }
    }

    public class MPrice
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public string Oil_Parity { get; set; }
        public string Japan_LT { get; set; }
        public string NEA { get; set; }
        public string OPEC { get; set; }
        public string NBP { get; set; }
        public string SWE { get; set; }
        public string HH { get; set; }
        public string Brent { get; set; }
    }


    public class MPrice2
    {
        public string Month { get; set; }
        public string Year { get; set; }
        public string Oil_Parity { get; set; }
        public string Japan_LT { get; set; }
        public string NEA { get; set; }
        public string OPEC { get; set; }
        public string NBP { get; set; }
        public string SWE { get; set; }
        public string HH { get; set; }
        public string Brent { get; set; }

        public string oilindex_1 { get; set; }
        public string oilindex_2 { get; set; }
        public string ttf { get; set; }
        public string jkm { get; set; }
        public string aeco { get; set; }

    }
}

