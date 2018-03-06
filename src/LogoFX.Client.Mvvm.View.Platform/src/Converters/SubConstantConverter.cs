using System;
using System.Globalization;
using System.Windows.Data;

namespace LogoFX.Client.Mvvm.View.Converters
{
    public class SubConstantConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                parameter = 0;

            Decimal dec = System.Convert.ToDecimal(value);

            return dec - System.Convert.ToDecimal(parameter);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (parameter == null)
                parameter = 0;

            return System.Convert.ToDecimal(value) + System.Convert.ToDecimal(parameter);
        }
    }
}
