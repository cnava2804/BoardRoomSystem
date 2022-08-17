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
        public List<SelectListItem> AreasViewModel = new List<SelectListItem>();
        public List<SelectListItem> MeetingRooms = new List<SelectListItem>();
        public string LocationName { get; set; }
        public string AreasVMName { get; set; }
        public string MeetingRName { get; set; }

        //private List<Location> locations;
        //public string UserId { get; set; }

        public ReservationsViewModel(Reservations myreservations, List<Location> locations, List<AreasViewModel> areasViewModels, List<MeetingRooms> meetingRooms)
        {
            Reservations = myreservations;
            LocationName = myreservations.Location.Location_Name;
            AreasVMName = myreservations.AreasViewModel.Area_Name;
            MeetingRName = myreservations.MeetingRooms.MTGR_Name;
            
            //UserId = userid;
            foreach (var loc in locations)
            {
                Location.Add(new SelectListItem() { Text = loc.Location_Name });
            }
            foreach (var areaVM in areasViewModels)
            {
                AreasViewModel.Add(new SelectListItem() { Text = areaVM.Area_Name });
            }
            foreach (var meetR in meetingRooms)
            {
                MeetingRooms.Add(new SelectListItem() { Text = meetR.MTGR_Name });
            }
        }

        public ReservationsViewModel(List<Location> locations, List<AreasViewModel> areasViewModels, List<MeetingRooms> meetingRooms)
        {
            //UserId = userid;
            foreach (var loc in locations)
            {
                Location.Add(new SelectListItem() { Text = loc.Location_Name });
            }
            foreach (var areaVM in areasViewModels)
            {
                AreasViewModel.Add(new SelectListItem() { Text = areaVM.Area_Name });
            }
            foreach (var meetR in meetingRooms)
            {
                MeetingRooms.Add(new SelectListItem() { Text = meetR.MTGR_Name });
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
