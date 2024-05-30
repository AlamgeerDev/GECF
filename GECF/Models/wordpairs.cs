using System;
namespace GECF.Models
{
    public class wordpairs
    {
       
    }

public class MRootResponse
{
    public List<MDatum> data { get; set; }
    public int status { get; set; }
}

public class MDatum
{
    public string country { get; set; }
    public string country_code { get; set; }
    public string country_type { get; set; }
    public string type { get; set; }
    public string type_code { get; set; }
    public List<int> color { get; set; }
    public List<YearData> year_data { get; set; }
}
public class CountryGdp
{
    public string CountryName { get; }
    public IList<GdpValue> Values { get; }

    public CountryGdp(string country, params GdpValue[] values)
    {
        this.CountryName = country;
        this.Values = new List<GdpValue>(values);
    }
}

public class GdpValue
{
    public DateTime Year { get; }
    public string Value { get; }

    public GdpValue(DateTime year, string value)
    {
        this.Year = year;
        this.Value = value;
    }
}
public class YearData
{
    public string year { get; set; }
    public double value { get; set; }


}
}

