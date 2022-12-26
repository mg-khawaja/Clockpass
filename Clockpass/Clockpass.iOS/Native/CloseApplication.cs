using Clockpass.Helper;
using System.Threading;

namespace Clockpass.iOS.Native
{
    public class CloseApplication : ICloseApplication
    {
        public void closeApplication()
        {
            Thread.CurrentThread.Abort();
        }
    }
}