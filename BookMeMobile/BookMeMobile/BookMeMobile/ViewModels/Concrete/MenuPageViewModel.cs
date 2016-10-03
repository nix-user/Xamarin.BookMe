using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Data;
using BookMeMobile.Data.Concrete;
using BookMeMobile.Enums;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Model;
using BookMeMobile.Pages;
using BookMeMobile.ViewModels.Abstract;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    public class MenuPageViewModel : BaseViewModel
    {
        private ZXingScannerPage scanPage;

        public ObservableCollection<MenuPageItem> MasterPageItems { get; }

        public ICommand SelectItemCommand { get; set; }

        private MenuPageItem selected;
        private INavigationService navigationService;

        public MenuPageViewModel(INavigationService navigationService) : base(navigationService)
        {
            this.SelectItemCommand = new Command(this.SelectItem);
            this.MasterPageItems = new ObservableCollection<MenuPageItem>();
            this.navigationService = navigationService;
            this.MasterPageItems.Add(new MenuPageItem
            {
                Title = "Профиль",
                IconSource = "profileMenu.png",
                TargetType = typeof(ProfilePage),
                ViewModel = this
            });
            this.MasterPageItems.Add(new MenuPageItem
            {
                Title = "QR Бронирование",
                IconSource = "profileMenu.png",
                TargetType = typeof(QrReservation),
                ViewModel = this
            });
            this.MasterPageItems.Add(new MenuPageItem
            {
                Title = "Поиск комнат",
                IconSource = "profileMenu.png",
                TargetType = typeof(SelectPage),
                ViewModel = this
            });
        }

        public async void SelectItem(object model)
        {
            var item = model as MenuPageItem;
            if (item != null)
            {
                if (item.TargetType == typeof(ProfilePage))
                {
                    this.GoToProfilePage();
                }

                if (item.TargetType == typeof(SelectPage))
                {
                    this.navigationService.ShowViewModel<SelectViewModel>();
                    //this.navigationService.ShowViewModel(new SelectViewModel(new ListRoomManager(), this.navigationService));
                }

                if (item.TargetType == typeof(QrReservation))
                {
                    this.scanPage = new ZXingScannerPage();
                    this.scanPage.OnScanResult += this.HandleScanResult;
                    this.selected = null;
                    await this.navigationService.XamarinNavigation.PushAsync(this.scanPage);
                }
            }
        }

        private void GoToProfilePage()
        {
            //var viewModel = new ProfileViewModel(
            //    new ProfileService(
            //        new ProfileRepository(
            //            new HttpService(
            //                new CustomDependencyService(),
            //                new HttpClientHandler()))),
            //    this.navigationService);
            //this.navigationService.ShowViewModel(viewModel);
            this.navigationService.ShowViewModel<ProfileViewModel>();
        }

        private async Task<ProfileModel> GetProfileData()
        {
            var profileService = new ProfileService(new ProfileRepository(
                new HttpService(
                    new CustomDependencyService(),
                    new HttpClientHandler())));
            var operationResult = await profileService.GetUserData();
            if (operationResult.Status == StatusCode.Ok)
            {
                return operationResult.Result;
            }
            else
            {
                this.ShowErrorMessage(operationResult.Status);
                return null;
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