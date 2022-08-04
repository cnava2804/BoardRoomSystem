using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardRoomSystem.Models
{
    public class Reservations
    {
        [Key]
        public int Reservation_Id { get; set; }

        [Display(Name = "Asunto")]
        public string Reservation_Subject { get; set; }

        [Display(Name = "Ubicación")]
        public string Reservation_Location { get; set; }

        [Display(Name = "Destinatario")]
        public string Reservation_Recipient { get; set; }

        [Display(Name = "Fecha de Inicio")]
        public DateTime Reservation_StartDate { get; set; }
        [Display(Name = "Fecha Final")]
        public DateTime Reservation_EndtDate { get; set; }
        [Display(Name = "Número de Personas")]
        public int Reservation_NumbPeople { get; set; }
        [Display(Name = "Descripción")]
        [Column(TypeName = "nvarchar(300)")]
        public string Reservation_Description { get; set; }
        [Column(TypeName = "nvarchar(300)")]
        [Display(Name = "Encargado de la Reunión")]
        public string Reservation_Delegate { get; set; }
    }
}
