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
using BookMeMobile.Enums;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Model;
using BookMeMobile.Pages;
using BookMeMobile.Resources;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;
using HttpClientHandler = BookMeMobile.Data.Concrete.HttpClientHandler;

namespace BookMeMobile.ViewModels.Concrete
{
    public class MasterViewModel : BaseViewModel
    {
        public MasterViewModel(INavigationService navigationService, Page currentPage) : base(navigationService)
        {
            this.DetailPage = currentPage;
        }

        public Page DetailPage { get; set; }

        public bool IsPresented { get; set; }
    }
}