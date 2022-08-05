using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardRoomSystem.Models
{
    public class Buildings
    {
        [Key]
        public int Building_Id { get; set; }

        [Display(Name ="Nombre del Edificio")]
        [Column(TypeName ="nvarchar(70)")]

        public string Building_Name { get; set; }

        public IEnumerable<MeetingRooms> MeetingRooms { get; set; }
    }
}
