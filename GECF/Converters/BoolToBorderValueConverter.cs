using System;
using System.Globalization;

namespace GECF.Converters
{
	public class BoolToBorderValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((Boolean)value)
                    return 0;
                else
                    return 2;
            }
            return 2;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return 2;
        }
    }
}

