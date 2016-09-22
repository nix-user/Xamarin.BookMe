using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Pages;
using BookMeMobile.Pages.Login;
using BookMeMobile.ViewModels;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile.Infrastructure.Concrete
{
    public class NavigationService : INavigationService
    {
        private const string ViewModelHasNotBeenRegisteredMessage = "View model has not been registered. Make sure you called RegisterViewModel method";

        private Dictionary<Type, Type> viewModelPagesDictionary = new Dictionary<Type, Type>()
        {
            { typeof(LoginViewModel), typeof(LoginPage) }
        };

        private INavigation xamarinNavigation;

        public NavigationService(INavigation xamarinNavigation)
        {
            this.xamarinNavigation = xamarinNavigation;
        }

        public void RegisterViewModel<TViewModel, TView>() where TViewModel : BaseViewModel
                                                           where TView : BasePage
        {
            this.viewModelPagesDictionary.Add(typeof(TViewModel), typeof(TView));
        }

        public void ShowViewModel(BaseViewModel viewModel)
        {
            var viewModelsViewType = this.viewModelPagesDictionary[viewModel.GetType()];
            if (viewModelsViewType == null)
            {
                throw new NavigationServiceException(ViewModelHasNotBeenRegisteredMessage);
            }

            var view = (BasePage)Activator.CreateInstance(viewModelsViewType);
            view.ViewModel = viewModel;
            this.xamarinNavigation.PushAsync(view);
        }
    }
}