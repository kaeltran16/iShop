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
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var category = await _unitOfWork.CategoryRepository.GetCategories();
            if (category == null)
                return NotFound();
            var categoryResource = _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(category);
            return Ok(categoryResource);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {

            var category = await _unitOfWork.CategoryRepository.GetCategory(id);
            if (category == null)
                return NotFound();
            var categoryResource = _mapper.Map<Category, CategoryResource>(category);
            return Ok(categoryResource);
        }


    }
}