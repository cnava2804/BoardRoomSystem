using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardRoomSystem.Models
{
    public class State
    {
        [Key]
        public int State_Id { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        [Display(Name ="Estado")]
        public string state_Name { get; set; }
        public virtual ICollection<MeetingRoom> MeetingRooms { get; set; }

        //public virtual ICollection<MeetingRoom> MeetingRooms { get; set; }


    }
}
