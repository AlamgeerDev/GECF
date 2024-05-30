using System;
using System.Globalization;

namespace GECF.Converters
{
	public class BoolToImageConverter : IValueConverter
    {
		public BoolToImageConverter()
		{
		}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //value = System.Convert.ToInt32(value);
            //bool isFav = System.Convert.ToBoolean(value);
            //if (isFav)
            //    return "star_selected.png";
            //else
            //    return "star_unselected.png";

            if (value is bool)
            {
                if ((Boolean)value)
                    return "graph_view.png";
                else
                    return "table_view.png";
            }

            return "graph_view.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return "graph_view.png";
        }
    }
}

