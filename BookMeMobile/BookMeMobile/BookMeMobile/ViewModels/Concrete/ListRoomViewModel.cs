using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
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

        public List<RoomViewModel> ListRoom { get; }

        public ICommand ReserveCommand { get; set; }

        public string IsNotRooms
        {
            get { return this.ListRoom.Any() ? null : "Комнат нет"; }
        }

        public ListRoomViewModel(IEnumerable<Room> rooms, SelectModel selectModel, INavigationService navigationService) : base(navigationService)
        {
            this.ListRoom = rooms.Select(x => new RoomViewModel(x, this)).ToList();
            this.ReserveCommand = new Command(this.Reserve);
            this.selectModel = selectModel;
        }

        public void Reserve(object selectElement)
        {
            RoomViewModel selectedRoom = selectElement as RoomViewModel;
            this.NavigationService.ShowViewModel<AddReservationViewModel>(new { filterParameter = this.selectModel, roomModel = selectedRoom }, true);
        }
    }
}