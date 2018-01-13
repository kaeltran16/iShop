using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Web.Server.Commons.Extensions;
using iShop.Web.Server.Commons.Helpers;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;
using iShop.Web.Server.Persistent.Repositories.Contracts;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.Server.APIs
{

    [Route("/api/[controller]")]
    public class ProductsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<ProductsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // GET
        [HttpGet("{id}", Name = ItemName.Product)]
        public async Task<IActionResult> Get(string id)
        {
            bool isValid = Guid.TryParse(id, out var productId);
            if (!isValid)
                return InvalidId(id);

            var product = await _unitOfWork.ProductRepository.GetProduct(productId);

            if (product == null)
                NotFound(ItemName.Product, productId);

            var productResource = _mapper.Map<Product, ProductResource>(product);

            return Ok(productResource);
        }

        // GET 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _unitOfWork.ProductRepository.GetProducts();

            var productResources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);

            return Ok(productResources);
        }

        // POST
        //[Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SavedProductResource savedProductResources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<SavedProductResource, Product>(savedProductResources);

            await _unitOfWork.ProductRepository.AddAsync(product);
            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.SavedFail, ItemName.Product, product.Id);
                return FailedToSave(ItemName.Category, product.Id);
            }

            product = await _unitOfWork.ProductRepository.GetProduct(product.Id);

            var result = _mapper.Map<Product, ProductResource>(product);

            _logger.LogMessage(LoggingEvents.Created, ItemName.Product, product.Id);

            return CreatedAtRoute(ItemName.Product, new { id = product.Id }, result);
        }

        // PUT
        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SavedProductResource savedProductResource)
        {
            bool isValid = Guid.TryParse(id, out var productId);
            if (!isValid)
                return InvalidId(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _unitOfWork.ProductRepository.GetProduct(productId);

            if (product == null)
                return NotFound(ItemName.Product, productId);

            _mapper.Map<SavedProductResource, Product>(savedProductResource, product);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.SavedFail, ItemName.Product, product.Id);
                return FailedToSave(ItemName.Category, product.Id);
            }

            product = await _unitOfWork.ProductRepository.GetProduct(product.Id);

            var result = _mapper.Map<Product, SavedProductResource>(product);

            _logger.LogMessage(LoggingEvents.Updated, ItemName.Product, product.Id);

            return Ok(result);
        }

        // DELETE
        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            bool isValid = Guid.TryParse(id, out var productId);
            if (!isValid)
                return InvalidId(id);

            var product = await _unitOfWork.ProductRepository.GetProduct(productId);

            if (product == null)
                return NotFound(ItemName.Product, productId);

            _unitOfWork.ProductRepository.Remove(product);
            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.SavedFail, ItemName.Product, product.Id);
                return FailedToSave(ItemName.Category, product.Id);
            }

            _logger.LogMessage(LoggingEvents.Deleted, ItemName.Product, product.Id);


            return NoContent();
        }
    }
}