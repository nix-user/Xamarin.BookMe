using System.Threading.Tasks;
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
            this.SetUpActivityIndicator(this.loader, this.rootLayout);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await((ProfileViewModel)this.ViewModel).GetDataProfile();
            this.BindingContext = this.ViewModel;
        }
    }
}