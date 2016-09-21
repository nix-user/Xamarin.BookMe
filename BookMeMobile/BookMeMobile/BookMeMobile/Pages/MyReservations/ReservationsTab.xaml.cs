using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace BookMeMobile.Pages.MyReservations
{
    public partial class ReservationsTab : ContentPage
    {
        public ReservationsTab()
        {
            this.InitializeComponent();
        }

        private async void Cell_OnTapped(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("11:00 - 16:00. Каждый 2 год, каждый рабочий день первой недели сентября. ", "Отмена", null, "Удалить");
        }
    }
}