//
// https://learn.microsoft.com/en-us/windows/apps/design/style/segoe-fluent-icons-font
//
using System;
using System.Linq;
using System.Collections;
using System.Windows;

namespace WpfSegoeFluentIconsFont
{
    public static class IconsFactory
    {
        private static ListItemModel[] LoadIcons()
        {
            ResourceDictionary resourceDictionary = new ResourceDictionary()
            {
                Source = new Uri($"pack://application:,,,/{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name};component/Fonts/SegoeFluentIcons.xaml", UriKind.RelativeOrAbsolute)
            };

            return resourceDictionary
                .OfType<DictionaryEntry>()
                .Where(x => x.Value is string sValue && sValue.Length == 1 && sValue[0] >= 0xE700 && sValue[0] <= 0xF800)
                .OrderBy(x => x.Value)
                .Select(x => new ListItemModel() { Name = x.Key as string, Value = x.Value as string })
                .ToArray();
        }

        public static readonly ListItemModel[] Icons = LoadIcons();
    }
}
