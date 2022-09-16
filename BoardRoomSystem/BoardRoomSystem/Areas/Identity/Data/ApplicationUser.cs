using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardRoomSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace BoardRoomSystem.Areas.Identity.Data;

// Add profile data for application users by adding properties to the ApplicationUser class
public class ApplicationUser : IdentityUser
{
    public string FirstNameUser { get; set; }
    public string LastNameUser { get; set; }
    //public IEnumerable<Event> Events { get; set; }
}
public class ApplicationRole : IdentityRole
{

}

