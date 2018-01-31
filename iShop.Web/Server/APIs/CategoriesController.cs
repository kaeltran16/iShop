using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using iShop.Common.Extensions;
using iShop.Common.Helpers;
using iShop.Core.DTOs;
using iShop.Core.Entities;
using iShop.Infrastructure.Logging;
using iShop.Infrastructure.Persistent.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
namespace iShop.Web.Server.APIs
{
    /// <summary>
    /// This controller handles crud request for Cactegory
    /// </summary>
    [Route("/api/[controller]")]
    public class CategoriesController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<CategoriesController> _logger;

        public CategoriesController(IMapper mapper, IUnitOfWork unitOfWork, IAppLogger<CategoriesController> logger)
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

            var categoryResource = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(category);

            return Ok(categoryResource);
        }


        // GET
        [HttpGet("{id}", Name = ApplicationConstants.ControllerName.Category)]
        public async Task<IActionResult> Get(string id)
        {
            // Validate the input id, make sure it has the correct format
            bool isValid = Guid.TryParse(id, out var categoryId);

            if (!isValid)
                return InvalidId(id);

            var category = await _unitOfWork.CategoryRepository.GetCategory(categoryId);

            if (category == null)
                return NullOrEmpty();

            var categoryResource = _mapper.Map<Category, CategoryDto>(category);

            return Ok(categoryResource);
        }



        // POST
        [Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryDto categoryResources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _mapper.Map<CategoryDto, Category>(categoryResources);

            await _unitOfWork.CategoryRepository.AddAsync(category);

            // if something happens and the new item can not be saved, return the error
            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogInformation("");
                return FailedToSave(category.Id);
            }

            category = await _unitOfWork.CategoryRepository.GetCategory(category.Id);

            var result = _mapper.Map<Category, CategoryDto>(category);
            _logger.LogInformation("");
            //_logger.LogMessage(LoggingEvents.Created, ApplicationConstants.ControllerName.Category, category.Id);
            return CreatedAtRoute(ApplicationConstants.ControllerName.Category, new { id = category.Id }, result);
        }

        // DELETE
        [Authorize(Roles = ApplicationConstants.RoleName.SuperUser)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            bool isValid = Guid.TryParse(id, out var categoryId);

            if (!isValid)
                return InvalidId(id);

            var category = await _unitOfWork.CategoryRepository.GetCategory(categoryId);

            if (category == null)
                return NullOrEmpty();

            _unitOfWork.CategoryRepository.Remove(category);
            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogInformation("");
                //_logger.LogMessage(LoggingEvents.Fail, ApplicationConstants.ControllerName.Category, category.Id);
                return FailedToSave(category.Id);
            }

            //_logger.LogMessage(LoggingEvents.Deleted, ApplicationConstants.ControllerName.Category, category.Id);
            _logger.LogInformation("");
            return NoContent();
        }



        // PUT
        [Authorize(Policy = ApplicationConstants.PolicyName.SuperUsers)]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] CategoryDto categoryResource)
        {
            bool isValid = Guid.TryParse(id, out var categoryId);

            if (!isValid)
                return InvalidId(id);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _unitOfWork.CategoryRepository.GetCategory(categoryId);

            if (category == null)
                return NullOrEmpty();

            _mapper.Map(categoryResource, category);

            if (!await _unitOfWork.CompleteAsync())
            {
                //_logger.LogMessage(LoggingEvents.SavedFail, ApplicationConstants.ControllerName.Category, category.Id);
                _logger.LogInformation("");
                return FailedToSave(category.Id);
            }

            category = await _unitOfWork.CategoryRepository.GetCategory(category.Id);

            var result = _mapper.Map<Category, CategoryDto>(category);
            //_logger.LogMessage(LoggingEvents.Updated, ApplicationConstants.ControllerName.Category, category.Id);
            _logger.LogInformation("");
            return Ok(result);
        }

    }
}