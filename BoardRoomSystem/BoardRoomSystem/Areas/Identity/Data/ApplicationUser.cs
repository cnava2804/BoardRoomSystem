using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using BoardRoomSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace BoardRoomSystem.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the AplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}

