using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Pages;
using BookMeMobile.Resources;
using BookMeMobile.ViewModels.Abstract;
using Microsoft.Practices.Unity;
using Xamarin.Forms;

namespace BookMeMobile.Infrastructure.Concrete
{
    public class NavigationService : INavigationService
    {
        public INavigation XamarinNavigation { get; set; }

        public NavigationService()
        {
        }

        public NavigationService(INavigation xamarinNavigation)
        {
            this.XamarinNavigation = xamarinNavigation;
        }

        public void ShowViewModel<TViewModel>()
            where TViewModel : BaseViewModel
        {
            this.ShowViewModel<TViewModel>(new Dictionary<string, object>());
        }

        public void ShowViewModel<TViewModel>(object parameterValuesObject)
            where TViewModel : BaseViewModel
        {
            this.ShowViewModel<TViewModel>(this.ObjectToDictionary(parameterValuesObject));
        }

        public void ShowViewModel<TViewModel>(IDictionary<string, object> parameterValues)
            where TViewModel : BaseViewModel
        {
            parameterValues.Add("navigationService", this);

            ParameterOverride[] parameters = parameterValues.Select(p => new ParameterOverride(p.Key, p.Value)).ToArray();

            var viewModel = (BaseViewModel)App.Container.Resolve(typeof(TViewModel), parameters);
            var view = this.GetPage(viewModel);
            this.XamarinNavigation.PushAsync(view);
        }

        public void ShowViewModelAsMainPage<TViewModel>(out Page mainPage)
            where TViewModel : BaseViewModel
        {
            var viewModel = (BaseViewModel)App.Container.Resolve(typeof(TViewModel), new ParameterOverride("navigationService", this));
            var view = this.GetPage(viewModel);
            this.XamarinNavigation = view.Navigation;
            mainPage = new NavigationPage(view);
        }

        private BasePage GetPage(BaseViewModel viewModel)
        {
            var viewModelsViewType = this.GetPageType(viewModel);
            var view = (BasePage)Activator.CreateInstance(viewModelsViewType);
            view.ViewModel = viewModel;
            return view;
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

        private Dictionary<string, object> ObjectToDictionary(object source)
        {
            var dictionary = new Dictionary<string, object>();
            IEnumerable<PropertyInfo> properties = source.GetType().GetRuntimeProperties();

            foreach (var property in properties)
            {
                var val = property.GetValue(source);
                dictionary.Add(property.Name, val);
            }

            return dictionary;
        }
    }
}