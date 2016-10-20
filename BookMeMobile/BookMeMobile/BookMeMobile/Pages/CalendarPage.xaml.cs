using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.ViewModels;
using Xamarin.Forms;
using XamForms.Controls;

namespace BookMeMobile.Pages
{
    public partial class CalendarPage : BasePage
    {
        public CalendarPage()
        {
            this.InitializeComponent();
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            this.SetUpViewModelSubscriptions(this.ViewModel);
            this.BindingContext = this.ViewModel;
        }
    }
}