using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using OctoPrintWinIot.Pages;
using System.Threading.Tasks;
using Windows.UI.Core;


// Die Vorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 dokumentiert.

namespace OctoPrintWinIot
{
    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            // Start Task that updates time
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Low,
                    () =>
                    {
                        Lbl_Time.Text = DateTime.Now.ToString("HH:mm tt");
                        Lbl_TimeBtm.Text = DateTime.Now.ToString("HH:mm tt");
                        Lbl_Date.Text = DateTime.Now.ToString("MMMM dd, yyyy");
                    });
                    await Task.Delay(1000);
                }
            });

            if (Lbl_Time.Visibility == Visibility.Visible)
            {
                Lbl_TimeBtm.Visibility = Visibility.Collapsed;
            }
        }

        private void Frame_Main_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void Frame_Main_Navigating(object sender, NavigatingCancelEventArgs e)
        {
          
        }

        private void Btn_Home_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame_Main.Visibility = Visibility.Collapsed;
            Lbl_TimeBtm.Visibility = Visibility.Collapsed;
            Lbl_Time.Visibility = Visibility.Visible;
            Lbl_Date.Visibility = Visibility.Visible;
            
        }

        private void Btn_ShowStatus_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame_Main.Visibility = Visibility.Visible;
            Lbl_TimeBtm.Visibility = Visibility.Visible;
            Lbl_Time.Visibility = Visibility.Collapsed;
            Lbl_Date.Visibility = Visibility.Collapsed;

            Pages.Page_Status _SP = new Pages.Page_Status();
            Frame_Main.Navigate(_SP.GetType());
        }

        private void Btn_ShowVideo_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame_Main.Visibility = Visibility.Visible;
            Lbl_TimeBtm.Visibility = Visibility.Visible;
            Lbl_Time.Visibility = Visibility.Collapsed;
            Lbl_Date.Visibility = Visibility.Collapsed;

            Pages.Page_Camera _CP = new Pages.Page_Camera();
            Frame_Main.Navigate(_CP.GetType());
        }

        private void Btn_ShowControl_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame_Main.Visibility = Visibility.Visible;
            Lbl_TimeBtm.Visibility = Visibility.Visible;
            Lbl_Time.Visibility = Visibility.Collapsed;
            Lbl_Date.Visibility = Visibility.Collapsed;

            Pages.Page_Control _CrtlP = new Pages.Page_Control();
            Frame_Main.Navigate(_CrtlP.GetType());
        }
    }
}
