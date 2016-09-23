using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class SelectPage : BasePage
    {
        public SelectPage()
        {
            this.InitializeComponent();
            var viewModel = new SelectViewModel() { Navigation = this.Navigation };
            this.SetUpViewModelSubscriptions(viewModel);
            this.BindingContext = viewModel;
            this.SetUpActivityIndicator(this.loader, this.rootLayout);
            this.SetUpToPlatform();
        }

        public void SetUpToPlatform()
        {
            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                MainLayout.Padding = new Thickness(0, -40, 0, 0);
            }
        }
    }
}