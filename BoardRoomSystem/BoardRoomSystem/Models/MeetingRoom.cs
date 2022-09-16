using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BoardRoomSystem.Models
{
    public class MeetingRoom 
    {
        [Key]
        public int IdMeetR { get; set; }
        [Display(Name = "Sala")]
        public string NameMeetR { get; set; }
        [Display(Name = "Equipamiento")]
        public string DescriptionMeetR { get; set; }
        [Display(Name = "Máximo de personas")]
        public int MaxNumbPeopleMeetR { get; set; }
        [Display(Name = "Imagen")]
        public string ImagePath { get; set; }
        [Display(Name = "Ubicación")]
        public int IdLocation { get; set; }

        [ForeignKey("IdLocation")]
        public virtual Location Location { get; set; }

        [Display(Name = "Estado")]
        public int State_Id { get; set; }

        [ForeignKey("State_Id")]
        public virtual State State { get; set; }
        public virtual ICollection<Event> Events { get; set; }


        //public virtual ICollection<Reservations> Reservations { get; set; }
        //[Display(Name = "Estado")]
        //public int State_Id { get; set; }

        //[ForeignKey("State_Id")]
        //public virtual State States { get; set; }

        //[Display(Name = "Ubicación")]
        //public int IdLocation { get; set; }
        //[ForeignKey("IdLocation")]
        //public virtual Location Location { get; set; }

    }
}
