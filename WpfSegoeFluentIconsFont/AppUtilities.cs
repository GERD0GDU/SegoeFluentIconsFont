using System;
using System.ComponentModel;
using System.Diagnostics;

namespace WpfSegoeFluentIconsFont
{
    public static class AppUtilities
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never), Browsable(false)]
        private static string _ApplicationTitle = null;
        public static string ApplicationTitle
        {
            get
            {
                if (_ApplicationTitle is null)
                {
                    _ApplicationTitle =
                        $"{AssemblyInfo.Title} {AssemblyInfo.Version} v.";
                }

                return _ApplicationTitle;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never), Browsable(false)]
        private static string _Platform = null;
        public static string Platform
        {
            get
            {
                if (_Platform is null)
                {
                    _Platform = (IntPtr.Size != 8) ? "x86" : "x64";
                }
                return _Platform;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never), Browsable(false)]
        private static string _ReleaseDate = null;
        public static string ReleaseDate
        {
            get
            {
                if (_ReleaseDate is null)
                {
                    _ReleaseDate = new System.IO.FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location)
                        .LastWriteTime.ToString("dd MMM yyyy dddd HH':'mm':'ss");
                }
                return _ReleaseDate;
            }
        }

        [DebuggerBrowsable(DebuggerBrowsableState.Never), Browsable(false)]
        private static Uri _MailTo = null;
        public static Uri Mailto
        {
            get
            {
                if (_MailTo is null)
                {
                    string subject = Uri.EscapeDataString($"{AssemblyInfo.Title} {AssemblyInfo.Version}.v ({AssemblyInfo.Configuration}_{Platform})");
                    string body = "";
                    body += $"Title: {AssemblyInfo.Title}\r\n";
                    body += $"Version: {AssemblyInfo.Version}\r\n";
                    body += $"Product: {AssemblyInfo.Product}\r\n";
                    body += $"Release Date: {ReleaseDate}\r\n";
                    body += $"Configuration: {AssemblyInfo.Configuration}\r\n";
                    body += $"Platform: {Platform}\r\n\r\n";
                    _MailTo = new Uri($"mailto:{AssemblyInfo.Email}?subject={subject}&body={Uri.EscapeDataString(body)}");
                }
                return _MailTo;
            }
        }
    }
}
