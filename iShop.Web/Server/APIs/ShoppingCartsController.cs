using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace iShop.Web.Server.APIs
{
    [Route("/api/shoppingcart")]
    public class ShoppingCartsController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ShoppingCartsController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //create  shopping cart 
        [HttpPost]
        public async Task<IActionResult> CreateShoppingCart([FromBody] ShoppingCartResourceSave shoppingCartResources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var shoppingCart = _mapper.Map<ShoppingCartResourceSave, ShoppingCart>(shoppingCartResources);
            await _unitOfWork.ShoppingCartRepository.AddAsync(shoppingCart);
            await _unitOfWork.CompleteAsync();
            shoppingCart = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(shoppingCart.Id);
            var result = (_mapper.Map<ShoppingCart, ShoppingCartResource>(shoppingCart));
            return Ok(result);
        }

        //update a shopping cart
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppingCart(int id, [FromBody]ShoppingCartResourceSave ShoppingCartResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(id);
            if (shoppingCarts == null)
                return NotFound();
            _mapper.Map<ShoppingCartResourceSave, ShoppingCart>(ShoppingCartResource, shoppingCarts);
            await _unitOfWork.CompleteAsync();
            shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(shoppingCarts.Id);
            var result = _mapper.Map<ShoppingCart, ShoppingCartResourceSave>(shoppingCarts);
            return Ok(result);
        }


       
        [HttpGet("{id}")]
        public async Task<IActionResult> GetShoppingCart(int id)
        {
            var shoppingCart = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(id);
            if (shoppingCart == null)
                return NotFound();
            var shoppingCartResource = _mapper.Map<ShoppingCart, ShoppingCartResource>(shoppingCart);
            return Ok(shoppingCartResource);
        }

        //delete a shopping cart with id 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCart(int id)
        {
            // just false is enough =))
            var shoppingCart = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(id, /*includeRelated: */false);
            if (shoppingCart == null)
                return NotFound();
            _unitOfWork.ShoppingCartRepository.Remove(shoppingCart);
            await _unitOfWork.CompleteAsync();
            return Ok(id);
        }


        //get  all shopping cart in this web
        [HttpGet]
        public async Task<IActionResult> GetShoppingCarts()
        {
            var shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetShoppingCarts();
            if (shoppingCarts == null)
                return NotFound();
            var shoppingCartResources =
                _mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartResource>>(shoppingCarts);
            return Ok(shoppingCartResources);
        }


        // get shopping cart   of current user 
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetShoppingCartOfUser(string userId)
        {
            var shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetShoppingCartOfUser(userId);
            if (shoppingCarts == null)
                return NotFound();
            var shoppingCartResource =
                _mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartResource>>(shoppingCarts);
            return Ok(shoppingCartResource);
        }
    }
}