using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;

namespace iShop.Web.Server.APIs
{
    [Route("/api/product/{productId}/[controller]/{imageId}")]
    public class ImagesController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IHostingEnvironment _host;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImagesController(IHostingEnvironment host, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _host = host;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> UpLoad(Guid productId, IFormFile file)
        {
            var product = await _unitOfWork.ProductRepository.GetProduct(productId, false);

            if (product == null)
                return NotFound("");

            if (file == null)
                return BadRequest("Null file");

            if (file.Length == 0)
                return BadRequest("Empty file");

            if (file.Length > 5242880)
                return BadRequest("Oversized");

            //if (!photoSettings.isSupported(file.FileName))
            //    return BadRequest("Invalid file type");


            var uploadFolderPath = Path.Combine(_host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadFolderPath))
            {
                Directory.CreateDirectory(uploadFolderPath);
            }

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);

            var filePath = Path.Combine(uploadFolderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var image = new Image { FileName = fileName };
            product.Images.Add(image);

            await _unitOfWork.CompleteAsync();
            return Ok(_mapper.Map<Image, ImageResource>(image));
        }

        //public async Task<IEnumerable<ImageResource>> GetImages(int vehicleId)
        //{
        //    var photos = await (_photoRepository).GetPhotos(vehicleId);
        //    return _mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        //}
    }

}
