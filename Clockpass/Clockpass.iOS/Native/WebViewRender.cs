using Clockpass.Helper;
using Clockpass.iOS.Native;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(WebViewer), typeof(WebViewRender))]
namespace Clockpass.iOS.Native
{
    public class WebViewRender : WkWebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            var webView = e.NewElement as WebViewer;
            if (webView != null)
                webView.EvaluateJavascript = async (js) =>
                {
                    var x = await webView.EvaluateJavaScriptAsync(js);
                    return x;
                };
           
        }
    }
}