//------------------------------------------------------------------------------ 
// 
// File provided for Reference Use Only by Handeck (c) 2023
// Copyright (c) Handeck. All rights reserved.
//
// Author: Gokhan Erdogdu
// 
//------------------------------------------------------------------------------
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace WpfSegoeFluentIconsFont.Converters
{
    [ValueConversion(typeof(bool), typeof(double))]
    public class BoolToOpacityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value is bool bVal) && bVal
                ? 1.0d
                : 0.5d;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
