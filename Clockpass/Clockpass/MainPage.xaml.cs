using Clockpass.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Clockpass
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            CustomWebview.Navigating += CustomWebview_Navigating;
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
        private void CustomWebview_Navigating(object sender, WebNavigatingEventArgs e)
        {
            if (e.Url.Contains("app.clockpass.info/app/clock-in-2.php"))
            {
                CheckLocationPermission();
            };
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
