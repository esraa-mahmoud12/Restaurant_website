using System;
using System.Collections.Generic;

namespace Restaurant_App.Models
{

    public enum Day
    {
        Saturday = 1,
        Sunday = 2,
        Monday = 3,
        Tuesday = 4,
        Wednsday = 5,
        Thursday = 6,
        Friday = 7

    }

    

    public class Meal
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public Day Day { get; set; }
        public DateTime Date { get; set; }

        public User user { get; set; }
        

    }
}