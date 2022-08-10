using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardRoomSystem.Models;

namespace BoardRoomSystem.Models.ViewModels
{
    public class ReservationsViewModel
    {
        public Reservations Reservations { get; set; }
        public List<SelectListItem> Location = new List<SelectListItem>();
        public string LocationName { get; set; }

        //private List<Location> locations;
        //public string UserId { get; set; }

        public ReservationsViewModel(Reservations myreservations, List<Location> locations)
        {
            Reservations = myreservations;
            LocationName = myreservations.Location.Location_Name;
            //UserId = userid;
            foreach (var loc in locations)
            {
                Location.Add(new SelectListItem() { Text = loc.Location_Name });
            }
        }

        public ReservationsViewModel(List<Location> locations)
        {
            //UserId = userid;
            foreach (var loc in locations)
            {
                Location.Add(new SelectListItem() { Text = loc.Location_Name });
            }
        }

        public ReservationsViewModel()
        {

        }

        //public ReservationsViewModel(List<Location> locations)
        //{
        //    this.locations = locations;
        //}
    }
}
