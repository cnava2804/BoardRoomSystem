using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardRoomSystem.Models
{
    public class AreasViewModel
    {
        [Key]
        public int IdArea { get; set; }

        [Display(Name ="Nombre de Area")]
        [Column(TypeName ="nvarchar(50)")]
        public string NameArea { get; set; }
        public virtual ICollection<Event> Events { get; set; }

    }
}
