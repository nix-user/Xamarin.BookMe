﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Entity;
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
        private IRoomService roomService;
        private ProfileModel oldModel;
        private IEnumerable<Room> allRoomItems;

        public ProfileViewModel(IProfileService profileService, IRoomService roomService, INavigationService navigationService) : base(navigationService)
        {
            this.ProfileModel = new ProfileModel();
            this.profileService = profileService;
            this.roomService = roomService;
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
                this.OnPropertyChanged();
                this.OnPropertyChanged("IsEnableButtonSave");
            }
        }

        public string MyFloor
        {
            get
            {
                return this.ProfileModel.Floor == 0 ? string.Empty : this.ProfileModel.Floor.ToString();
            }

            set
            {
                int result;
                if (int.TryParse(value, out result))
                {
                    this.ProfileModel.Floor = result;
                }
                else
                {
                    this.ProfileModel.Floor = 0;
                }

                this.OnPropertyChanged();
                this.OnPropertyChanged("IsEnableButtonSave");
            }
        }

        public bool IsEnableButtonSave => this.oldModel != null && !this.oldModel.Equals(this.ProfileModel);

        public ICommand ChangeSaveCommand { get; set; }

        public override void OnAttachedToView()
        {
            base.OnAttachedToView();

            this.GetDataProfile();
        }

        private async Task GetDataProfile()
        {
            var operationResult = await ExecuteOperation(async () => await this.profileService.GetUserData());
            this.allRoomItems = (await ExecuteOperation(async () => await this.roomService.GetAllRoom())).Result;
            if (operationResult.Status == StatusCode.Ok)
            {
                if (operationResult.Result != null)
                {
                    this.oldModel = new ProfileModel(operationResult.Result);
                    this.FavoriteRoom = operationResult.Result.FavouriteRoom;
                    this.MyFloor = operationResult.Result.Floor.ToString();
                }
                else
                {
                    this.oldModel = new ProfileModel();
                }
            }
            else
            {
                await this.ShowErrorMessage(operationResult.Status);
            }

            this.OnPropertyChanged("AllRoom");
            this.OnPropertyChanged("SelectedRoom");
        }

        public List<string> AllRoom
        {
            get { return this.allRoomItems?.Select(x => x.Number).ToList(); }
        }

        public object SelectedRoom
        {
            get { return this.FavoriteRoom; }
            set { this.FavoriteRoom = value.ToString(); }
        }

        private async void SaveChanges(object binding)
        {
            var operationResult =
                   (await this.ExecuteOperation(async () => await this.profileService.SaveProfileModel(this.ProfileModel)));
            if (operationResult.Status == StatusCode.Ok)
            {
                this.oldModel = new ProfileModel(ProfileModel);
                this.MyFloor = this.oldModel.Floor.ToString();
                await this.ShowInformationDialog(AlertMessages.SuccessHeader, AlertMessages.SuccessBody);
            }
            else
            {
                await this.ShowErrorMessage(operationResult.Status);
            }
        }
    }
}