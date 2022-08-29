using BoardRoomSystem.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoardRoomSystem.Data;

namespace BoardRoomSystem.Areas.Identity.Data
{
    public interface IDAL
    {
        public List<Reservations> GetReservations();
        public List<Reservations> GetMyReservations(string userid);
        public Reservations GetReservations(int id);
        public void CreateReservations(IFormCollection form);
        public void UpdateReservations(IFormCollection form);
        public void DeleteReservations(int id);
        public List<Location> GetLocations();
        public Location GetLocation(int id);
        public void CreateLocation(Location location);
        public List<AreasViewModel> GetAreasViewModel();
        public AreasViewModel GetAreaVM(int id);
        public void CreateAreaVM(AreasViewModel areasViewModel);
        public List<MeetingRooms> GetMeetingRooms();
        public MeetingRooms GetMeetingRoom(int id);
        public void CreateMeetingRoom(MeetingRooms meetingRooms);
    }

    public class DAL : IDAL
    {
        private BoardRoomSystemDBContext db = new BoardRoomSystemDBContext();

        public List<Reservations> GetReservations()
        {
            return db.Reservations.ToList();
        }

        public List<Reservations> GetMyReservations(string userid)
        {
            return db.Reservations.Where(x => x.User.Id == userid).ToList();
        }

        public Reservations GetReservations(int id)
        {
            return db.Reservations.FirstOrDefault(x => x.Reservation_Id == id);
        }


        public void CreateReservations(IFormCollection form)
        {
           
            var locname = form["Location"].ToString();
            var areaVM = form["AreasViewModel"].ToString();
            var meetR = form["MeetingRooms"].ToString();
            var user = db.Users.FirstOrDefault(x => x.Id == form["UserId"].ToString());

            var newreservation = new Reservations(form, db.Locations.FirstOrDefault(x => x.Location_Name == locname), user, 
            db.AreasViewModels.FirstOrDefault(a => a.Area_Name == areaVM), db.MeetingRooms.FirstOrDefault(m => m.MTGR_Name == meetR));
            db.Reservations.Add(newreservation);
            db.SaveChanges();
        }

        public void UpdateReservations(IFormCollection form)
        {
            var locname = form["Location"].ToString();
            var areaVM = form["AreasViewModel"].ToString();
            var meetR = form["MeetingRooms"].ToString();
            var reservationid = int.Parse(form["Reservations.Reservation_Id"]);
            var myreservation = db.Reservations.FirstOrDefault(x => x.Reservation_Id == reservationid);
            var location = db.Locations.FirstOrDefault(x => x.Location_Name == locname);
            var areasViewModel = db.AreasViewModels.FirstOrDefault(a => a.Area_Name == locname);
            var meetingRooms = db.MeetingRooms.FirstOrDefault(m => m.MTGR_Name == locname);
            var user = db.Users.FirstOrDefault(x => x.Id == form["UserId"].ToString());

            myreservation.UpdateReservations(form, location, user, areasViewModel, meetingRooms);
            db.Entry(myreservation).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteReservations(int id)
        {
            var myreservation = db.Reservations.Find(id);
            db.Reservations.Remove(myreservation);
            db.SaveChanges();
        }

        public List<Location> GetLocations()
        {
            return db.Locations.ToList();
        }

        public Location GetLocation(int id)
        {
            return db.Locations.Find(id);
        }

        public void CreateLocation(Location location)
        {
            db.Locations.Add(location);
            db.SaveChanges();
        }

        public List<AreasViewModel> GetAreasViewModel()
        {
            return db.AreasViewModels.ToList();
        }

        public AreasViewModel GetAreaVM(int id)
        {
            return db.AreasViewModels.Find(id);
        }

        public void CreateAreaVM(AreasViewModel areasViewModel)
        {
            db.AreasViewModels.Add(areasViewModel);
            db.SaveChanges();
        }

        public List<MeetingRooms> GetMeetingRooms()
        {
            return db.MeetingRooms.ToList();
        }

        public MeetingRooms GetMeetingRoom(int id)
        {
            return db.MeetingRooms.Find(id);
        }

        public void CreateMeetingRoom(MeetingRooms meetingRooms)
        {
            db.MeetingRooms.Add(meetingRooms);
            db.SaveChanges();
        }

    }
}
