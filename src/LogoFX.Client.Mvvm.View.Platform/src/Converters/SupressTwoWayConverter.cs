﻿using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace LogoFX.Client.Mvvm.View.Converters
{
  /// <summary>
  /// Supplies OneWayToSource binding mode in otherwise unsupported platforms
  /// </summary>
  public class SupressTwoWayConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value;
    }
  }
}
