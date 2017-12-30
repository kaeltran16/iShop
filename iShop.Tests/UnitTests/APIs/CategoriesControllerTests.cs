//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using AutoMapper;
//using FluentAssertions;
//using iShop.Web.Server.APIs;
//using iShop.Web.Server.Core.Models;
//using iShop.Web.Server.Core.Resources;
//using iShop.Web.Server.Extensions;
//using iShop.Web.Server.Persistent.Repositories.Contracts;
//using iShop.Web.Server.Persistent.UnitOfWork.Contracts;
//using Microsoft.AspNetCore.Mvc;
//using Moq;
//using Xunit;

//namespace iShop.Tests.UnitTests.APIs
//{
//    public class CategoriesControllerTests
//    {
//        private readonly CategoriesController _controller;
//        private readonly Mock<ICategoryRepository> _mockRepository;
//        private readonly string _userId;
//        private readonly Mock<IMapper> _mockMapper;

//        public CategoriesControllerTests()
//        {
//            _mockRepository = new Mock<ICategoryRepository>();
//            var mockUnitOfWork = new Mock<IUnitOfWork>();
//            mockUnitOfWork.SetupGet(u => u.CategoryRepository).Returns(_mockRepository.Object);

//            _mockMapper = new Mock<IMapper>();

//            _controller = new CategoriesController(_mockMapper.Object, mockUnitOfWork.Object);

//            _userId = "1";
//            _controller.MockUser(_userId, "test@domain.com");
//        }

//        [Fact]
//        public async void GetCategories_Existed_ShouldReturnOkResultWithListOfCategoryResource()
//        {
//            var id = Guid.NewGuid();
//            _mockRepository.Setup(g => g.GetCategories()).ReturnsAsync(new List<Category>() { new Category() { Id = id } });
//            _mockMapper.Setup(m => m.Map<IEnumerable<Category>, IEnumerable<CategoryResource>>(It.IsAny<IEnumerable<Category>>()))
//                .Returns(new List<CategoryResource>() { new CategoryResource() { Id = id } });

//            var result = await _controller.Get();

//            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

//            var categoryResource = okResult.Value.Should().BeAssignableTo<IEnumerable<CategoryResource>>().Subject;
//            categoryResource.ElementAt(0).Id.Should().Be(id);
//        }

//        [Fact]
//        public async void GetCategory_NotExisted_ShouldReturnNotFound()
//        {
//            var id = Guid.NewGuid();
//            _mockRepository.Setup(g => g.GetCategory(id)).ReturnsAsync((Category)null);

//            var result = await _controller.GetCategory(id);

//            result.Should().BeOfType<NotFoundResult>();
//        }

//        [Fact]
//        public async void GetCategory_Existed_ShouldReturnOKWithCategoryResource()
//        {
//            var categoryId = Guid.NewGuid();
            
//            _mockRepository.Setup(g => g.GetCategory(categoryId)).ReturnsAsync(new Category() {Id=categoryId});
//            _mockMapper.Setup(m => m.Map<Category, CategoryResource>(It.IsAny<Category>()))
//                .Returns(new CategoryResource(){Id =categoryId });
//             var result = await _controller.GetCategory(categoryId);

//            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
//            var categoryResource = okResult.Value.Should().BeAssignableTo<CategoryResource>().Subject;
//            categoryResource.Id.Should().Be(categoryId);
//        }
//    }
//}
