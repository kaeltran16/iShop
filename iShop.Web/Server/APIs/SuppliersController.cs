using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Web.Helpers;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;
using iShop.Web.Server.Extensions;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iShop.Web.Server.APIs
{
    [Route("/api/[controller]")]
    public class SuppliersController: BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SuppliersController> _logger;

        public SuppliersController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<SuppliersController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }


        // GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var suppliers = await _unitOfWork.SupplierRepository.GetSuppliers();

            var supplierResources = _mapper.Map<IEnumerable<Supplier>, IEnumerable<SupplierResource>>(suppliers);

            return Ok(supplierResources);
        }


        // GET
        [HttpGet("{id}", Name = GetName.Supplier)]
        public async Task<IActionResult> Get(string id)
        {
            bool isValid = Guid.TryParse(id, out var supplierId);

            if (!isValid)
                return InvalidId(id);

            var supplier = await _unitOfWork.SupplierRepository.GetSupplier(supplierId);

            if (supplier == null)
                return NotFound(ItemName.Supplier, supplierId);

            var supplierResource = _mapper.Map<Supplier, SupplierResource>(supplier);

            return Ok(supplierResource);
        }



        // POST
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SupplierResource supplierResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var supplier = _mapper.Map<SupplierResource, Supplier>(supplierResource);

            await _unitOfWork.SupplierRepository.AddAsync(supplier);

            // if something happens and the new item can not be saved, return the error
            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.SavedFail, ItemName.Supplier, supplier.Id);
                return FailedToSave(ItemName.Supplier, supplier.Id);
            }

            supplier = await _unitOfWork.SupplierRepository.GetSupplier(supplier.Id);

            var result = _mapper.Map<Supplier, SupplierResource>(supplier);

            _logger.LogMessage(LoggingEvents.Created, ItemName.Supplier, supplier.Id);
            return CreatedAtRoute(GetName.Supplier, new { id = supplier.Id }, result);
        }

        // DELETE
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool isValid = Guid.TryParse(id, out var supplierId);

            if (!isValid)
                return InvalidId(id);
            var supplier = await _unitOfWork.SupplierRepository.GetSupplier(supplierId);

            if (supplier == null)
                return NotFound(ItemName.Supplier, supplierId);

            _unitOfWork.SupplierRepository.Remove(supplier);
            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.Fail, ItemName.Supplier, supplier.Id);
                return FailedToSave(ItemName.Supplier, supplierId);
            }

            _logger.LogMessage(LoggingEvents.Deleted, ItemName.Supplier, supplier.Id);
            return NoContent();
        }



        // PUT
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] SupplierResource supplierResource)
        {
            bool isValid = Guid.TryParse(id, out var supplierId);

            if (!isValid)
                return InvalidId(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var supplier = await _unitOfWork.SupplierRepository.GetSupplier(supplierId);

            if (supplier == null)
                return NotFound(ItemName.Supplier, supplierId);

            _mapper.Map<SupplierResource, Supplier>(supplierResource, supplier);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.SavedFail, ItemName.Supplier, supplier.Id);
                return FailedToSave(ItemName.Supplier, supplier.Id);
            }

            supplier = await _unitOfWork.SupplierRepository.GetSupplier(supplier.Id);

            var result = _mapper.Map<Supplier, SupplierResource>(supplier);
            _logger.LogMessage(LoggingEvents.Updated, ItemName.Supplier, supplier.Id);
            return Ok(result);
        }

    }
}
