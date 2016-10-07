using System.Collections.Generic;
using BookMeMobile.ViewModels.Abstract;
using Xamarin.Forms;

namespace BookMeMobile.Infrastructure.Abstract
{
    public interface INavigationService
    {
        void RemoveFromNavigationStakcToIndexFromTheEnd(int index = 1);

        void ShowViewModel<TViewModel>(bool modal = false) where TViewModel : BaseViewModel;

        void ShowViewModel<TViewModel>(object parameterValuesObject, bool modal = false) where TViewModel : BaseViewModel;

        void ShowViewModel<TViewModel>(IDictionary<string, object> parameterValues, bool modal = false) where TViewModel : BaseViewModel;

        Page ShowViewModelAsMainPage<TViewModel>() where TViewModel : BaseViewModel;

        Page ShowViewModelAsMainPageWithMenu<TViewModel>() where TViewModel : BaseViewModel;

        INavigation XamarinNavigation { get; set; }
    }
}