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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;

namespace WpfSegoeFluentIconsFont.Converters
{
    [ValueConversion(typeof(ListItemModel), typeof(FlowDocument))]
    public class ListItemModel2FlowDocument : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is ListItemModel item))
            {
                return new FlowDocument();
            }

            FlowDocument doc = new FlowDocument();
            Paragraph paragraph = new Paragraph() { Margin = new System.Windows.Thickness(8) };

            paragraph.Inlines.Add(new LineBreak());
            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.Green,
                Text = "<!--If the font is installed on your computer-->"
            });
            paragraph.Inlines.Add(new LineBreak());

            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.Blue,
                Text = "<TextBlock "
            });

            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.Red,
                Text = "\r\n    FontFamily"
            });
            paragraph.Inlines.Add("=");
            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.DarkMagenta,
                Text = "\"Segoe Fluent Icons\" "
            });

            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.Red,
                Text = "\r\n    FontSize"
            });
            paragraph.Inlines.Add("=");
            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.DarkMagenta,
                Text = "\"32\" "
            });

            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.Red,
                Text = "\r\n    Text"
            });
            paragraph.Inlines.Add("=");
            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.DarkMagenta,
                Text = $"\"&#x{item.UnicodeValue};\" "
            });

            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.Blue,
                Text = "/>"
            });

            paragraph.Inlines.Add(new LineBreak());
            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.Green,
                Text = "<!--Or if the font is embedded in resources.-->"
            });
            paragraph.Inlines.Add(new LineBreak());
            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.Green,
                Text = "<!--See: SegoeFluentIcons.xaml-->"
            });
            paragraph.Inlines.Add(new LineBreak());

            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.Blue,
                Text = "<TextBlock "
            });

            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.Red,
                Text = "\r\n    FontFamily"
            });
            paragraph.Inlines.Add("=");
            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.DarkMagenta,
                Text = "\"{StaticResource SegoeFluentIcons}\" "
            });

            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.Red,
                Text = "\r\n    FontSize"
            });
            paragraph.Inlines.Add("=");
            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.DarkMagenta,
                Text = "\"32\" "
            });

            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.Red,
                Text = "\r\n    Text"
            });
            paragraph.Inlines.Add("=");
            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.DarkMagenta,
                Text = $"\"{{StaticResource {item.Name}}}\" "
            });

            paragraph.Inlines.Add(new Run()
            {
                Foreground = Brushes.Blue,
                Text = "/>"
            });
            paragraph.Inlines.Add(new LineBreak());

            doc.Blocks.Add(paragraph);
            return doc;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
