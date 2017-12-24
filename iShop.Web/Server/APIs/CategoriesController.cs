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

    [Route("/api/Category")]
    public class CategoriesController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public CategoriesController(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        // /api/Category . get all categories 
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var category = await _unitOfWork.CategoryRepository.GetCategories();
            if (category == null)
                return NotFound();
            var categoryResource = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(category);
            return Ok(categoryResource);

        }


        // /api/Category/id  get category of  value id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {

            var category = await _unitOfWork.CategoryRepository.GetCategory(id);
            if (category == null)
                return NotFound();
            var categoryResource = _mapper.Map<Category, CategoryResource>(category);
            return Ok(categoryResource);
        }



        // /api/Category    Use to create a Category and return this category 
        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryResource categoryResources)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = _mapper.Map<CategoryResource, Category>(categoryResources);


            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.CompleteAsync();

            category = await _unitOfWork.CategoryRepository.GetCategory(category.Id);

            var result = _mapper.Map<Category,CategoryResource>(category);

            return Ok(result);
        }



        // /api/Category Use  to delete a category with id of us and return this id 
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetCategory(id);

            if (category == null)
                return NotFound();

            _unitOfWork.CategoryRepository.Remove(category);
            await _unitOfWork.CompleteAsync();

            return Ok(id);
        }



        // /api/Category/id   Use to update a category 
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryResource categoryResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var category = await _unitOfWork.CategoryRepository.GetCategory(id);
            if (category == null)
                return NotFound();
            _mapper.Map<CategoryResource, Category>(categoryResource, category);
            await _unitOfWork.CompleteAsync();
            category = await _unitOfWork.CategoryRepository.GetCategory(category.Id);
            var result = _mapper.Map<Category, CategoryResource>(category);
            return Ok(result);
        }

    }
}