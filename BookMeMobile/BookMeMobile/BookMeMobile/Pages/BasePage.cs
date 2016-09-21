using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Render;
using BookMeMobile.ViewModels;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public class BasePage : ContentPage
    {
        private Loader loader;
        private IViewContainer<View> rootLayout;

        protected void SetUpViewModelSubscriptions(BaseViewModel baseViewModel)
        {
            baseViewModel.ToggleProgressIndicator = this.ToggleProgressIndicator;
            baseViewModel.ShowInfoMessage = this.ShowInfoMessage;
        }

        protected void SetUpActivityIndicator(Loader loader, IViewContainer<View> rootLayout)
        {
            this.loader = loader;
            this.rootLayout = rootLayout;
        }

        protected void ShowActivityIndicator()
        {
            this.SetIsEnabledToChildren(this.rootLayout, false);
            this.loader.Show();
        }

        protected void HideActivityIndicator()
        {
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

        private void ToggleProgressIndicator(bool isIndicatorShown)
        {
            if (isIndicatorShown)
            {
                this.ShowActivityIndicator();
            }
            else
            {
                this.HideActivityIndicator();
            }
        }

        private void ShowInfoMessage(string title, string content, string cancelText)
        {
            this.DisplayAlert(title, content, cancelText);
        }
    }
}