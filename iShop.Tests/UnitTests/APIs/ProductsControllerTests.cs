using System;
using System.Collections.Generic;
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
    public class ProductsControllerTests
    {
        private readonly ProductsController _controller;
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly string _userId;

        public ProductsControllerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.ProductRepository).Returns(_mockRepository.Object);

            var mockMapper = new Mock<IMapper>();

            _controller = new ProductsController(mockMapper.Object, mockUnitOfWork.Object);

            _userId = "1";
            _controller.MockUser(_userId, "test@domain.com");
        }

        [Fact]
        public async void CreateProduct_ModelStateInvalid_ShouldReturnBadRequest()
        {
            var newProduct = new ProductResourceSave()
            {
               Id = 1
            };
            _controller.ModelState.AddModelError("SessionName", "error");
            var result = await _controller.CreateProduct(newProduct);
            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void CreateProduct_ValidProduct_ShouldReturnOkObjectResult()
        {
            var newProduct = new ProductResourceSave()
            {
                Id = 1
            };
      
            var result = await _controller.CreateProduct(newProduct);

            var okResult = result.Should().BeOfType<CreatedAtActionResult>().Subject;
            okResult.Should().BeOfType<OkResult>();
        }

    }
}
