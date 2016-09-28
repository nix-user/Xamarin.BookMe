using BookMeMobile.BL.Concrete;
using BookMeMobile.Data;
using BookMeMobile.Data.Concrete;
using BookMeMobile.Infrastructure.Concrete;
using BookMeMobile.ViewModels.Concrete;

namespace BookMeMobile.Pages
{
    public partial class ProfilePage : BasePage
    {
        public ProfilePage()
        {
            this.InitializeComponent();
            this.ViewModel =
                new ProfileViewModel(
                    new ProfileService(
                        new ProfileRepository(
                            new HttpService(
                                new CustomDependencyService(),
                                new HttpClientHandler()))),
                    new NavigationService(this.Navigation));
            this.SetUpActivityIndicator(this.loader, this.rootLayout);
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            this.SetUpViewModelSubscriptions(this.ViewModel);
            this.BindingContext = this.ViewModel;
        }
    }
}