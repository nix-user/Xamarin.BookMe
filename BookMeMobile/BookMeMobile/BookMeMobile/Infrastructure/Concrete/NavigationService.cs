using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Pages;
using BookMeMobile.Pages.Login;
using BookMeMobile.Resources;
using BookMeMobile.ViewModels;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile.Infrastructure.Concrete
{
    public class NavigationService : INavigationService
    {
        public INavigation XamarinNavigation { get; set; }

        public NavigationService(INavigation xamarinNavigation)
        {
            this.XamarinNavigation = xamarinNavigation;
        }

        public void ShowViewModel(BaseViewModel viewModel)
        {
            var viewModelsViewType = this.GetPageType(viewModel);
            var view = (BasePage)Activator.CreateInstance(viewModelsViewType);
            view.ViewModel = viewModel;
            this.XamarinNavigation.PushAsync(view);
        }

        private Type GetPageType(BaseViewModel viewModel)
        {
            var viewModelType = viewModel.GetType();
            var currentAssembly = viewModelType.GetTypeInfo().Assembly;
            var pageTypeName = this.GetPageName(viewModelType.Name);
            var pageType = currentAssembly.ExportedTypes.FirstOrDefault(type => type.FullName.Contains(pageTypeName));
            if (pageType == null || !pageType.GetTypeInfo().IsSubclassOf(typeof(BasePage)))
            {
                throw new NavigationServiceException($"Page for view model {viewModelType.FullName} not found. " +
                                                     $"Are you sure it is named {pageTypeName}? and inherited from BasePage?");
            }

            return pageType;
        }

        private string GetPageName(string viewModelName)
        {
            int endingStringIndex = viewModelName.IndexOf(NamingOptions.ViewModelEnding);
            return viewModelName.Substring(0, endingStringIndex) + NamingOptions.PageEnding;
        }
    }
}