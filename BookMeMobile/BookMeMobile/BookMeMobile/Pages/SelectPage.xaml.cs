using BookMeMobile.ViewModels.Concrete;

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
        }
    }
}