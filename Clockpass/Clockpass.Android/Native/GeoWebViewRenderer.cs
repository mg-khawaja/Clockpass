using Android.Webkit;
using Clockpass.Helper;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using Clockpass.Droid.Native;

[assembly: ExportRenderer(typeof(GeoWebView), typeof(GeoWebViewRenderer))]
namespace Clockpass.Droid.Native
{
    [System.Obsolete]
    public class GeoWebViewRenderer : WebViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            Control.Settings.JavaScriptEnabled = true;
            Control.SetWebChromeClient(new MyWebClient());
        }
    }

    public class MyWebClient : WebChromeClient
    {
        public override void OnGeolocationPermissionsShowPrompt(string origin, GeolocationPermissions.ICallback callback)
        {
            callback.Invoke(origin, true, false);
        }
    }
}