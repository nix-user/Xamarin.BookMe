using System;
using BookMeMobile.ViewModels.Concrete;

namespace BookMeMobile.Model
{
    public class MenuPageItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }

        public MenuPageViewModel ViewModel { get; set; }
    }
}