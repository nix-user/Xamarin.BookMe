using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL.Abstract;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Data;
using BookMeMobile.Data.Concrete;
using BookMeMobile.Entity;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Model;
using BookMeMobile.Pages;
using BookMeMobile.ViewModels.Abstract;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    public class ListRoomViewModel : BaseViewModel
    {
        private SelectModel selectModel;

        public ProfileModel ProfileModel { get; set; }

        public string FavoriteRoom => ProfileModel.FavouriteRoom;

        public bool HasFavoriteRoom => this.ListRoom.Any(x => x.NumberRoom == ProfileModel.FavouriteRoom);

        public RoomViewModel FavoriteOrFloorRoom => this.ListRoom.FirstOrDefault(x => this.GetFloorInNumber(x.NumberRoom) == ProfileModel.Floor);

        public List<RoomViewModel> ListRoom { get; }

        public ICommand ReserveCommand { get; set; }

        public ICommand ReserveFavoriteRoomCommand { get; set; }

        public bool IsNotRooms => !this.ListRoom.Any();

        public ListRoomViewModel(IEnumerable<Room> rooms, SelectModel selectModel, ProfileModel profileModel, INavigationService navigationService) : base(navigationService)
        {
            this.ListRoom = rooms.Select(x => new RoomViewModel(x, this)).ToList();
            this.selectModel = selectModel;
            this.ProfileModel = profileModel;

            this.ReserveCommand = new Command(this.Reserve);
            this.ReserveFavoriteRoomCommand = new Command(this.ReserveFavoriteCommand);
        }

        private void ReserveFavoriteCommand(object obj)
        {
            RoomViewModel selectedRoom = this.ListRoom.FirstOrDefault(x => x.NumberRoom == ProfileModel.FavouriteRoom);
            this.NavigationService.ShowViewModel<AddReservationViewModel>(new { filterParameter = this.selectModel, roomModel = selectedRoom });
        }

        public void Reserve(object selectElement)
        {
            RoomViewModel selectedRoom = selectElement as RoomViewModel;
            this.NavigationService.ShowViewModel<AddReservationViewModel>(new { filterParameter = this.selectModel, roomModel = selectedRoom });
        }

        private int GetFloorInNumber(string s)
        {
            string stringFloor = new string(s.Replace(" ", string.Empty).ToCharArray().Where(char.IsDigit).ToArray());

            if (stringFloor.Length < 3)
            {
                return int.Parse(stringFloor.ToString());
            }
            else
            {
                return (stringFloor.Length < 4) ? int.Parse(stringFloor[0].ToString()) : int.Parse(stringFloor[0].ToString() + stringFloor[1]);
            }
        }
    }
}