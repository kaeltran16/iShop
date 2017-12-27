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
    [Route("/api/Order")]
    public class OrdersController : Microsoft.AspNetCore.Mvc.Controller
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrdersController(IMapper mapper, IOrderRepository repository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //create  a Order 
        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderResourceSave orderResources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var order = _mapper.Map<OrderResourceSave, Order>(orderResources);
            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.CompleteAsync();
            order = await _unitOfWork.OrderRepository.GetOrder(order.ShoppingCartId);
            var result = (_mapper.Map<Order, OrderResource>(order));
            return Ok(result);
        }

        //update a order
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int shoppingCartId, [FromBody]OrderResourceSave orderResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var order = await _unitOfWork.OrderRepository.GetOrder(shoppingCartId);
            if (order == null)
                return NotFound();
            _mapper.Map<OrderResourceSave, Order>(orderResource, order);
            await _unitOfWork.CompleteAsync();
            order = await _unitOfWork.OrderRepository.GetOrder(order.ShoppingCartId);
            var result = _mapper.Map<Order, OrderResourceSave>(order);
            return Ok(result);
        }


        // get order  with shopping cart id 
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int shoppingCartId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrder(shoppingCartId);
            if (order == null)
                return NotFound();
            var orderResource = _mapper.Map<Order, OrderResource>(order);
            return Ok(orderResource);
        }

        //delete a order with id 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int shoppingCartId)
        {
          
            var order = await _unitOfWork.OrderRepository.GetOrder(shoppingCartId, false);
            if (order == null)
                return NotFound();
            _unitOfWork.OrderRepository.Remove(order);
            await _unitOfWork.CompleteAsync();
            return Ok(shoppingCartId);
        }


        //get  all Order 
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _unitOfWork.OrderRepository.GetOrders();
            if (orders == null)
                return NotFound();
            var orderResources =
                _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(orders);
            return Ok(orderResources);
        }


        // get order   of current user 
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetOrderOfUser(string userId)
        {
            var order = await _unitOfWork.OrderRepository.GetOrderOfUser(userId);
            if (order == null)
                return NotFound();
            var orderResources =
                _mapper.Map<IEnumerable<Order>, IEnumerable<OrderResource>>(order);
            return Ok(orderResources);
        }



    }
}