﻿using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace LogoFX.Client.Mvvm.View.Converters
{
    /// <summary>
    /// Compares between the provided value and parameter and returns the comparison result.
    /// </summary>
    /// <seealso cref="IValueConverter" />
    public class EqualsToBooleanConverter : IValueConverter
    {
        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null && parameter != null)
            {
                return false;
            }

            if (value != null && parameter == null)
            {
                return false;
            }

            if (value == null && parameter == null)
            {
                return true;
            }

            object compareTo = null;

            if (value is Enum)
            {
                try
                {
                    compareTo = Enum.Parse(value.GetType(), (string)parameter, false);
                }

                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("EqualsToBooleanConverter" + ex);
                }
            }
            else
            {
#if !WinRT
                compareTo = (from TypeConverterAttribute customAttribute in value.GetType().GetCustomAttributes(typeof(TypeConverterAttribute), true)
                             select (TypeConverter)Activator.CreateInstance(Type.GetType(customAttribute.ConverterTypeName))
                                 into tc
                                 where tc.CanConvertFrom(typeof(string))
                                 select tc.ConvertFrom(parameter)).FirstOrDefault();
#endif
            }


            if (value.Equals(compareTo))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Converts a value.
        /// </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>
        /// A converted value. If the method returns null, the valid null value is used.
        /// </returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}