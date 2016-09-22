﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Model;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class ReservationPage : BasePage
    {
        public ReservationPage(RoomFilterParameters filterparametr, int idRoom)
        {
            this.InitializeComponent();
            var viewModel = new AddReservationViewModel(filterparametr, idRoom) { Navigation = this.Navigation };
            this.SetUpViewModelSubscriptions(viewModel);
            this.BindingContext = viewModel;
            this.SetUpActivityIndicator(this.loader, this.rootLayout);
        }
    }
}