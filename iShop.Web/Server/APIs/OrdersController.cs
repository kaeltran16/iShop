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
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.Server.APIs
{
    [Route("/api/[controller]/user/")]
    public class OrdersController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<OrdersController> _logger;

        public OrdersController(IMapper mapper, IOrderRepository repository, IUnitOfWork unitOfWork,
            ILogger<OrdersController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        // GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _unitOfWork.OrderRepository.GetOrders();

            var orderResources =
                _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);

            return Ok(orderResources);
        }

        // GET
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserOrders(Guid userId)
        {
            var order = await _unitOfWork.OrderRepository.GetUserOrders(userId);

            var orderResources =
                _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(order);

            return Ok(orderResources);
        }

        // GET
        [HttpGet("{userId}/{id}", Name = GetName.Order)]
        public async Task<IActionResult> Get(Guid userId, Guid id)
        {
            var order = await _unitOfWork.OrderRepository.GetOrder(userId, id);

            if (order == null)
                return NotFound(
                    new ErrorMessage { Code = 404, Message = "item with id " + id + " not existed" }.ToString());

            var orderResource = _mapper.Map<Order, OrderResource>(order);

            return Ok(orderResource);
        }


        // POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SavedOrderResource savedOrderResources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<SavedOrderResource, Order>(savedOrderResources);

            await _unitOfWork.OrderRepository.AddAsync(order);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogError(LoggingEvents.Fail,
                    "order with userId " + savedOrderResources.UserId + ", shoppingCartId " +
                    savedOrderResources.ShoppingCartId + " failed to saved");

                return StatusCode(500,
                    new ErrorMessage
                    {
                        Code = 500,
                        Message = "order by userId " + savedOrderResources.UserId + " failed to saved"
                    }
                        .ToString());
            }

            order = await _unitOfWork.OrderRepository.GetOrder(order.UserId, order.ShoppingCartId);
            var result = (_mapper.Map<Order, OrderResource>(order));

            _logger.LogInformation(LoggingEvents.Created, "order by userId " + order.UserId + " is created");

            return CreatedAtRoute(GetName.Order,
                new { userId = savedOrderResources.UserId, id = savedOrderResources.ShoppingCartId }, result);
        }

        // /api/Order/id update a order
        [HttpPut("{userId}/{id}")]
        public async Task<IActionResult> UpdateOrder(Guid userId, Guid shoppingCartId, [FromBody]SavedOrderResource savedOrderResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _unitOfWork.OrderRepository.GetOrder(userId, shoppingCartId);

            if (order == null)
                return NotFound(
                    new ErrorMessage { Code = 404, Message = "item with id " + shoppingCartId + " not existed" }.ToString());

            _mapper.Map<SavedOrderResource, Order>(savedOrderResource, order);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogError(LoggingEvents.Fail, "item with id " + shoppingCartId + " failed to saved");
                return StatusCode(500,
                    new ErrorMessage { Code = 500, Message = "item with id " + shoppingCartId + " failed to saved" }
                        .ToString());
            }

            order = await _unitOfWork.OrderRepository.GetOrder(order.UserId, order.ShoppingCartId);

            var result = _mapper.Map<Order, SavedOrderResource>(order);

            _logger.LogInformation(LoggingEvents.Updated, "item with id " + shoppingCartId + " updated");

            return Ok(result);
        }

        // DELETE
        [HttpDelete("{userId}/{id}")]
        public async Task<IActionResult> DeleteOrder(Guid userId, Guid shoppingCartId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrder(userId, shoppingCartId, false);

            if (order == null)
                return NotFound(
                    new ErrorMessage { Code = 404, Message = "item with id " + shoppingCartId + " not existed" }.ToString());

            _unitOfWork.OrderRepository.Remove(order);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogError(LoggingEvents.Fail, "item with id " + shoppingCartId + " failed to saved");
                return StatusCode(500,
                    new ErrorMessage { Code = 500, Message = "item with id " + shoppingCartId + " failed to saved" }
                        .ToString());
            }

            _logger.LogInformation(LoggingEvents.Deleted, "item with id " + shoppingCartId + " is deleted");

            return Ok(shoppingCartId);
        }
    }
}