using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardRoomSystem.Helpers
{
    public static class JSONListHelper
    {
        public static string GetReservationListJSONString(List<Models.Reservations> reservations)
        {
            var reservationlist = new List<Reservations>();
            foreach (var model in reservations)
            {
                var myreservations = new Reservations()
                {
                    id = model.Reservation_Id,
                    start = model.Reservation_StartDate,
                    end = model.Reservation_EndtDate,
                    resourceId = model.Location.Location_Id,
                    description = model.Reservation_Description,
                    title = model.Reservation_Subject
                };
                reservationlist.Add(myreservations);
            }
            return System.Text.Json.JsonSerializer.Serialize(reservationlist);
        }

        public static string GetResourceListJSONString(List<Models.Location> locations)
        {
            var resourcelist = new List<Resource>();

            foreach (var loc in locations)
            {
                var resource = new Resource()
                {
                    id = loc.Location_Id,
                    title = loc.Location_Name
                };
                resourcelist.Add(resource);
            }
            return System.Text.Json.JsonSerializer.Serialize(resourcelist);
        }
    }

    public class Reservations
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int resourceId { get; set; }
        public string description { get; set; }
    }

    public class Resource
    {
        public int id { get; set; }
        public string title { get; set; }

    }
}
