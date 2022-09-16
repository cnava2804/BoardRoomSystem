using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardRoomSystem.Models
{
    public class Location
    {

        [Key]
        public int IdLocation { get; set; }

        [Display(Name = "Ubicación")]
        [Column(TypeName = "nvarchar(200)")]
        public string NameLocation { get; set; }

        public virtual ICollection<MeetingRoom> MeetingRooms { get; set; }
        public virtual ICollection<Event> Events { get; set; }

        //public virtual ICollection<MeetingRoom> MeetingRooms { get; set; }
        ////Relational data
        //public virtual ICollection<Reservations> Reservations { get; set; }

        //public Location()
        //{
        //    MeetingRooms = new HashSet<MeetingRoom>();
        //}
    }
}
