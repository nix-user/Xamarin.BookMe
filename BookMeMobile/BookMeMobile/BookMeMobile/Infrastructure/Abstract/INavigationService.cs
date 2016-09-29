using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.ViewModels;
using BookMeMobile.ViewModels.Abstract;
using Xamarin.Forms;

namespace BookMeMobile.Infrastructure.Abstract
{
    public interface INavigationService
    {
        void ShowViewModel(BaseViewModel baseViewModel);

        INavigation XamarinNavigation { get; set; }
    }
}