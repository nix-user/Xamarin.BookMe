using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Render;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public class ActivityIndicatorPage : ContentPage
    {
        private Loader loader;
        private IViewContainer<View> rootLayout;

        protected void SetUpActivityIndicator(Loader loader, IViewContainer<View> rootLayout)
        {
            this.loader = loader;
            this.rootLayout = rootLayout;
        }

        protected async Task PerformWithActivityIndicator(Func<Task> action)
        {
            this.SetIsEnabledToChildren(this.rootLayout, false);
            this.loader.Show();

            await action();

            this.SetIsEnabledToChildren(this.rootLayout, true);
            this.loader.Hide();
        }

        private void SetIsEnabledToChildren(IViewContainer<View> rootLayout, bool value)
        {
            foreach (var child in rootLayout.Children)
            {
                var layout = child as Layout<View>;
                if (layout != null)
                {
                    this.SetIsEnabledToChildren(layout, value);
                }
                else
                {
                    child.IsEnabled = value;
                }
            }
        }
    }
}