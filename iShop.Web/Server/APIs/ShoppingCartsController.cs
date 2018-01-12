using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Web.Server.Commons.Extensions;
using iShop.Web.Server.Commons.Helpers;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.Server.APIs
{
    [Route("/api/[controller]")]
    public class ShoppingCartsController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ShoppingCartsController> _logger;

        public ShoppingCartsController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<ShoppingCartsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // GET
        [HttpGet("{id}", Name = ItemName.ShoppingCart)]
        public async Task<IActionResult> Get(string id)
        {
            bool isValid = Guid.TryParse(id, out var shoppingCartId);
            if (!isValid)
                return InvalidId(id);

            var shoppingCart = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(shoppingCartId);

            if (shoppingCart == null)
                return NotFound(ItemName.ShoppingCart, shoppingCartId);

            var shoppingCartResource = _mapper.Map<ShoppingCart, ShoppingCartResource>(shoppingCart);

            return Ok(shoppingCartResource);
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetShoppingCarts();

            var shoppingCartResources =
                _mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartResource>>(shoppingCarts);

            return Ok(shoppingCartResources);
        }


        // GET
        [Authorize]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserShoppingCarts(Guid id)
        {
            var shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetUserShoppingCarts(id);

            var shoppingCartResource =
                _mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartResource>>(shoppingCarts);

            return Ok(shoppingCartResource);
        }

        // POST
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SavedShoppingCartResource shoppingCartResources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // do not need to check for duplication in here
            var shoppingCart = _mapper.Map<SavedShoppingCartResource, ShoppingCart>(shoppingCartResources);

            await _unitOfWork.ShoppingCartRepository.AddAsync(shoppingCart);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.SavedFail, ItemName.ShoppingCart, shoppingCart.Id);
                return FailedToSave(ItemName.ShoppingCart, shoppingCart.Id);
            }

            shoppingCart = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(shoppingCart.Id);

            var result = (_mapper.Map<ShoppingCart, ShoppingCartResource>(shoppingCart));

            _logger.LogMessage(LoggingEvents.Created, ItemName.Product, shoppingCart.Id);

            return CreatedAtRoute(ItemName.ShoppingCart, new { id = shoppingCart.Id }, result);
        }

        // PUT
        //[HttpPut("{id}")]
        //[Authorize]
        // public async Task<IActionResult> Update(Guid id, [FromBody]SavedShoppingCartResource shoppingCartResource)
        // {
        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     var shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(id);

        //     if (shoppingCarts == null)
        //         return NotFound(
        //             new ErrorMessage { Code = 404, Message = "item with id " + id + " not existed" }.ToString());

        //     _mapper.Map<SavedShoppingCartResource, ShoppingCart>(shoppingCartResource, shoppingCarts);

        //     if (!await _unitOfWork.CompleteAsync())
        //     {
        //         _logger.LogError(LoggingEvents.Fail, "item with id " + id + " failed to saved");
        //         return StatusCode(500,
        //             new ErrorMessage { Code = 500, Message = "item with id " + id + " failed to saved" }
        //                 .ToString());
        //     }

        //     shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(shoppingCarts.Id);

        //     var result = _mapper.Map<ShoppingCart, SavedShoppingCartResource>(shoppingCarts);

        //     _logger.LogInformation(LoggingEvents.Updated, "item with id " + id + " updated");

        //     return Ok(result);
        // }


        // DELETE
        [Authorize]
        [HttpDelete("{id}")]     
        public async Task<IActionResult> Delete(string id)
        {
            bool isValid = Guid.TryParse(id, out var shoppingCartId);
            if (!isValid)
                return InvalidId(id);

            var shoppingCart = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(shoppingCartId, false);

            if (shoppingCart == null)
                return NotFound(ItemName.ShoppingCart, shoppingCartId);

            _unitOfWork.ShoppingCartRepository.Remove(shoppingCart);
            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.SavedFail, ItemName.ShoppingCart, shoppingCart.Id);
                return FailedToSave(ItemName.ShoppingCart, shoppingCart.Id);
            }

            _logger.LogMessage(LoggingEvents.Deleted, ItemName.ShoppingCart, shoppingCart.Id);

            return NoContent();
        }
    }
}