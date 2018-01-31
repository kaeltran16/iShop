using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.Extensions;
using iShop.Common.Helpers;
using iShop.Core.DTOs;
using iShop.Core.Entities;
using iShop.Infrastructure.Persistent.Repositories.Contracts;
using iShop.Infrastructure.Persistent.UnitOfWork.Contracts;
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
        [Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var orders = await _unitOfWork.OrderRepository.GetOrders();

            var orderResources =
                _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(orders);

            return Ok(orderResources);
        }

        // GET
        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetUserOrders(string id)
        {
            bool isValid = Guid.TryParse(id, out var userId);

            if (!isValid)
                return InvalidId(id);

            if (userId != User.GetUserId())
                return UnAuthorized();

            var order = await _unitOfWork.OrderRepository.GetUserOrders(userId);

            var orderResources =
                _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(order);

            return Ok(orderResources);
        }

        // GET
        [HttpGet("{id}", Name = ApplicationConstants.ControllerName.Order)]
        public async Task<IActionResult> Get(string id)
        {
            bool isValid = Guid.TryParse(id, out var orderId);
            if (!isValid)
                return InvalidId(id);

            var order = await _unitOfWork.OrderRepository.GetOrder(orderId);

            if (order == null)
                return NotFound(orderId);

            if (order.UserId != User.GetUserId())
                return UnAuthorized();

            var orderResource = _mapper.Map<Order, OrderDto>(order);

            return Ok(orderResource);
        }


        // POST
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SavedOrderDto resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = _mapper.Map<SavedOrderDto, Order>(resource);

            await _unitOfWork.OrderRepository.AddAsync(order);

            if (!await _unitOfWork.CompleteAsync())
            {
                //_logger.LogMessage(LoggingEvents.SavedFail, ApplicationConstants.ControllerName.Order, order.Id);
                _logger.LogInformation("");
                return FailedToSave(order.Id);
            }

            order = await _unitOfWork.OrderRepository.GetOrder(order.Id, false);
            var result = (_mapper.Map<Order, OrderDto>(order));

            //_logger.LogMessage(LoggingEvents.Created, ApplicationConstants.ControllerName.Order, order.Id);
            _logger.LogInformation("");
            return CreatedAtRoute(ApplicationConstants.ControllerName.Order,
                new { id = order.Id }, result);
        }

        //PUT
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody]SavedOrderDto savedOrderResource)
        {
            bool isValid = Guid.TryParse(id, out var orderId);
            if (!isValid)
                return InvalidId(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var order = await _unitOfWork.OrderRepository.GetOrder(orderId);

            if (order == null)
                return NotFound(orderId);

            if (order.UserId != User.GetUserId())
                return UnAuthorized();

            _mapper.Map(savedOrderResource, order);

            if (!await _unitOfWork.CompleteAsync())
            {
                //_logger.LogMessage(LoggingEvents.SavedFail, ApplicationConstants.ControllerName.Order, order.Id);
                _logger.LogInformation("");
                return FailedToSave(order.Id);
            }

            order = await _unitOfWork.OrderRepository.GetOrder(order.Id);

            var result = _mapper.Map<Order, SavedOrderDto>(order);

            //_logger.LogMessage(LoggingEvents.Updated, ApplicationConstants.ControllerName.Order, order.Id);
            _logger.LogInformation("");
            return Ok(result);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(string id)
        {
            bool isValid = Guid.TryParse(id, out var orderId);
            if (!isValid)
                return InvalidId(id);

            var order = await _unitOfWork.OrderRepository.GetOrder(orderId);

            if (order == null)
                return NotFound(orderId);

            if (order.UserId != User.GetUserId())
                return NotFound(orderId);

            _unitOfWork.OrderRepository.Remove(order);

            if (!await _unitOfWork.CompleteAsync())
            {
                //_logger.LogMessage(LoggingEvents.SavedFail, ApplicationConstants.ControllerName.Order, order.Id);
                _logger.LogInformation("");
                return FailedToSave(order.Id);
            }

            //_logger.LogMessage(LoggingEvents.Deleted, ApplicationConstants.ControllerName.Order, order.Id);
            _logger.LogInformation("");
            return NoContent();
        }
    }
}