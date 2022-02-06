using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Restaurant_App.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string MealName { get; set; }
        public Day Day { get; set; }
        public DateTime Date { get; set; }

        public User user { get; set; }
        public Meal meal { get; set; }
    }
}