using Clockpass.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Clockpass
{
    public partial class MainPage : ContentPage
    {
        string username { get; set; }
        string password { get; set; }
        public MainPage()
        {
            InitializeComponent();
            CustomWebview.Navigating += CustomWebview_Navigating;
            CustomWebview.Navigated += CustomWebview_Navigated; ;

            var connection = Xamarin.Essentials.Connectivity.NetworkAccess;
            if (connection != Xamarin.Essentials.NetworkAccess.Internet)
            {
                ShowConnectionError();
            }
            else
            {
                ShowWebviewAsync();
            }
            Xamarin.Essentials.Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            CheckLocationPermission();
        }

        private async void CustomWebview_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if (e.Url.Contains("app.clockpass.info/app/login.php"))
            {
                var FirstUserName = await SecureStorage.GetAsync("remember_email");

                var FirstPassword = await SecureStorage.GetAsync("remember_password");
                await CustomWebview.EvaluateJavascript("document.getElementById(\"username\").value = \"" + FirstUserName + "\";" +
                    "document.getElementById(\"password\").value = \"" + FirstPassword + "\";");
            };
            if (e.Url.Contains("app.clockpass.info/app/login.php"))
            {
                var val = await CustomWebview.EvaluateJavascript("document.getElementById(\"username\").value;");

            };
        }

        private async void CustomWebview_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.Contains("app.clockpass.info/app/clock-in-2.php"))
            {
                CheckLocationPermission();
            };
            if (e.Url.Contains("app.clockpass.info/app/login.php"))
            {
                //var val = await CustomWebview.EvaluateJavascript("document.getElementById(\"username\").value;");

            };
            if (e.Url.Contains("app.clockpass.info/app/page-home.php"))
            {
                try
                {
                    var firstUserName = "";
                    var firstPassword = "";
                    var val = username = await CustomWebview.EvaluateJavascript("document.getElementById(\"username\").value + \" \" + document.getElementById(\"password\").value");
                    var splitted = val.Split(' ', (char)2);
                    if (DeviceInfo.Platform == DevicePlatform.Android)
                    {
                        firstUserName = splitted[0].Remove(0, 1);
                        firstPassword = splitted[1].Remove(splitted[1].Length - 1);
                    }
                    else
                    {
                        firstUserName = splitted[0];
                        firstPassword = splitted[1];
                    }

                    await SecureStorage.SetAsync("remember_email", firstUserName);
                    await SecureStorage.SetAsync("remember_password", firstPassword);
                }
                catch (Exception ex)
                {

                }

            };
        }
        private Func<string, Task<string>> _evaluateJavascript;
        public Func<string, Task<string>> EvaluateJavascript
        {
            get { return _evaluateJavascript; }
            set { _evaluateJavascript = value; }
        }

        private void Connectivity_ConnectivityChanged(object sender, Xamarin.Essentials.ConnectivityChangedEventArgs e)
        {
            var connection = Xamarin.Essentials.Connectivity.NetworkAccess;
            if (connection != Xamarin.Essentials.NetworkAccess.Internet)
            {
                ShowConnectionError();
            }
            else
            {
                ShowWebviewAsync();
            }
        }
        private void Button_Clicked(object sender, EventArgs e)
        {
            var connection = Xamarin.Essentials.Connectivity.NetworkAccess;
            if (connection != Xamarin.Essentials.NetworkAccess.Internet)
            {
                ShowConnectionError();
            }
            else
            {
                ShowWebviewAsync();
            }
        }
        private void ShowConnectionError()
        {
            ConnectionError.IsVisible = true;
            web.IsVisible = false;
        }
        private async Task ShowWebviewAsync()
        {
            CustomWebview.Reload();
            await Task.Delay(1000);
            ConnectionError.IsVisible = false;
            web.IsVisible = true;
        }
        private async void CheckLocationPermission()
        {
            var status1 = await Xamarin.Essentials.Permissions.CheckStatusAsync<Xamarin.Essentials.Permissions.LocationWhenInUse>();
            if (status1 != Xamarin.Essentials.PermissionStatus.Granted)
            {
                status1 = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.LocationWhenInUse>();
                if (status1 != Xamarin.Essentials.PermissionStatus.Granted)
                {
                    await DisplayAlert("Error", "Please grant location permission to fetch your current location!", "OK");
                    //status1 = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.LocationWhenInUse>();
                    //if (status1 != Xamarin.Essentials.PermissionStatus.Granted)
                    //{
                    //    await DisplayAlert("Error", "Please grant location permission from the settings to proceed!", "OK");
                    //    System.Diagnostics.Process.GetCurrentProcess().Kill();
                    //}
                }
            }
        }
    }
}
