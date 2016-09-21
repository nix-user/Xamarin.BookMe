using System;
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
    public partial class ReservationPage : ActivityIndicatorPage
    {
        public ReservationPage(RoomFilterParameters filterparametr, int idRoom)
        {
            this.InitializeComponent();
            this.BindingContext = new AddReservationViewModel(filterparametr, idRoom);
        }
    }
}