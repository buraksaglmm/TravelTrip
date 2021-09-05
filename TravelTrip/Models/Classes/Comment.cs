using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TravelTrip.Models.Classes
{
    public class Comment
    {
        [Key]
        public int commentID { get; set; }
        public virtual New New { get; set; }
        public int commentNewId { get; set; }
        public string commentAvatar { get; set; }
        public string commentUsername { get; set; }
        public string commentMail { get; set; }
        public string commentMessage { get; set; }
        public DateTime commentTime { get; set; }
        public bool commentConfirm { get; set; }

    }
}