using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Entity
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