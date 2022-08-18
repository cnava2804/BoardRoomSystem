using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BoardRoomSystem.Models
{
    public class ImagesModel
    {
        [Display(Name = "Image Cover")]
        public IFormFile CoverPhoto { get; set; }
    }
}
