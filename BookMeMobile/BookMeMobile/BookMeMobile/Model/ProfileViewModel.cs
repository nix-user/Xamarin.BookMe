using BookMeMobile.Entity;

namespace BookMeMobile.Model
{
   public class ProfileViewModel
    {
       public ProfileViewModel(User user)
       {
           this.FavoriteRoom = user.FavoriteRoom;
           this.MyRoom = user.MyRoom;
           this.UserName = user.Login;
       }

       public string UserName { get; set; }

       public string FavoriteRoom { get; set; }

       public string MyRoom { get; set; }
    }
}