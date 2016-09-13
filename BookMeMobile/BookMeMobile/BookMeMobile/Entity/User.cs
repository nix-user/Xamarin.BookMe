using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMeMobile.Entity
{
    public class User
    {
        public int Id { get; set; }

        public string Password { get; set; }

        public string Login { get; set; }

        public string FavoriteRoom { get; set; }

        public string MyRoom { get; set; }
    }
}