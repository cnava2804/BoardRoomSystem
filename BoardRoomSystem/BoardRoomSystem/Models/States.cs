using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardRoomSystem.Models
{
    public class States
    {
        [Key]
        public int State_Id { get; set; }
        [Column(TypeName = "nvarchar(30)")]
        public int state_Name { get; set; }

        public IEnumerable<MeetingRooms> MeetingRooms { get; set; }

    }
}
