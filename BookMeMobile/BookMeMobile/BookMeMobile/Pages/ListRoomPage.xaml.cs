using System;
using System.Collections.Generic;
using System.Linq;
using BookMeMobile.Entity;
using BookMeMobile.Infrastructure.Concrete;
using BookMeMobile.Model;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class ListRoomPage : BasePage
    {
        public ListRoomPage()
        {
            this.InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var viewModel = (ListRoomViewModel)this.ViewModel;
            if (viewModel.FavoriteOrFloorRoom != null)
            {
                this.listRoom.ScrollTo(viewModel.FavoriteOrFloorRoom, ScrollToPosition.Center, true);
            }
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            this.SetUpViewModelSubscriptions(this.ViewModel);
            this.BindingContext = this.ViewModel;
        }
    }
}