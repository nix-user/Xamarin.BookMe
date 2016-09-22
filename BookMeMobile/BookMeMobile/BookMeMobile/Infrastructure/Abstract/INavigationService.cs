using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.ViewModels;

namespace BookMeMobile.Infrastructure.Abstract
{
    public interface INavigationService
    {
        void ShowViewModel(BaseViewModel baseViewModel);
    }
}