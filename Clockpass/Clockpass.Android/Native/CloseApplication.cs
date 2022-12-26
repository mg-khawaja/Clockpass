using Android.App;
using Clockpass.Helper;
using Xamarin.Forms;

namespace Clockpass.Droid.Native
{
    public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            var activity = (Activity)Forms.Context;
            activity.FinishAffinity();
        }
    }
}