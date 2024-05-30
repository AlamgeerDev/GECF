﻿using System;
using System.Globalization;

namespace GECF.Converters
{
	public class BoolToBackgroundColorConverter: IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
            {
                if ((Boolean)value)
                    return Colors.White;

                else
                    return Colors.Transparent;
            }
            return Colors.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Colors.Green;
        }
    }
}

