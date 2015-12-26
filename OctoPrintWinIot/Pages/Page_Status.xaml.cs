using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web.Provider;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Windows.System.Threading;
using Windows.UI.ViewManagement;

// Die Elementvorlage "Leere Seite" ist unter http://go.microsoft.com/fwlink/?LinkId=234238 dokumentiert.

namespace OctoPrintWinIot.Pages
{ 

    /// <summary>
    /// Eine leere Seite, die eigenständig verwendet oder zu der innerhalb eines Rahmens navigiert werden kann.
    /// </summary>
    public sealed partial class Page_Status : Page
    {
        public String apiUrl = "msi-octopi.no-ip.org";
        public String apiKey = "5E6F88D2A38643478B0183C2AFEFFBEA";

        private DispatcherTimer timer;

        public Page_Status()
        {
            this.InitializeComponent();
            //getPrintOperation();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            timer.Start();

        }

        private void Timer_Tick(object sender, object e)
        {
            getPrintOperation();
            getjobOperation();
        }

        private async void getPrintOperation()
        {
            HttpClient client = new HttpClient();
            string url = "http://"+ apiUrl +"/api/printer?apikey="+ apiKey;
            var responses = await client.GetAsync(url);

            responses.EnsureSuccessStatusCode();

            string content = await responses.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);

            txtPrintTemp.Text = json["temperature"]["tool0"]["actual"].ToString() + "°C / " + json["temperature"]["tool0"]["target"].ToString() + "°C";
            txtState.Text = json["state"]["text"].ToString();

        }

        private async void getjobOperation()
        {
            HttpClient client = new HttpClient();
            string url = "http://" + apiUrl + "/api/job?apikey=" + apiKey;
            var responses = await client.GetAsync(url);

            responses.EnsureSuccessStatusCode();

            string content = await responses.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);

            if (json["progress"]["completion"].ToString() == "")
            {
                pbProgress.Value = 0;
            }
            else
            {
                string ProcessStr = json["progress"]["completion"].ToString();
                double ProcessDouble = double.Parse(ProcessStr);
                int ProgressBar = Convert.ToInt32(ProcessDouble);
                pbProgress.Value = ProgressBar;
            }
        }
    }
}
