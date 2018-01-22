using System;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Web.Server.Commons.Extensions;
using iShop.Web.Server.Commons.Helpers;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            IOptionsSnapshot<ImageSettings> imageSettings, ILogger<ImagesController> logger)
        {
            _host = host;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageSettings = imageSettings.Value;
            _logger = logger;
        }
        [Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpPost]
        public async Task<IActionResult> UpLoad(string productId, IFormFile file)
        {
            bool isValid = Guid.TryParse(productId, out var id);

            if (!isValid)
                return InvalidId(productId);
            var product = await _unitOfWork.ProductRepository.GetProduct(id, false);

            if (product == null)
                return NotFound(id);

            if (file == null || file.Length == 0) 
                return NullOrEmpty();

            if (file.Length > _imageSettings.MaxByte)
                return InvalidSize( ApplicationConstants.ControllerName.Image, _imageSettings.MaxByte);

            if (!_imageSettings.IsSupported(file.FileName))
                return UnSupportedType(_imageSettings.AcceptedTypes);


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
                _logger.LogMessage(LoggingEvents.Fail,  ApplicationConstants.ControllerName.Image, image.Id);
                return FailedToSave(image.Id);
            }
            _logger.LogMessage(LoggingEvents.Created,  ApplicationConstants.ControllerName.Image, image.Id);

            return Ok(_mapper.Map<Image, ImageResource>(image));
        }


        //[HttpGet("{productId}")]
        //public async Task<IActionResult> Get(Guid productId)
        //{
        //    var images = await _unitOfWork.ImageRepository.GetProductImages(productId);
        //    var imageResource = _mapper.Map<IEnumerable<Image>, IEnumerable<ImageResource>>(images);
        //    return Ok(imageResource);
        //}

        // DELETE
        [Authorize(Policy = "SuperUsers")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool isValid = Guid.TryParse(id, out var imageId);

            if (!isValid)
                return InvalidId(id);

            var image = await _unitOfWork.ImageRepository.Get(imageId);

            if (image == null)
                return NullOrEmpty();

            _unitOfWork.ImageRepository.Remove(image);
            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.Fail, ApplicationConstants.ControllerName.Category, image.Id);
                return FailedToSave(image.Id);
            }

            _logger.LogMessage(LoggingEvents.Deleted, ApplicationConstants.ControllerName.Category, image.Id);
            return NoContent();
        }
    }

}
