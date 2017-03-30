using System;
using System.Globalization;
using System.Windows.Data;

namespace BPIDM.Utils
{
    class sizePercentConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            //[1] contains the ItemsControl.ActualWidth we binded to, [0] the percentage
            //In this case, I assume the percentage is a double between 0 and 1
            return (double)values[1] * (double)values[0];
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class colorToMDResourcePack : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return "pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor." + (string)values[0] + ".xaml";
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
