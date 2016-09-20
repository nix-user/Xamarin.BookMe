using Xamarin.Forms;

namespace BookMeMobile.Render
{
    public class Loader : ActivityIndicator
    {
        public void Show()
        {
            this.IsEnabled = true;
            this.IsRunning = true;
        }

        public void Hide()
        {
            this.IsEnabled = false;
            this.IsRunning = false;
        }
    }
}