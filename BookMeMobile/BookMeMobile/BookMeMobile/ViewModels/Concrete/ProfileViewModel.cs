using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Enums;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Model;
using BookMeMobile.Resources;
using BookMeMobile.ViewModels.Abstract;
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
            this.ProfileModel = new ProfileModel();
            this.profileService = profileService;
            this.ChangeSaveCommand = new Command(this.SaveChanges);
        }

        public async Task GetDataProfile()
        {
            await Task.Delay(1000);
            var operationResult = await ExecuteOperation(async () => await this.profileService.GetUserData());
            if (operationResult.Status == StatusCode.Ok)
            {
                this.oldModel = new ProfileModel(operationResult.Result);
                this.FavoriteRoom = operationResult.Result.FavouriteRoom;
                this.MyFloor = operationResult.Result.Floor;
                this.oldModel = new ProfileModel(ProfileModel);
            }
            else
            {
                this.ShowErrorMessage(operationResult.Status);
            }
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
                this.OnPropertyChanged();
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
                this.OnPropertyChanged();
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