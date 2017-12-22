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
        private readonly IMapper mapper;
        private readonly ICategoryRepository repository;


        public CategoriesController(IMapper mapper, ICategoryRepository repository)
        {
            this.repository = repository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var category = await repository.GetCategories();
            if (category == null)
                return NotFound();
            var categoryResource = mapper.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(category);
            return Ok(categoryResource);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {

            var category = await repository.GetCategory(id);
            if (category == null)
                return NotFound();
            var categoryResource = mapper.Map<Category, CategoryResource>(category);
            return Ok(categoryResource);
        }


    }
}