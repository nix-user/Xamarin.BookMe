using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using Javax.Security.Auth;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class ProfilePage : ContentPage
    {
        private ProfileViewModel profileViewModel;
        private User currentUser;

        public ProfilePage()
        {
            this.InitializeComponent();
            this.txtFavoriteRoomCell.PropertyChanged += this.FavoriteRoomCell_OnCompleted;
            this.txtMyRoomCell.PropertyChanged += this.MyRoomCell_OnCompleted;
        }

        private void BtnSave_OnClicked(object sender, EventArgs e)
        {
            this.profileViewModel = (ProfileViewModel)this.BindingContext;
            btnSave.IsEnabled = false;
        }

        private void FavoriteRoomCell_OnCompleted(object sender, EventArgs e)
        {
            if (this.txtFavoriteRoomCell.Text != this.currentUser.FavoriteRoom)
            {
                this.VisibleButon();
            }
        }

        private void MyRoomCell_OnCompleted(object sender, EventArgs e)
        {
            if (this.txtMyRoomCell.Text != this.currentUser.MyRoom.ToString())
            {
                this.VisibleButon();
            }
        }

        private void VisibleButon()
        {
            if (this.btnSave != null)
            {
                this.btnSave.IsEnabled = true;
            }
        }
    }
}