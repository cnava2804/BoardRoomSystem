using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardRoomSystem.Models.ViewModel
{
    public class ImageCreateModel
    {
        [Key]
        public int IdMeetR { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [Display(Name = "Nombre")]
        [StringLength(100, ErrorMessage = "No debe de tener mas de 70 caracteres.")]
        [MinLength(5, ErrorMessage = "Debe tener mas de 5 caracteres.")]
        public string NameMeetR { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        [Display(Name = "Equipamiento")]
        public string DescriptionMeetR { get; set; }
        [Display(Name = "Capacidad máxima de personas")]
        public int MaxNumbPeopleMeetR { get; set; }

        [Required(ErrorMessage = "Elija una imagen")]
        [Display(Name = "Imagen")]
        public IFormFile ImagePath { get; set; }

        [Display(Name = "Ubicación")]
        public int IdLocation { get; set; }
        [Display(Name = "Estado")]
        public int State_Id { get; set; }
    }
}
