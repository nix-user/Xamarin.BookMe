using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2.BL;
using App2.Entity;
using Java.Nio.Channels;
using Xamarin.Forms;

namespace App2.Pages
{
    public partial class MyBooks : ContentPage
    {
        public List<MyBookViewResult> ResultRoom { get; set; }
        public User CurrentUser { get; set; }
        public MyBooks(User user)
        {
            InitializeComponent();
            ListRoomManager manager=new ListRoomManager(user);
            ResultRoom = manager.GetUserBookings();
            if (ResultRoom.Any())
            {
                listRoom.BindingContext = ResultRoom;
            }
            else
            {
                listRoom.Header = ListRoomIsEmpty();
            }
        }

        public Label ListRoomIsEmpty()
        {
            return new Label()
            {
                Text = "Комнат нет",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 24
            };
        }
       
    }
}
