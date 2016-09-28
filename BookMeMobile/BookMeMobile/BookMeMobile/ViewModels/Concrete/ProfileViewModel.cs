using System;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL;
using BookMeMobile.BL.Abstract;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Data;
using BookMeMobile.Data.Concrete;
using BookMeMobile.Enums;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Model;
using BookMeMobile.Resources;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    public class ProfileViewModel : BaseViewModel
    {
        public ProfileModel ProfileModel { get; set; }

        private IProfileService profileService;
        private ProfileModel oldModel;

        public ProfileViewModel(IProfileService profileService, INavigationService navigationService) : base(navigationService)
        {
            this.profileService = new ProfileService(new ProfileRepository(new HttpService(new CustomDependencyService(), new HttpClientHandler())));
            ProfileModel = this.profileService.GetProfileFromFile().Result ?? new ProfileModel();
            this.oldModel = new ProfileModel(ProfileModel);
            this.ChangeSaveCommand = new Command(this.SaveChanges);
        }

        public string FavoriteRoom
        {
            get
            {
                return this.ProfileModel?.FavouriteRoom;
            }

            set
            {
                this.ProfileModel.FavouriteRoom = value;
                this.OnPropertyChanged("IsEnableButtonSave");
            }
        }

        public int MyFloor
        {
            get
            {
                return this.ProfileModel.Floor;
            }

            set
            {
                this.ProfileModel.Floor = value;
                this.OnPropertyChanged("IsEnableButtonSave");
            }
        }

        public bool IsEnableButtonSave => !this.oldModel.Equals(ProfileModel);

        public ICommand ChangeSaveCommand { get; set; }

        private async void SaveChanges(object binding)
        {
            var operationResult =
                   (await this.ExecuteOperation(async () => await this.profileService.SaveProfileModel(this.ProfileModel)));
            if (operationResult.Status == StatusCode.Ok)
            {
                this.oldModel = new ProfileModel(ProfileModel);
                this.MyFloor = this.oldModel.Floor;
                this.ShowInformationDialog(AlertMessages.SuccessHeader, AlertMessages.SuccessBody);
            }
            else
            {
                this.ShowErrorMessage(operationResult.Status);
            }
        }
    }
}