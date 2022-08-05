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
        [Column(TypeName = "nvarchar(100)")]
        [Display(Name = "Solicitada Por:")]
        public string Reservation_Delegate { get; set; }

        [Display(Name = "Ubicación")]
        public int Location_id { get; set; }

        [ForeignKey("Location_id")]
        public Location Location { get; set; }

        [Display(Name = "Área quien la solicita")]
        public int Area_Id { get; set; }

        [ForeignKey("Area_Id")]
        public AreasViewModel Areas { get; set; }

        [Display(Name = "Sala a Reservar")]
        public int MTGR_Id { get; set; }

        [ForeignKey("MTGR_Id")]
        public MeetingRooms MeetingRooms { get; set; }
    }
}
