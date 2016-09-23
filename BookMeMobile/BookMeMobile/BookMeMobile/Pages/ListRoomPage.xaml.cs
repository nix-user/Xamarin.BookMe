﻿using System;
using System.Collections.Generic;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class ListRoomPage : BasePage
    {
        public ListRoomPage(IEnumerable<Room> search, SelectModel reservation)
        {
            this.InitializeComponent();
            var viewModel = new ListRoomViewModel(search, reservation)
            {
                Navigation = this.Navigation
            };
            this.SetUpViewModelSubscriptions(viewModel);
            this.BindingContext = viewModel;
        }
    }
}