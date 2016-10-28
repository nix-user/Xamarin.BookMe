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
        }

        public void SetUpToPlatform()
        {
            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                MainLayout.Padding = new Thickness(0, -40, 0, 0);
            }

            if (Device.OS == TargetPlatform.iOS)
            {
                this.rootLayout.Padding = new Thickness(0, 50);
                var distanceBetweenBlocks = new Thickness(0, 25);
                this.DateSpace.Padding = distanceBetweenBlocks;
                this.timeLayout.Padding = distanceBetweenBlocks;
                this.checkerLayout.Padding = distanceBetweenBlocks;
                this.ButtonSpace.Padding = distanceBetweenBlocks;
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