using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Web.Helpers;
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
    public class ProductsController : Microsoft.AspNetCore.Mvc.Controller
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
        [HttpGet("{id}", Name = GetName.Product)]
        public async Task<IActionResult> Get(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductId(id);

            if (product == null)
                return NotFound(
                    new ErrorMessage { Code = 404, Message = "item with id " + id + " not existed" }.ToString());

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

            var products = await _unitOfWork.ProductRepository.GetProducts();
            foreach (var prod in products)
            {
                // if we do, return bad request
                if (prod.Title.Equals(savedProductResources.Title))
                    return BadRequest(new ErrorMessage()
                    {
                        Code = 400,
                        Message = "category with name " + savedProductResources.Title + " exists"
                    }.ToString());
            }

            var product = _mapper.Map<SavedProductResource, Product>(savedProductResources);

            await _unitOfWork.ProductRepository.AddAsync(product);
            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogError(LoggingEvents.Fail, "item with id " + product.Id + " failed to saved");
                return StatusCode(500,
                    new ErrorMessage { Code = 500, Message = "item with id " + product.Id + " failed to saved" }
                        .ToString());
            }

            product = await _unitOfWork.ProductRepository.GetProductId(product.Id);

            var result = _mapper.Map<Product, ProductResource>(product);

            _logger.LogInformation(LoggingEvents.Created, "item with id " + product.Id + " is created");

            return CreatedAtRoute(GetName.Product, new { id = product.Id }, result);
        }

        // PUT
        //[Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] SavedProductResource savedProductResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _unitOfWork.ProductRepository.GetProductId(id);

            if (product == null)
                return NotFound(
                    new ErrorMessage { Code = 404, Message = "item with id " + id + " not existed" }.ToString());

            _mapper.Map<SavedProductResource, Product>(savedProductResource, product);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogError(LoggingEvents.Fail, "item with id " + id + " failed to saved");
                return StatusCode(500,
                    new ErrorMessage { Code = 500, Message = "item with id " + id + " failed to saved" }
                        .ToString());
            }

            product = await _unitOfWork.ProductRepository.GetProductId(product.Id);

            var result = _mapper.Map<Product, SavedProductResource>(product);

            _logger.LogInformation(LoggingEvents.Updated, "item with id " + id + " updated");

            return Ok(result);
        }

        // DELETE
        //[Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductId(id);

            if (product == null)
                return NotFound(
                    new ErrorMessage { Code = 404, Message = "item with id " + id + " not existed" }.ToString());

            _unitOfWork.ProductRepository.Remove(product);
            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogError(LoggingEvents.Fail, "item with id " + id + " failed to saved");
                return StatusCode(500,
                    new ErrorMessage { Code = 500, Message = "item with id " + id + " failed to saved" }
                        .ToString());
            }

            _logger.LogInformation(LoggingEvents.Deleted, "item with id " + id + " is deleted");

            return NoContent();
        }
    }
}