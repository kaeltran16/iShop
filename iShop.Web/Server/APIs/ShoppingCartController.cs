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
    [Route("/api/shoppingcart")]
    public class ShoppingCartController : Controller
    {
        private readonly IMapper mapper;
        private readonly IShoppingCartRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public ShoppingCartController(IMapper mapper, IShoppingCartRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.repository = repository;
            this.mapper = mapper;
        }

//        //create  shopping cart 
//        [HttpPost]
//        public async Task<IActionResult> CreateShoppingCart([FromBody] ShoppingCartResourceSave ShoppingCartResources)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);
//            var ShoppingCart = mapper.Map<ShoppingCartResourceSave, ShoppingCart>(ShoppingCartResources);
//            repository.Add(ShoppingCart);
//            await unitOfWork.CompleteAsync();
//            ShoppingCart = await repository.GetShoppingCart(ShoppingCart.Id);
//            var result = (mapper.Map<ShoppingCart, ShoppingCartResource>(ShoppingCart));
//            return Ok(result);
//        }
//
//        //update a shopping cart
//        [HttpPut("{id}")]
//        public async Task<IActionResult> UpdateShoppingCart(int id, [FromBody]ShoppingCartResourceSave ShoppingCartResource)
//        {
//            if (!ModelState.IsValid)
//                return BadRequest(ModelState);
//            var ShoppingCart = await repository.GetShoppingCart(id);
//            if (ShoppingCart == null)
//                return NotFound();
//            mapper.Map<ShoppingCartResourceSave, ShoppingCart>(ShoppingCartResource, ShoppingCart);
//            await unitOfWork.CompleteAsync();
//            ShoppingCart = await repository.GetShoppingCart(ShoppingCart.Id);
//            var result = mapper.Map<ShoppingCart, ShoppingCartResourceSave>(ShoppingCart);
//            return Ok(result);
//        }
//
//
//        // Get ShoppingCart by ID :))
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetShoppingCartID(int id)
//        {
//            var ShoppingCart = await repository.GetShoppingCart(id);
//            if (ShoppingCart == null)
//                return NotFound();
//            var ShoppingCartResource = mapper.Map<ShoppingCart, ShoppingCartResource>(ShoppingCart);
//            return Ok(ShoppingCartResource);
//        }
//
//        //delete a shopping cart with id 
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteShoppingCart(int id)
//        {
//            var ShoppingCart = await repository.GetShoppingCart(id, includeRelated: false);
//            if (ShoppingCart == null)
//                return NotFound();
//            repository.Remove(ShoppingCart);
//            await unitOfWork.CompleteAsync();
//            return Ok(id);
//        }
//
//
//        //get  all shopping cart in this web
//        [HttpGet]
//        public async Task<IActionResult> GetShoppingCart()
//        {
//            var ShoppingCarts = await repository.GetShoppingCarts();
//            if (ShoppingCarts == null)
//                return NotFound();
//            var ShoppingCartResources = mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartResource>>(ShoppingCarts);
//            return Ok(ShoppingCartResources);
//        }
//
//
//        // get shopping cart   of current user 
//        [HttpGet("{userId}")]
//        public async Task<IActionResult> GetShoppingCartOfUser(string userId)
//        {
//            var ShoppingCarts = await repository.GetShoppingCartOfUser(userId);
//            if (ShoppingCarts == null)
//                return NotFound();
//            var ShoppingCartResource = mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartResource>>(ShoppingCarts);
//            return Ok(ShoppingCartResource);
//        }
//




    }
}