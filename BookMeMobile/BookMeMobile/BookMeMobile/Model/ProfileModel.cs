using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Model
{
    public class ProfileModel
    {
        public ProfileModel(ProfileModel model)
        {
            this.FavouriteRoom = model.FavouriteRoom;
            this.Floor = model.Floor;
        }

        public ProfileModel()
        {
            this.Floor = 1;
            this.FavouriteRoom = string.Empty;
        }

        public int Floor { get; set; }

        public string FavouriteRoom { get; set; }

        public override bool Equals(object obj)
        {
            ProfileModel secondObj = (ProfileModel)obj;
            return this.GetHashCode() == secondObj.GetHashCode();
        }

        public override int GetHashCode()
        {
            return this.Floor + this.FavouriteRoom.GetHashCode();
        }
    }
}