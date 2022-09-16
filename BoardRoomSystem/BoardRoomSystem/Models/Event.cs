using BoardRoomSystem.Areas.Identity.Data;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardRoomSystem.Models
{
    public class Event
    {
        [Key]
        public int EventID { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public System.DateTime Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }

        [Display(Name = "Location")]
        public int IdLocation { get; set; }
        [ForeignKey("IdLocation")]
        public virtual Location Location { get; set; }

        //public int IdMeetR { get; set; }
        //public virtual MeetingRoom MeetingRoom { get; set; }

        [Display(Name = "Sala")]
        public int IdMeetR { get; set; }

        [ForeignKey("IdMeetR")]
        public virtual MeetingRoom MeetingRoom { get; set; }

        [Display(Name = "Usuario")]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
