using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;
using iShop.Web.Server.Persistent.Repositories.Contracts;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.Server.APIs
{

    [Route("/api/Product")]
    public class ProductsController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IMapper mapper, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductResourceSave productResources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = _mapper.Map<ProductResourceSave, Product>(productResources);


             await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.CompleteAsync();

            product = await _unitOfWork.ProductRepository.GetProductId(product.Id);

            var result = _mapper.Map<Product, ProductResource>(product);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductResourceSave productResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await _unitOfWork.ProductRepository.GetProductId(id);

            if (product == null)
                return NotFound();

            _mapper.Map<ProductResourceSave, Product>(productResource, product);


            await _unitOfWork.CompleteAsync();

            product = await _unitOfWork.ProductRepository.GetProductId(product.Id);
            var result = _mapper.Map<Product, ProductResourceSave>(product);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductId(id);

            if (product == null)
                return NotFound();

            _unitOfWork.ProductRepository.Remove(product);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }

        // get product by ID :))
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductID(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductId(id);

            if (product == null)
                return NotFound();

            var productResource = _mapper.Map<Product, ProductResource>(product);

            return Ok(productResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteproduct(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductId(id, includeRelated: false);

            if (product == null)
                return NotFound();

            _unitOfWork.ProductRepository.Remove(product);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }



        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var products = await _unitOfWork.ProductRepository.GetProduct();

            if (products == null)
                return NotFound();

            var productResources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);


            return Ok(productResources);
        }




    }
}