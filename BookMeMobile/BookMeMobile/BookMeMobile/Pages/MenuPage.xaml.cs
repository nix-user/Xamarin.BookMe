using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class MenuPage : ContentPage
    {
        public ListView ListView
        {
            get { return this.listView; }
        }

        public MenuPage()
        {
            this.InitializeComponent();
            var masterPageItems = new List<MenuPageItem>();
            //TODO: uncomment after fix
            //masterPageItems.Add(new MenuPageItem
            //{
            //    Title = "Профиль",
            //    IconSource = "profileMenu.png",
            //    TargetType = typeof(ProfilePage)
            //});
            //masterPageItems.Add(new MenuPageItem
            //{
            //    Title = "QR Бронирование",
            //    IconSource = "profileMenu.png",
            //    TargetType = typeof(QrReservation)
            //});
            listView.ItemsSource = masterPageItems;
        }
    }
}