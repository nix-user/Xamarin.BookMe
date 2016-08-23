using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Pages.MyBookPages;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class MainPage : MasterDetailPage
    {
        private MenuPage masterPage;
        private User currentUser;

        public MainPage(User currentUser)
        {
            this.currentUser = currentUser;
            this.MasterBehavior = MasterBehavior.SplitOnPortrait;
            this.masterPage = new MenuPage();
            this.Master = this.masterPage;
            this.Detail = new NavigationPage(new SelectPage(currentUser));
            this.Detail.Padding = new Thickness(0, 20, 0, 0);
            this.masterPage.ListView.ItemSelected += this.OnItemSelected;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuPageItem;
            if (item != null)
            {
                if (item.TargetType == typeof(MyBooks))
                {
                    this.Detail = new NavigationPage((Page)Activator.CreateInstance(item.TargetType, this.currentUser));
                    this.masterPage.ListView.SelectedItem = null;
                    this.IsPresented = false;
                }

                if (item.TargetType == typeof(ProfilePage))
                {
                    this.Detail.Navigation.PushAsync(new ProfilePage());
                    this.masterPage.ListView.SelectedItem = null;
                    this.IsPresented = false;
                }
            }
        }
    }
}