using System;
using Microsoft.AspNetCore.Http;

namespace DatingAPP.API.Dtos
{
    public class PhotoForCreationDto
    {
        public string Url { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public string PublicId { get; set; }

        //Constructor
        public PhotoForCreationDto()
        {
            DateAdded = DateTime.Now;
        }
    }
}