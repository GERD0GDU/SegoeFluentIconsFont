using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WpfSegoeFluentIconsFont
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static readonly int WM_USER_MESSAGE = User32Native.RegisterWindowMessage("{ADE11A76-3E3C-40F1-910E-559ECDB4E918}");
        private PreventMultipleInstances m_preventMultipleInstances = null;

        protected override void OnStartup(StartupEventArgs e)
        {
            ArgumentWrapper.Default = new ArgumentWrapper(e.Args);

            m_preventMultipleInstances = new PreventMultipleInstances("{DE84E0C1-435D-4137-AE72-5366224BC388}");
            if (m_preventMultipleInstances)
            {
                System.Diagnostics.Debug.Assert(WM_USER_MESSAGE >= 0xC000 && WM_USER_MESSAGE <= 0xFFFF);

                IntPtr hWnd = User32Native.FindWindow(null, AppUtilities.ApplicationTitle);
                if (hWnd != IntPtr.Zero)
                {
                    if (ArgumentWrapper.Default.IsExit)
                    {
                        User32Native.SendMessage(hWnd, WM_USER_MESSAGE, User32Native.SW_EXIT, IntPtr.Zero);
                    }
                    else
                    {
                        User32Native.SendMessage(hWnd, WM_USER_MESSAGE, User32Native.SW_SHOW, IntPtr.Zero);
                    }

                    Shutdown(1);
                    return;
                }
            }
            else if (ArgumentWrapper.Default.IsExit)
            {
                Shutdown(1);
                return;
            }

            // ana pencereyi başlangıç penceresi olarak set et
            StartupUri = new Uri("MainWindow.xaml", UriKind.Relative);
        }
    }
}
