using System;
using System.Globalization;

namespace GECF.Converters
{
	public class BoolToRememberImageConverter: IValueConverter
    {
        public BoolToRememberImageConverter()
        {
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            if (value is bool)
            {
                if ((Boolean)value)
                    return "remember_on.png";
                else
                    return "remember_off.png";
            }

            return "remember_on.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "remember_on.png";
        }
    }
}

