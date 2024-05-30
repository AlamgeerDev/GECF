using System;
using System.Globalization;

namespace GECF.Converters
{
	public class IntToColorConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
            {
                if (((int)value).Equals(0))
                    return Colors.Orange;
                else if (((int)value).Equals(2))
                    return Colors.Orange;


            }
            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

