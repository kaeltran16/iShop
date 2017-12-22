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

        private readonly IMapper mapper;
        private readonly IProductRepository repository;
        private readonly IUnitOfWork unitOfWork;

        public ProductsController(IMapper mapper, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductResourceSave productResources)
        {


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = mapper.Map<ProductResourceSave, Product>(productResources);


            repository.Add(product);
            await unitOfWork.CompleteAsync();

            product = await repository.GetProductId(product.Id);

            var result = mapper.Map<Product, ProductResource>(product);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductResourceSave productResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var product = await repository.GetProductId(id);

            if (product == null)
                return NotFound();

            mapper.Map<ProductResourceSave, Product>(productResource, product);


            await unitOfWork.CompleteAsync();

            product = await repository.GetProductId(product.Id);
            var result = mapper.Map<Product, ProductResourceSave>(product);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var product = await repository.GetProductId(id);

            if (product == null)
                return NotFound();

            repository.Remove(product);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }

        // get product by ID :))
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductID(int id)
        {
            var product = await repository.GetProductId(id);

            if (product == null)
                return NotFound();

            var productResource = mapper.Map<Product, ProductResource>(product);

            return Ok(productResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteproduct(int id)
        {
            var product = await repository.GetProductId(id, includeRelated: false);

            if (product == null)
                return NotFound();

            repository.Remove(product);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }



        [HttpGet]
        public async Task<IActionResult> GetProduct()
        {
            var products = await repository.GetProduct();

            if (products == null)
                return NotFound();

            var productResources = mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);


            return Ok(productResources);
        }




    }
}