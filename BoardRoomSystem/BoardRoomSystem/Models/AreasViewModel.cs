using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardRoomSystem.Models
{
    public class AreasViewModel
    {
        [Key]
        public int Area_Id { get; set; }

        [Display(Name ="Nombre de Area")]
        [Column(TypeName ="nvarchar(50)")]
        public string Area_Name { get; set; }

        //public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
