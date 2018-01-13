using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Web.Server.Commons.Extensions;
using iShop.Web.Server.Commons.Helpers;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace iShop.Web.Server.APIs
{
    [Route("/api/product/{productId}/[controller]")]
    public class ImagesController : BaseController
    {
        private readonly IHostingEnvironment _host;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ImageSettings _imageSettings;
        private readonly ILogger<ImagesController> _logger;

        public ImagesController(IHostingEnvironment host, IUnitOfWork unitOfWork, IMapper mapper,
            ImageSettings imageSettings, ILogger<ImagesController> logger)
        {
            _host = host;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageSettings = imageSettings;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> UpLoad(string productId, IFormFile file)
        {
            bool isValid = Guid.TryParse(productId, out var id);

            if (!isValid)
                return InvalidId(productId);
            var product = await _unitOfWork.ProductRepository.GetProduct(id, false);

            if (product == null)
                return NotFound(ItemName.Product, id);

            if (file == null)
                return NullOrEmpty(ItemName.Image);

            if (file.Length == 0)
                return NullOrEmpty(ItemName.Image);

            if (file.Length > _imageSettings.MaxByte)
                return Oversized(ItemName.Image, _imageSettings.MaxByte);

            if (!_imageSettings.IsSupported(file.FileName))
                return UnSupportedType(ItemName.Image, _imageSettings.AcceptedTypes);


            var uploadFolderPath = Path.Combine(_host.WebRootPath, "images");

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

            var image = new Image { FileName = fileName, ProductId = id};
            await _unitOfWork.ImageRepository.AddAsync(image);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.Fail, ItemName.Image, image.Id);
                return FailedToSave(ItemName.Image, image.Id);
            }
            _logger.LogMessage(LoggingEvents.Created, ItemName.Image, image.Id);

            return Ok(_mapper.Map<Image, ImageResource>(image));
        }


        //[HttpGet("{productId}")]
        //public async Task<IActionResult> Get(Guid productId)
        //{
        //    var images = await _unitOfWork.ImageRepository.GetProductImages(productId);
        //    var imageResource = _mapper.Map<IEnumerable<Image>, IEnumerable<ImageResource>>(images);
        //    return Ok(imageResource);
        //}
    }

}
