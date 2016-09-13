﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Pages.MyBookPages;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace BookMeMobile.Pages
{
    public partial class MainPage : MasterDetailPage
    {
        private MenuPage masterPage;
        private User currentUser;
        private ZXingScannerPage scanPage;

        public MainPage(User currentUser)
        {
            this.currentUser = currentUser;
            this.MasterBehavior = MasterBehavior.SplitOnPortrait;
            this.masterPage = new MenuPage();
            this.Master = this.masterPage;
            this.Detail = new SelectPage(currentUser);
            this.Detail.Padding = new Thickness(0, 20, 0, 0);
            this.masterPage.ListView.ItemSelected += this.OnItemSelected;
        }

        public MainPage(User currentUser, Page page)
        {
            this.currentUser = currentUser;
            this.MasterBehavior = MasterBehavior.SplitOnPortrait;
            this.masterPage = new MenuPage();
            this.Master = this.masterPage;
            this.Detail = page;
            this.Detail.Padding = new Thickness(0, 20, 0, 0);

            this.masterPage.ListView.ItemSelected += this.OnItemSelected;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MenuPageItem;
            if (item != null)
            {
                if (item.TargetType == typeof(ProfilePage))
                {
                    this.Detail = (Page)Activator.CreateInstance(item.TargetType);
                }

                if (item.TargetType == typeof(QrReservation))
                {
                    this.scanPage = new ZXingScannerPage();
                    this.scanPage.OnScanResult += this.HandleScanResult;
                    await this.Navigation.PushAsync(this.scanPage);
                }

                this.masterPage.ListView.SelectedItem = null;
                this.IsPresented = false;
            }
        }

        private void HandleScanResult(Result result)
        {
            this.scanPage.IsScanning = false;

            Device.BeginInvokeOnMainThread(() =>
            {
                Navigation.PopAsync();
                QrReservation code = new QrReservation();
                code.ScanResult(result, currentUser);
            });
        }
    }
}