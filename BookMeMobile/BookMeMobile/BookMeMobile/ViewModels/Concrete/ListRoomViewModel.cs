using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.Entity;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Pages;
using BookMeMobile.ViewModels;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile.Model
{
    public class ListRoomViewModel : BaseViewModel
    {
        private static readonly IEnumerable<string> UnallowedResources = new List<string>()
        {
            "709",
            "710",
            "712a",
            "713",
            "Netatmo",
            "NetatmoCityHall",
            "Projector 1",
            "Projector 2"
        };

        private SelectModel selectModel;

        public List<RoomViewModel> ListRoom { get; }

        public ICommand ReserveCommand { get; set; }

        public string IsNotRooms
        {
            get { return this.ListRoom.Any() ? null : "Комнат нет"; }
        }

        public ListRoomViewModel(IEnumerable<Room> rooms, INavigationService navigationService, SelectModel selectModel) : base(navigationService)
        {
            this.ListRoom = rooms.Where(x => !UnallowedResources.Contains(x.Number)).Select(x => new RoomViewModel()
            {
                Id = x.Id,
                NumberRoom = x.Number,
                ListViewModel = this
            }).ToList();

            this.ReserveCommand = new Command(this.Reserve);
            this.selectModel = selectModel;
        }

        public void Reserve(object selectElement)
        {
            RoomViewModel selectedRoom = selectElement as RoomViewModel;
            this.NavigationService.ShowViewModel(new AddReservationViewModel(this.selectModel, this.NavigationService, selectedRoom));
        }

        /// <summary>
        /// Sort room collection by user preference
        /// </summary>
        /// <param name="list">Collection to sort</param>
        /// <returns></returns>
        private List<RoomViewModel> Sort(List<RoomViewModel> list)
        {
            User currentUser = new User();
            currentUser.MyRoom = "410";
            currentUser.FavoriteRoom = "505";
            int userFloor = this.GetFloorInNumber(currentUser.MyRoom);
            list.Sort((view1, view2) =>
            {
                if (Math.Abs(GetFloorInNumber(view1.NumberRoom.ToString()) - userFloor) >
                    Math.Abs(GetFloorInNumber(view2.NumberRoom.ToString()) - userFloor))
                {
                    return 1;
                }
                else
                {
                    if (Math.Abs(GetFloorInNumber(view1.NumberRoom.ToString()) - userFloor) <
                        Math.Abs(GetFloorInNumber(view2.NumberRoom.ToString()) - userFloor))
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            });
            if (list.FindIndex(x => x.NumberRoom == currentUser.FavoriteRoom) > 0)
            {
                RoomViewModel first = list[list.FindIndex(x => x.NumberRoom == currentUser.FavoriteRoom)];
                list.Remove(first);
                list.Insert(0, first);
            }

            return list;
        }

        /// <summary>
        /// Method extracts floor number from room title
        /// </summary>
        /// <param name="roomTitle">Title of room</param>
        /// <returns>floor number</returns>
        private int GetFloorInNumber(string roomTitle)
        {
            return roomTitle[0];
        }
    }
}