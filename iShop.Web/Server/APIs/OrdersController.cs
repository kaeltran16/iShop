using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Web.Helpers;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;
using iShop.Web.Server.Extensions;
using iShop.Web.Server.Persistent.Repositories.Contracts;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.Server.APIs
{
    [Route("/api/[controller]")]
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
        [Authorize]
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserOrders(Guid userId)
        {
            if (userId != User.GetUserId())
                return Unauthorized();

            var order = await _unitOfWork.OrderRepository.GetUserOrders(userId);

            var orderResources =
                _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(order);

            return Ok(orderResources);
        }

        // GET
        [Authorize]
        [HttpGet("{id}", Name = GetName.Order)]
        public async Task<IActionResult> Get(Guid id)
        {
            var order = await _unitOfWork.OrderRepository.GetOrder(id);

            if (order == null)
                return NotFound(
                    new ErrorMessage { Code = 404, Message = "item with id " + id + " not existed" }.ToString());
            var userId = User.GetUserId();
            if (order.UserId != userId)
                return Unauthorized();

            var orderResource = _mapper.Map<Order, OrderResource>(order);

            return Ok(orderResource);
        }


        // POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SavedOrderResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<SavedOrderResource, Order>(resource);

            await _unitOfWork.OrderRepository.AddAsync(order);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogError(LoggingEvents.Fail,
                    "order " + resource.Id + " with userId " + resource.UserId + " failed to saved");

                return StatusCode(500,
                    new ErrorMessage
                    {
                        Code = 500,
                        Message = "order with id " + resource.Id +" by userId " + resource.UserId + " failed to saved"
                    }
                        .ToString());
            }

            order = await _unitOfWork.OrderRepository.GetOrder(order.Id, false);
            var result = (_mapper.Map<Order, OrderResource>(order));

            _logger.LogInformation(LoggingEvents.Created, "order by userId " + order.UserId + " is created");

            return CreatedAtRoute(GetName.Order,
                new { id = order.Id }, result);
        }

        // /api/Order/id update a order
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(Guid id, [FromBody]SavedOrderResource savedOrderResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _unitOfWork.OrderRepository.GetOrder(id);

            if (order == null)
                return NotFound(
                    new ErrorMessage { Code = 404, Message = "item with id " + id + " not existed" }.ToString());
            if (order.UserId != User.GetUserId())
                return Unauthorized();
            
            _mapper.Map<SavedOrderResource, Order>(savedOrderResource, order);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogError(LoggingEvents.Fail, "item with id " + id + " failed to saved");
                return StatusCode(500,
                    new ErrorMessage { Code = 500, Message = "item with id " + id + " failed to saved" }
                        .ToString());
            }

            order = await _unitOfWork.OrderRepository.GetOrder(order.Id);

            var result = _mapper.Map<Order, SavedOrderResource>(order);

            _logger.LogInformation(LoggingEvents.Updated, "item with id " + order.Id + " updated");

            return Ok(result);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var order = await _unitOfWork.OrderRepository.GetOrder(id);

            if (order == null)
                return NotFound(
                    new ErrorMessage { Code = 404, Message = "item with id " + id + " not existed" }.ToString());

            _unitOfWork.OrderRepository.Remove(order);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogError(LoggingEvents.Fail, "item with id " + id + " failed to saved");
                return StatusCode(500,
                    new ErrorMessage { Code = 500, Message = "item with id " + id + " failed to saved" }
                        .ToString());
            }

            _logger.LogInformation(LoggingEvents.Deleted, "item with id " + id + " is deleted");

            return Ok(id);
        }
    }
}