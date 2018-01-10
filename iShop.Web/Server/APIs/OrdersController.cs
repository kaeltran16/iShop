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
    public class OrdersController : BaseController
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
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserOrders(string id)
        {
            Guid userId;
            try
            {
                userId = Guid.Parse(id);
            }
            catch (Exception e)
            {
                return InvalidId(id);
            }

            if (userId != User.GetUserId())
                return UnAuthorized();

            var order = await _unitOfWork.OrderRepository.GetUserOrders(userId);

            var orderResources =
                _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(order);

            return Ok(orderResources);
        }

        // GET
        [Authorize]
        [HttpGet("{id}", Name = GetName.Order)]
        public async Task<IActionResult> Get(string id)
        {
            Guid orderId;
            try
            {
                orderId = Guid.Parse(id);
            }
            catch (Exception)
            {
                return InvalidId(id);
            }
            var order = await _unitOfWork.OrderRepository.GetOrder(orderId);

            if (order == null)
                return NotFound(ItemName.Order, orderId);

            var userId = User.GetUserId();

            if (order.UserId != userId)
                return UnAuthorized();

            var orderResource = _mapper.Map<Order, OrderResource>(order);

            return Ok(orderResource);
        }


        // POST
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SavedOrderResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<SavedOrderResource, Order>(resource);

            await _unitOfWork.OrderRepository.AddAsync(order);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.SavedFail, ItemName.Order, order.Id);

                return FailedToSave(ItemName.Order, order.Id);
            }

            order = await _unitOfWork.OrderRepository.GetOrder(order.Id, false);
            var result = (_mapper.Map<Order, OrderResource>(order));

            _logger.LogMessage(LoggingEvents.Created, ItemName.Order, order.Id);

            return CreatedAtRoute(GetName.Order,
                new { id = order.Id }, result);
        }

       // PUT
       //[HttpPut("{id}")]
       // public async Task<IActionResult> Update(string id, [FromBody]SavedOrderResource savedOrderResource)
       // {
       //     Guid orderId;
       //     try
       //     {
       //         Guid.TryParse(id, out orderId);
       //     }
       //     catch (Exception)
       //     {
       //         return InvalidId(id);
       //     }

       //     if (!ModelState.IsValid)
       //         return BadRequest(ModelState);

       //     var order = await _unitOfWork.OrderRepository.GetOrder(orderId);

       //     if (order == null)
       //         return NotFound(ItemName.Order, orderId);

       //     if (order.UserId != User.GetUserId())
       //         return UnAuthorized();

       //     _mapper.Map<SavedOrderResource, Order>(savedOrderResource, order);

       //     if (!await _unitOfWork.CompleteAsync())
       //     {
       //         _logger.LogMessage(LoggingEvents.SavedFail, ItemName.Order, order.Id);

       //         return FailedToSave(ItemName.Order, order.Id);
       //     }

       //     order = await _unitOfWork.OrderRepository.GetOrder(order.Id);

       //     var result = _mapper.Map<Order, SavedOrderResource>(order);

       //     _logger.LogMessage(LoggingEvents.Updated, ItemName.Order, order.Id);

       //     return Ok(result);
       // }

        // DELETE
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            Guid orderId;
            try
            {
                orderId = Guid.Parse(id);
            }
            catch (Exception)
            {
                return InvalidId(id);
            }
            var order = await _unitOfWork.OrderRepository.GetOrder(orderId);

            if (order == null)
                return NotFound(ItemName.Order, orderId);

            _unitOfWork.OrderRepository.Remove(order);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.SavedFail, ItemName.Order, order.Id);

                return FailedToSave(ItemName.Order, order.Id);
            }

            _logger.LogMessage(LoggingEvents.Deleted, ItemName.Order, order.Id);

            return NoContent();
        }
    }
}