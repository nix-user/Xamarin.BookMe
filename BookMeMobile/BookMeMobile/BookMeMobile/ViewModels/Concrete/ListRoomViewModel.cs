using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.Entity;
using BookMeMobile.Pages;
using BookMeMobile.ViewModels;
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

        public ListRoomViewModel(IEnumerable<Room> rooms, SelectModel selectModel)
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

        public async void Reserve(object selectElement)
        {
            RoomViewModel selectedRoom = selectElement as RoomViewModel;
            await this.Navigation.PushModalAsync(new AddReservationPage(this.selectModel, selectedRoom));
        }
    }
}