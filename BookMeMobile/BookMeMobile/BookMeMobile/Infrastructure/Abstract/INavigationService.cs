using System.Collections.Generic;
using BookMeMobile.ViewModels.Abstract;
using Xamarin.Forms;

namespace BookMeMobile.Infrastructure.Abstract
{
    public interface INavigationService
    {
        void ShowViewModel<TViewModel>() where TViewModel : BaseViewModel;

        void ShowViewModel<TViewModel>(object parameterValuesObject) where TViewModel : BaseViewModel;

        void ShowViewModelAsMainPage<TViewModel>(out Page mainPage) where TViewModel : BaseViewModel;

        void ShowViewModel<TViewModel>(IDictionary<string, object> parameterValues) where TViewModel : BaseViewModel;

        INavigation XamarinNavigation { get; set; }
    }
}