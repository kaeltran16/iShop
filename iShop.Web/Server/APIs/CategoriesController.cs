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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.Extensions.Logging;

namespace iShop.Web.Server.APIs
{

    [Route("/api/[controller]")]
    public class CategoriesController : Microsoft.AspNetCore.Mvc.Controller
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
        [HttpGet("{id}", Name = nameof(Get))]
        
        public async Task<IActionResult> Get(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategory(id);

            if (category == null)
                return NotFound(
                    new ErrorMessage {Code = 404, Message = "item with id " + id + " not existed"}.ToString());

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

            // go through all categories, make sure we do not create 2 categories with the same name 
            var categories = await _unitOfWork.CategoryRepository.GetCategories();
            foreach (var cate in categories)
            {
                // if we do, return bad request
                if (cate.Name.Equals(categoryResources.Name))
                    return BadRequest(new ErrorMessage()
                    {
                        Code = 400,
                        Message = "category with name " + categoryResources.Name + " exists"
                    }.ToString());
            }

            // everything is ok, create a new one
            var category = _mapper.Map<CategoryResource, Category>(categoryResources);

            await _unitOfWork.CategoryRepository.AddAsync(category);
            
            // if something happens and the new item can not be saved, return the error
            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogError(LoggingEvents.Fail, "item with id " + category.Id + " failed to saved");
                return StatusCode(500,
                    new ErrorMessage {Code = 500, Message = "item with id " + category.Id + " failed to saved"}
                        .ToString());
            }

            category = await _unitOfWork.CategoryRepository.GetCategory(category.Id);

            var result = _mapper.Map<Category,CategoryResource>(category);

            _logger.LogInformation(LoggingEvents.Created, "item with id " + category.Id + " is created");
            return CreatedAtRoute(nameof(Get), new {id = category.Id}, result);
        }



        // DELETE
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(Guid id)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategory(id);

            if (category == null)
                return NotFound(
                    new ErrorMessage { Code = 404, Message = "item with id " + id + " not existed" }.ToString());

            _unitOfWork.CategoryRepository.Remove(category);
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



        // POST
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryResource categoryResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _unitOfWork.CategoryRepository.GetCategory(id);

            if (category == null)
                return NotFound(
                    new ErrorMessage { Code = 404, Message = "item with id " + id + " not existed" }.ToString());

            _mapper.Map<CategoryResource, Category>(categoryResource, category);

            if (!await _unitOfWork.CompleteAsync())
            {
                _logger.LogError(LoggingEvents.Fail, "item with id " + id + " failed to saved");
                return StatusCode(500,
                    new ErrorMessage { Code = 500, Message = "item with id " + id + " failed to saved" }
                        .ToString());
            }

            category = await _unitOfWork.CategoryRepository.GetCategory(category.Id);

            var result = _mapper.Map<Category, CategoryResource>(category);
            return Ok(result);
        }

    }
}