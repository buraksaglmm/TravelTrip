using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelTrip.Models.Classes
{
    public class MainCategory
    {
        [Key]
        public int mainCategoryID { get; set; }
        public string mainCategoryName { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<New> News { get; set; }


    }
}