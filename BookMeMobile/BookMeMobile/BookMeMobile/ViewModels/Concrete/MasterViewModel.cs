using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.ViewModels.Abstract;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    public class MasterViewModel : BaseViewModel
    {
        public MasterViewModel(INavigationService navigationService, Page currentPage) : base(navigationService)
        {
            this.DetailPage = currentPage;
        }

        public Page DetailPage { get; set; }

        public bool IsPresented { get; set; }
    }
}