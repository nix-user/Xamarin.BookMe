﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using Xamarin.Forms;

namespace BookMeMobile.Pages.MyReservations
{
    public partial class MyReservationsPage : TabbedPage
    {
        public MyReservationsPage()
        {
            this.InitializeComponent();
            this.Children[0].BindingContext = new List<Reservation>()
            {
                new Reservation()
                {
                    Room = new Room() { Number = "405a", Id = 1, IsBig = true, IsHasPolykom = true },
                    Id = 1,
                    Author = "me",
                    Duration = new TimeSpan(1, 1, 1),
                    From = new DateTime(2016, 1, 1, 1, 1, 1),
                    To = new DateTime(2016, 1, 1, 1, 1, 1),
                    IsRecursive = true,
                    ResourceId = 5,
                    TextPeriod = "Целый день.",
                    TextRool = "Каждые 2 недели каждый ПН. ВТ. йцуйцуйцу"
                },
                new Reservation()
                {
                    Room = new Room() { Number = "405a", Id = 1, IsBig = true, IsHasPolykom = true },
                    Id = 1,
                    Author = "me",
                    Duration = new TimeSpan(1, 1, 1),
                    From = new DateTime(2016, 1, 1, 1, 1, 1),
                    To = new DateTime(2016, 1, 1, 1, 1, 1),
                    IsRecursive = false,
                    ResourceId = 5,
                    TextPeriod = "Целый день."
                }
            };
        }
    }
}