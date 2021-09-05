using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TravelTrip.Models.Classes
{
    public class New
    {
        [Key]
        public int newID { get; set; }
        public string newTitle { get; set; }
        [AllowHtml]
        public string newDescription { get; set; }
        [AllowHtml]
        public string newDetail { get; set; }
        public string newPhoto { get; set; }
        
        public DateTime newtime { get; set; }

        public int newViewNumber { get; set; }

        public int newCategoryId { get; set; }
        public Category newCategory { get; set; }

        public int newMainCategoryId { get; set; }
        public MainCategory newMainCategory { get; set; }

        public int newEditorId { get; set; }
        public Editor newEditor { get; set; }

        public ICollection<Comment> Comments { get; set; }

        [NotMapped]
        public string newPhotoBase64 { get; set; }
    }
}