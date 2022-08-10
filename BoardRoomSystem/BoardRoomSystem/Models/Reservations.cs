using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BoardRoomSystem.Areas.Identity.Data;

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

        //[Display(Name = "Ubicación")]
        //public int Location_id { get; set; }

        //[ForeignKey("Location_id")]
        //public Location Location { get; set; }

        //[Display(Name = "Área quien la solicita")]
        //public int Area_Id { get; set; }

        //[ForeignKey("Area_Id")]
        //public AreasViewModel Areas { get; set; }

        //[Display(Name = "Sala a Reservar")]
        //public int MTGR_Id { get; set; }

        //[ForeignKey("MTGR_Id")]
        //public MeetingRooms MeetingRooms { get; set; }

        //Relational data

        public virtual Location Location { get; set; }
        public virtual AreasViewModel AreasViewModel { get; set; }
        public virtual MeetingRooms MeetingRooms { get; set; }
        public virtual ApplicationUser User { get; set; }

        public Reservations(IFormCollection form, Location location, ApplicationUser user)
        {
            User = user;
            Reservation_Subject = form["Reservations.Reservation_Subject"].ToString();
            Reservation_Recipient = form["Reservations.Reservation_Recipient"].ToString();
            Reservation_StartDate = DateTime.Parse(form["Reservations.Reservation_StartDate"].ToString());
            Reservation_EndtDate = DateTime.Parse(form["Reservations.Reservation_EndtDate"].ToString());
            Reservation_NumbPeople = Convert.ToInt32(form["Reservations.Reservation_NumbPeople"].ToString());
            Reservation_Description = form["Reservations.Reservation_Description"].ToString();
            Reservation_Delegate = form["Reservations.Reservation_Delegate"].ToString();
            Location = location;
        }

        public void UpdateReservations(IFormCollection form, Location location, ApplicationUser user)
        {
            User = user;
            Reservation_Subject = form["Reservations.Reservation_Subject"].ToString();
            Reservation_Recipient = form["Reservations.Reservation_Recipient"].ToString();
            Reservation_StartDate = DateTime.Parse(form["Reservations.Reservation_StartDate"].ToString());
            Reservation_EndtDate = DateTime.Parse(form["Reservations.Reservation_EndtDate"].ToString());
            Reservation_NumbPeople = Convert.ToInt32(form["Reservations.Reservation_NumbPeople"].ToString());
            Reservation_Description = form["Reservations.Reservation_Description"].ToString();
            Reservation_Delegate = form["Reservations.Reservation_Delegate"].ToString();
            Location = location;
        }

        public Reservations()
        {

        }


    }
}
