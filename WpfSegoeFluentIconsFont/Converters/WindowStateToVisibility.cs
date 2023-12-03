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
    [ValueConversion(typeof(WindowState), typeof(Visibility))]
    public class WindowStateToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            WindowState paramValue = parameter.ToString().ToEnum<WindowState>();

            return value is WindowState state && state == paramValue
                ? Visibility.Visible
                : Visibility.Collapsed;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
