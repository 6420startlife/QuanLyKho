using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using FoodStore_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using QuanLyKho.Data;

namespace FoodStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CloudinaryController : ControllerBase
    {
        private readonly QuanLyKhoContext _context;
        private readonly Cloudinary_Info _cloudinary;
        private readonly IWebHostEnvironment _enviroment;
        public CloudinaryController(QuanLyKhoContext context, IOptionsMonitor<Cloudinary_Info> monitor, IWebHostEnvironment enviroment)
        {
            _context = context;
            _cloudinary = monitor.CurrentValue;
            _enviroment = enviroment;
        }

        [HttpPost]
        public IActionResult postImage(IFormFile file)
        {
            if (file.Length == 0)
            {
                return BadRequest();
            }
            string fileDirectory = Path.Combine(_enviroment.ContentRootPath,"Upload");
            if (!Directory.Exists(fileDirectory))
            {
                Directory.CreateDirectory(fileDirectory);
            }
            string filePath = Path.Combine(fileDirectory, file.FileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            Account account = new Account
            {
                Cloud = _cloudinary.CloudName,
                ApiKey = _cloudinary.API_Key,
                ApiSecret = _cloudinary.API_Secret
            };
            Cloudinary cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(filePath)
            };
            try
            {
                var uploadResult = cloudinary.Upload(uploadParams);
                FileInfo fileInfo = new FileInfo(filePath);
                fileInfo.Delete();
                return Ok(new ApiResponse
                {
                    Data = uploadResult.SecureUri.AbsoluteUri,
                    Success = true,
                    Message = "Upload ảnh thành công"
                });
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
