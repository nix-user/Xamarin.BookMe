using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Graphics.Pdf;
using BookMeMobile.BL;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Data;
using BookMeMobile.Data.Concrete;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Model;
using BookMeMobile.Pages;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;
using HttpClientHandler = BookMeMobile.Data.Concrete.HttpClientHandler;

namespace BookMeMobile.ViewModels.Concrete
{
    public class MasterViewModel : BaseViewModel
    {
        private ZXingScannerPage scanPage;

        public MasterViewModel(INavigationService navigationService, Page currentPage) : base(navigationService)
        {
            this.DetailPage = currentPage;
        }

        public MenuPage MasterPage
        {
            get
            {
                var menu = new MenuPage();
                menu.ListView.ItemSelected += this.OnItemSelected;
                return menu;
            }
        }

        public Page DetailPage { get; set; }

        public bool IsPresented { get; set; }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuPageItem;
            if (item != null)
            {
                if (item.TargetType == typeof(ProfilePage))
                {
                    this.NavigationService.ShowViewModel(new ProfileViewModel(
                        new ProfileService(new ProfileRepository(
                            new HttpService(
                                new CustomDependencyService(),
                                new HttpClientHandler()))),
                        this.NavigationService));
                }

                if (item.TargetType == typeof(SelectPage))
                {
                    this.NavigationService.ShowViewModel(new SelectViewModel(new ListRoomManager(), this.NavigationService));
                }

                if (item.TargetType == typeof(QrReservation))
                {
                    this.scanPage = new ZXingScannerPage();
                    this.scanPage.OnScanResult += this.HandleScanResult;
                    await this.NavigationService.XamarinNavigation.PushAsync(this.scanPage);
                }

                this.MasterPage.ListView.SelectedItem = null;
                this.IsPresented = false;
            }
        }

        private void HandleScanResult(Result result)
        {
            this.scanPage.IsScanning = false;

            Device.BeginInvokeOnMainThread(() =>
            {
                this.NavigationService.XamarinNavigation.PopAsync();
                QrReservation code = new QrReservation();
                code.ScanResult(result);
            });
        }
    }
}