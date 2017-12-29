using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using FluentAssertions;
using iShop.Web.Server.APIs;
using iShop.Web.Server.Core.Models;
using iShop.Web.Server.Core.Resources;
using iShop.Web.Server.Extensions;
using iShop.Web.Server.Persistent.Repositories.Contracts;
using iShop.Web.Server.Persistent.UnitOfWork.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace iShop.Tests.UnitTests.APIs
{
    public class CategoriesControllerTests
    {
        private readonly CategoriesController _controller;
        private readonly Mock<ICategoryRepository> _mockRepository;
        private readonly string _userId;
        private readonly Mock<IMapper> _mockMapper;

        public CategoriesControllerTests()
        {
            _mockRepository = new Mock<ICategoryRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.CategoryRepository).Returns(_mockRepository.Object);

            _mockMapper = new Mock<IMapper>();

            _controller = new CategoriesController(_mockMapper.Object, mockUnitOfWork.Object);

            _userId = "1";
            _controller.MockUser(_userId, "test@domain.com");
        }

        [Fact]
        public async void GetCategories_Existed_ShouldReturnOkResultWithListOfCategoryResource()
        {
            _mockRepository.Setup(g => g.GetCategories()).ReturnsAsync(new List<Category>() { new Category() { Id = 1 } });
            _mockMapper.Setup(m => m.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(It.IsAny<IEnumerable<Category>>()))
                .Returns(new List<CategoryResource>() { new CategoryResource() { Id = 1 } });

            var result = await _controller.GetCategories();

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            var categoryResource = okResult.Value.Should().BeAssignableTo<IEnumerable<CategoryResource>>().Subject;
            categoryResource.ElementAt(0).Id.Should().Be(1);
        }

        [Fact]
        public async void GetCategory_NotExisted_ShouldReturnNotFound()
        {
            var categoryId = 1;
            _mockRepository.Setup(g => g.GetCategory(categoryId)).ReturnsAsync((Category)null);

            var result = await _controller.GetCategory(categoryId);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void GetCategory_Existed_ShouldReturnOKWithCategoryResource()
        {
            var categoryId = 1;
            _mockRepository.Setup(g => g.GetCategory(categoryId)).ReturnsAsync(new Category() {Id=1});
            _mockMapper.Setup(m => m.Map<Category, CategoryResource>(It.IsAny<Category>()))
                .Returns(new CategoryResource(){Id =1 });
             var result = await _controller.GetCategory(categoryId);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var categoryResource = okResult.Value.Should().BeAssignableTo<CategoryResource>().Subject;
            categoryResource.Id.Should().Be(1);
        }
    }
}
