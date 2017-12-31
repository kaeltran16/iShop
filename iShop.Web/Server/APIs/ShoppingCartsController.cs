using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Web.Helpers;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.Server.APIs
{
    [Route("/api/[controller]")]
    public class ShoppingCartsController : Microsoft.AspNetCore.Mvc.Controller
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
        [HttpGet("{id}", Name = GetName.ShoppingCart)]
        public async Task<IActionResult> Get(Guid id)
        {
            var shoppingCart = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(id);

            if (shoppingCart == null)
                return NotFound(
                    new ErrorMessage { Code = 404, Message = "item with id " + id + " not existed" }.ToString());

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
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserShoppingCarts(Guid id)
        {
            var shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetUserShoppingCarts(id);

            var shoppingCartResource =
                _mapper.Map<IEnumerable<ShoppingCart>, IEnumerable<ShoppingCartResource>>(shoppingCarts);

            return Ok(shoppingCartResource);
        }

        // POST
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Create([FromBody] ShoppingCartResourceSave shoppingCartResources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // do not need to check for duplication in here
            var shoppingCart = _mapper.Map<ShoppingCartResourceSave, ShoppingCart>(shoppingCartResources);

            await _unitOfWork.ShoppingCartRepository.AddAsync(shoppingCart);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogError(LoggingEvents.Fail, "item with id " + shoppingCartResources.Id + " failed to saved");
                return StatusCode(500,
                    new ErrorMessage { Code = 500, Message = "item with id " + shoppingCartResources.Id + " failed to saved" }
                        .ToString());
            }

            shoppingCart = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(shoppingCart.Id);

            var result = (_mapper.Map<ShoppingCart, ShoppingCartResource>(shoppingCart));

            _logger.LogInformation(LoggingEvents.Created, "item with id " + shoppingCart.Id + " is created");

            return CreatedAtRoute(GetName.ShoppingCart, new { id = shoppingCart.Id }, result);
        }

        // PUT
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> Update(Guid id, [FromBody]ShoppingCartResourceSave shoppingCartResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(id);

            if (shoppingCarts == null)
                return NotFound(
                    new ErrorMessage { Code = 404, Message = "item with id " + id + " not existed" }.ToString());

            _mapper.Map<ShoppingCartResourceSave, ShoppingCart>(shoppingCartResource, shoppingCarts);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogError(LoggingEvents.Fail, "item with id " + id + " failed to saved");
                return StatusCode(500,
                    new ErrorMessage { Code = 500, Message = "item with id " + id + " failed to saved" }
                        .ToString());
            }

            shoppingCarts = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(shoppingCarts.Id);

            var result = _mapper.Map<ShoppingCart, ShoppingCartResourceSave>(shoppingCarts);

            _logger.LogInformation(LoggingEvents.Updated, "item with id " + id + " updated");

            return Ok(result);
        }


        // DELETE
        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var shoppingCart = await _unitOfWork.ShoppingCartRepository.GetShoppingCart(id, false);

            if (shoppingCart == null)
                return NotFound(
                    new ErrorMessage { Code = 404, Message = "item with id " + id + " not existed" }.ToString());

            _unitOfWork.ShoppingCartRepository.Remove(shoppingCart);
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