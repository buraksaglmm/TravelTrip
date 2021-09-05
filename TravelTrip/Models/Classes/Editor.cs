using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelTrip.Models.Classes
{
    public class Editor
    {
        [Key]
        public int editorID { get; set; }
        public string editorUsername { get; set; }
        public string editorFirstname { get; set; }
        public string editorLastname { get; set; }
        public string editorDetail { get; set; }
        public string editorEmail { get; set; }
        public string EditorPhoto { get; set; }
        public string editorPassword { get; set; }
        public string editorState { get; set; }
        public ICollection<New> News { get; set; }

    }
}