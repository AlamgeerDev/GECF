using System;
using System.Collections.ObjectModel;
using GECF.Models;

namespace GECF.Services
{
	public class PickerService
	{
        public static ObservableCollection<PickerClass> GetStatisticalPickers()
        {
            var statisticalPickers = new ObservableCollection<PickerClass>
                {
                    new PickerClass(){key=1,value="Monthly"} ,
                     new PickerClass(){key=1,value="Quarterly"} ,
                      new PickerClass(){key=1,value="Annual"} ,
                      new PickerClass(){key=1,value="Regional"} ,
                      new PickerClass(){key=1,value="Collections"} ,
                };

            return statisticalPickers;
        }

        public static ObservableCollection<PickerClass> GetGasMarketReportsPickers()
        {
            var statisticalPickers = new ObservableCollection<PickerClass>
                {
                    new PickerClass(){key=1,value="Daily"} ,
                     new PickerClass(){key=1,value="Weekly"} ,
                      new PickerClass(){key=1,value="Monthly"} ,
                };

            return statisticalPickers;
        }

        public static ObservableCollection<PickerClass> GetPricesPickers()
        {
            var pricesPickers = new ObservableCollection<PickerClass>
                {
                    new PickerClass(){key=1,value="Last 20 days"} ,
                     new PickerClass(){key=1,value="Weekly Average"} ,
                      new PickerClass(){key=1,value="Monthly Average"} ,
                };

            return pricesPickers;
        }



        public static ObservableCollection<PickerClass> GetMacroEconomicsPickers()
        {
            var statisticalPickers = new ObservableCollection<PickerClass>
                {
                    new PickerClass(){key=1,value="Population"},
                    new PickerClass(){key=1,value="GDP"},
                    new PickerClass(){key=1,value="Exports Of Goods & Services"},
                    new PickerClass(){key=1,value="Imports Of Goods & Services"},
                    new PickerClass(){key=1,value="Inflation"},
                    new PickerClass(){key=1,value="Energy Intensity"},
                    new PickerClass(){key=1,value="Energy Use"},
                    new PickerClass(){key=1,value="Gas Use"},
                    new PickerClass(){key=1,value="Labour Force Total"},
                };
            return statisticalPickers;
        }

        public static ObservableCollection<PickerClass> GetMacroIntheWorldPickers()
        {
            var statisticalPickers = new ObservableCollection<PickerClass>
                {
                    new PickerClass(){key=1,value="Gas Proven Reserves"},
                    new PickerClass(){key=1,value="Natural Gas Gross Production"},
                    new PickerClass(){key=1,value="Natural Gas Marketed Production"},
                    new PickerClass(){key=1,value="Natural Gas Pipeline Exports"},
                    new PickerClass(){key=1,value="Natural Gas LNG Exports"},
                    new PickerClass(){key=1,value="Natural Gas Domestic Consumption"},

                };
            return statisticalPickers;
        }


        public static ObservableCollection<PickerClass> GetMemberCountriesPickers()
        {
            var statisticalPickers = new ObservableCollection<PickerClass>
                {
                    new PickerClass(){key=1,value="Macroeconomics"} ,
                    new PickerClass(){key=1,value="Production"} ,
                    new PickerClass(){key=1,value="Domestic Consumption"} ,
                    new PickerClass(){key=1,value="Trade"} ,
                };

            return statisticalPickers;
        }

        public static ObservableCollection<PickerClass> GetNonGECFCountriesPickers()
        {
            var statisticalPickers = new ObservableCollection<PickerClass>
                {
                    new PickerClass(){key=1,value="Production"} ,
                    new PickerClass(){key=1,value="Domestic Consumption"} ,
                    new PickerClass(){key=1,value="Trade"} ,
                };

            return statisticalPickers;
        }
    }
}

