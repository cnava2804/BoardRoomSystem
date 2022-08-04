using BoardRoomSystem.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BoardRoomSystem.Models
{
    public class MeetingRooms
    {
        [Key]
        public int MTGR_Id { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [Display(Name = "Nombre")]
        [StringLength(100, ErrorMessage = "No debe de tener mas de 70 caracteres.")]
        [MinLength(5, ErrorMessage = "Debe tener mas de 5 caracteres.")]
        public string MTGR_Name { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        [Display(Name = "Descripción")]
        public string MTGR_Description { get; set; }
        [Display(Name = "Número máximo de personas")]
        public int MTGR_MaxNumbPeople { get; set; }
       
        [Column(TypeName = "nvarchar(200)")]
        [Display(Name = "Ubicación")]
        public string MTGR_Location { get; set; }
        [Display(Name = "Imagen")]
        public byte[] MTGR_Image { get; set; }

        //public IEnumerable<Position> Positions { get; set; }


        [Display(Name = "Estado")]
        public int State_Id { get; set; }

        [ForeignKey("State_Id")]
        public States States { get; set; }
    }
}
