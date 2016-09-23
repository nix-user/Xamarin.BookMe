using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.ViewModels;

namespace BookMeMobile.Model
{
    public class RoomViewModel : BaseViewModel
    {
        public Room Room { get; set; }

        public RoomViewModel()
        {
            this.Room = new Room();
        }

        public int Id
        {
            get { return this.Room.Id; }
            set { this.Room.Id = value; }
        }

        public string NumberRoom
        {
            get { return this.Room.Number; }
            set { this.Room.Number = value; }
        }

        public ListRoomViewModel ListViewModel { get; set; }
    }
}