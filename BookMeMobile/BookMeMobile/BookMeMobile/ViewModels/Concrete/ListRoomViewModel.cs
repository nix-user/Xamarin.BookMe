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
using Xamarin.Forms;

namespace BookMeMobile.Model
{
    public class ListRoomViewModel : BaseViewModel
    {
        private SelectModel selectModel;

        public List<RoomViewModel> ListRoom { get; }

        public ICommand ReserveCommand { get; set; }

        public string IsNotRooms
        {
            get { return this.ListRoom.Any() ? null : "Комнат нет"; }
        }

        public ListRoomViewModel(IEnumerable<Room> rooms, INavigationService navigationService, SelectModel selectModel) : base(navigationService)
        {
            this.ListRoom = rooms.Select(x => new RoomViewModel()
            {
                Id = x.Id,
                NumberRoom = x.Number,
                ListViewModel = this
            }).OrderBy(r => r.NumberRoom).ToList();

            this.ReserveCommand = new Command(this.Reserve);
            this.selectModel = selectModel;
        }

        public async void Reserve(object selectElement)
        {
            RoomViewModel selectedRoom = selectElement as RoomViewModel;
            await this.NavigationService.XamarinNavigation.PushModalAsync(new AddReservationPage(this.selectModel, selectedRoom));
        }
    }
}