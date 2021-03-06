﻿using System;
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

        public void RemoveFromNavigationStakcToIndexFromTheEnd(int index = 1)
        {
            var navigationStack = this.XamarinNavigation.NavigationStack;
            this.XamarinNavigation.RemovePage(navigationStack[navigationStack.Count - index]);
        }

        public void ShowViewModel<TViewModel>(bool modal = false)
            where TViewModel : BaseViewModel
        {
            this.PushViewModel<TViewModel>(new Dictionary<string, object>(), modal);
        }

        public void ShowViewModel<TViewModel>(object parameterValuesObject, bool modal = false)
            where TViewModel : BaseViewModel
        {
            this.PushViewModel<TViewModel>(this.ObjectToDictionary(parameterValuesObject), modal);
        }

        public void ShowViewModel<TViewModel>(IDictionary<string, object> parameterValues, bool modal = false)
            where TViewModel : BaseViewModel
        {
            this.PushViewModel<TViewModel>(parameterValues, modal);
        }

        public Page ShowViewModelAsMainPage<TViewModel>()
            where TViewModel : BaseViewModel
        {
            var view = this.SetupPage<TViewModel>();
            return new NavigationPage(view);
        }

        public Page ShowViewModelAsMainPageWithMenu<TViewModel>()
            where TViewModel : BaseViewModel
        {
            var view = this.SetupPage<TViewModel>();
            return new NavigationPage(new MasterPage(view, this));
        }

        public Page ShowViewModelAsMainPageWithMenu<TViewModel>(object parameterValuesObject)
            where TViewModel : BaseViewModel
        {
            var view = this.SetupPage<TViewModel>(this.ObjectToDictionary(parameterValuesObject));
            return new NavigationPage(new MasterPage(view, this));
        }

        private void PushViewModel<TViewModel>(IDictionary<string, object> parameterValues, bool modal)
        {
            parameterValues.Add("navigationService", this);

            ParameterOverride[] parameters = parameterValues.Select(p => new ParameterOverride(p.Key, p.Value)).ToArray();

            var view = this.ResolvePage(typeof(TViewModel));
            var viewModel = (BaseViewModel)App.Container.Resolve(typeof(TViewModel), parameters);
            view.ViewModel = viewModel;
            if (modal)
            {
                this.XamarinNavigation.PushModalAsync(view);
            }
            else
            {
                this.XamarinNavigation.PushAsync(new MasterPage(view, this));
            }
        }

        private BasePage SetupPage<TViewModel>(IDictionary<string, object> parameterValues = null)
        {
            parameterValues = parameterValues ?? new Dictionary<string, object>();
            parameterValues.Add("navigationService", this);
            ParameterOverride[] parameters = parameterValues.Select(p => new ParameterOverride(p.Key, p.Value)).ToArray();

            var view = this.ResolvePage(typeof(TViewModel));
            var viewModel = (BaseViewModel)App.Container.Resolve(typeof(TViewModel), parameters);
            view.ViewModel = viewModel;
            this.XamarinNavigation = view.Navigation;
            return view;
        }

        private BasePage ResolvePage(Type viewModelType)
        {
            var viewModelsPageType = this.GetPageType(viewModelType);
            var page = (BasePage)Activator.CreateInstance(viewModelsPageType);
            return page;
        }

        private Type GetPageType(Type viewModelType)
        {
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