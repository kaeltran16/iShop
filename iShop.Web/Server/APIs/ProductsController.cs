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
        // /api/Product    Use to create a product and return this product 
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
        // /api/Product/id   Use to update a product 
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
        // /api/Product Use  to delete a product with id of us and return this id 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductId(id);

            if (product == null)
                return NotFound();

            _unitOfWork.ProductRepository.Remove(product);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }

        // /api/Product/id get product by ID :))
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetProductId(id);

            if (product == null)
                return NotFound();

            var productResource = _mapper.Map<Product, ProductResource>(product);

            return Ok(productResource);
        }

       

      //  /api/Product  get all product  
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _unitOfWork.ProductRepository.GetProduct();

            if (products == null)
                return NotFound();

            var productResources = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);


            return Ok(productResources);
        }




    }
}