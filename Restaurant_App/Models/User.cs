using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_App.Models
{

    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool isAdmin { get; set; }

        public ICollection<Meal> Meals { get; set; }

         
    }
}