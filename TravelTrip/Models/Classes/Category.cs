using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelTrip.Models.Classes
{
    public class Category
    {
        [Key]
        public int categoryID { get; set; }
        public int mainCategoryID { get; set; }
        public string categoryName { get; set; }
        public string categoryPhoto { get; set; }
        public ICollection<New> News { get; set; }
        public MainCategory MainCategory { get; set; }
    }
}