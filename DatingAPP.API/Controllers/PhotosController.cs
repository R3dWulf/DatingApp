using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DatingAPP.API.Data;
using DatingAPP.API.Dtos;
using DatingAPP.API.Helpers;
using DatingAPP.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DatingAPP.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        //Constructor
        public PhotosController(
            IDatingRepository repo,
            IMapper mapper,
            IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _mapper = mapper;
            _cloudinaryConfig = cloudinaryConfig;
            _repo = repo;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repo.GetPhoto(id);

            var photo = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photo);
        }


        //Method
        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userId, PhotoForCreationDto photoDto)
        {
            var user = await _repo.GetUser(userId);

            if (user == null)
                return BadRequest("Cannot find user");
            
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            if (currentUserId != userId)
                return Unauthorized();

            var file = photoDto.File;

            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using ( var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream)
                    };

                    uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoDto.Url = uploadResult.Uri.ToString();
            photoDto.PublicId = uploadResult.PublicId;

            var photo = _mapper.Map<Photo>(photoDto);

            photo.User = user;

            if (!user.Photos.Any(m => m.IsMain))
                photo.IsMain = true;

            user.Photos.Add(photo);

            var photoToReturn = _mapper.Map<PhotoForReturnDto>(photo);

            if (await _repo.SaveAll())
            {
                return CreatedAtRoute("GetPhoto", new { id = photo.id }, photoToReturn);
            }
            return BadRequest("Could not add photo");  
        }
    }
}