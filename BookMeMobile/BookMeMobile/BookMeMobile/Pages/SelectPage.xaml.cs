using System;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class SelectPage : BasePage
    {
        public SelectPage()
        {
            this.InitializeComponent();
            this.SetUpActivityIndicator(this.loader, this.rootLayout);
            this.SetUpToPlatform();
            this.Date.MinimumDate = DateTime.Now;
        }

        public void SetUpToPlatform()
        {
            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                MainLayout.Padding = new Thickness(0, -40, 0, 0);
            }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            this.SetUpViewModelSubscriptions(this.ViewModel);
            this.BindingContext = this.ViewModel;
        }
    }
}