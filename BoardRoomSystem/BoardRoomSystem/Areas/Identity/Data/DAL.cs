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
            var user = db.Users.FirstOrDefault(x => x.Id == form["UserId"].ToString());
            var newreservation = new Reservations(form, db.Locations.FirstOrDefault(x => x.Location_Name == locname), user);
            db.Reservations.Add(newreservation);
            db.SaveChanges();
        }

        public void UpdateReservations(IFormCollection form)
        {
            var locname = form["Location"].ToString();
            var reservationid = int.Parse(form["Reservations.Reservation_Id"]);
            var myreservation = db.Reservations.FirstOrDefault(x => x.Reservation_Id == reservationid);
            var location = db.Locations.FirstOrDefault(x => x.Location_Name == locname);
            var user = db.Users.FirstOrDefault(x => x.Id == form["UserId"].ToString());
            myreservation.UpdateReservations(form, location, user);
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

    }
}
