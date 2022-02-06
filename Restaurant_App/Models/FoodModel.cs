using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Restaurant_App.Models
{
    public class FoodModel
    {
        public List<SelectListItem> Foods { get; set; }
        public int? FoodId { get; set; }
    }
}