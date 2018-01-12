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
    public class CategoriesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(IMapper mapper, IUnitOfWork unitOfWork, ILogger<CategoriesController> logger)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
     

        // GET
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var category = await _unitOfWork.CategoryRepository.GetCategories();

            var categoryResource = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(category);

            return Ok(categoryResource);
        }


        // GET
        [HttpGet("{id}", Name = ItemName.Category)]
        public async Task<IActionResult> Get(string id)
        {
            bool isValid = Guid.TryParse(id, out var categoryId);

            if (!isValid)
                return InvalidId(id);

            var category = await _unitOfWork.CategoryRepository.GetCategory(categoryId);

            if (category == null)
                return NotFound(ItemName.Category, categoryId);

            var categoryResource = _mapper.Map<Category, CategoryResource>(category);

            return Ok(categoryResource);
        }



        // POST
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryResource categoryResources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _mapper.Map<CategoryResource, Category>(categoryResources);

            await _unitOfWork.CategoryRepository.AddAsync(category);

            // if something happens and the new item can not be saved, return the error
            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.SavedFail, ItemName.Category, category.Id);
                return FailedToSave(ItemName.Category, category.Id);
            }

            category = await _unitOfWork.CategoryRepository.GetCategory(category.Id);

            var result = _mapper.Map<Category, CategoryResource>(category);

            _logger.LogMessage(LoggingEvents.Created, ItemName.Category, category.Id);
            return CreatedAtRoute(ItemName.Category, new { id = category.Id }, result);
        }

        // DELETE
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool isValid = Guid.TryParse(id, out var categoryId);

            if (!isValid)
                return InvalidId(id);
            var category = await _unitOfWork.CategoryRepository.GetCategory(categoryId);

            if (category == null)
                return NotFound(ItemName.Category, categoryId);

            _unitOfWork.CategoryRepository.Remove(category);
            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.Fail, ItemName.Category, category.Id);
                return FailedToSave(ItemName.Category, categoryId);
            }

            _logger.LogMessage(LoggingEvents.Deleted, ItemName.Category, category.Id);
            return NoContent();
        }



        // PUT
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CategoryResource categoryResource)
        {
            bool isValid = Guid.TryParse(id, out var categoryId);

            if (!isValid)
                return InvalidId(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _unitOfWork.CategoryRepository.GetCategory(categoryId);

            if (category == null)
                return NotFound(ItemName.Category, categoryId);

            _mapper.Map<CategoryResource, Category>(categoryResource, category);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogMessage(LoggingEvents.SavedFail, ItemName.Category, category.Id);
                return FailedToSave(ItemName.Category, categoryId);
            }

            category = await _unitOfWork.CategoryRepository.GetCategory(category.Id);

            var result = _mapper.Map<Category, CategoryResource>(category);
            _logger.LogMessage(LoggingEvents.Updated, ItemName.Category, category.Id);
            return Ok(result);
        }

    }
}