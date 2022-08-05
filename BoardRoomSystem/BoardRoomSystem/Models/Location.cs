using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardRoomSystem.Models
{
    public class Location
    {

        [Key]
        public int Location_Id { get; set; }

        [Display(Name = "Ubicación")]
        [Column(TypeName = "nvarchar(200)")]
        public string Location_Name { get; set; }

        public IEnumerable<Reservations> Reservation { get; set; }
    }
}
