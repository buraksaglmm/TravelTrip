using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelTrip.Models.Classes;

namespace TravelTrip.Models.Classes
{
    public class NewsComments
    {
        public IEnumerable<New> Deger1 { get; set; }
        public IEnumerable<New> Deger3 { get; set; }
        public IEnumerable<Comment> Deger2 { get; set; }
        public IEnumerable<Comment> Deger4 { get; set; }
        public IEnumerable<Category> Deger5 { get; set; }
        public IEnumerable<Editor> Deger6 { get; set; }
        public IEnumerable<New> Deger7 { get; set; }
        public IEnumerable<Category> Deger8 { get; set; }
        public IEnumerable<MainCategory> Deger9 { get; set; }

    }
}