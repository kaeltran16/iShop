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
    public class ProductsControllerTests
    {
        private readonly ProductsController _controller;
        private readonly Mock<IProductRepository> _mockRepository;
        private readonly string _userId;
        private readonly Mock<IMapper> _mockMapper;
        public ProductsControllerTests()
        {
            _mockRepository = new Mock<IProductRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.ProductRepository).Returns(_mockRepository.Object);

            _mockMapper = new Mock<IMapper>();

            _controller = new ProductsController(_mockMapper.Object, mockUnitOfWork.Object);

            _userId = "1";
            _controller.MockUser(_userId, "test@domain.com");
        }
        /// <summary>
        /// NOTE: BE AWARE!!!! 
        /// For CREATE, UPDATE and DELETE test methods below, i just test its basic behavior, not its actual behavior
        /// so by passing those tests, it does not mean that those API end points will work correctly
        /// </summary>

        [Fact]
        public async void CreateProduct_ModelStateInvalid_ShouldReturnBadRequest()
        {
            var newProduct = new ProductResourceSave() { Id = 1 };

            _controller.ModelState.AddModelError("Error", "error");

            var result = await _controller.CreateProduct(newProduct);

            result.Should().BeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async void CreateProduct_ValidProduct_ShouldReturnOkObjectResult()
        {
            var newProduct = new ProductResourceSave() { Id = 1 };

            _mockMapper.Setup(m => m.Map<ProductResourceSave, Product>(It.IsAny<ProductResourceSave>()))
                .Returns(new Product() { Id = 1 });

            var result = await _controller.CreateProduct(newProduct);

            // NOTE: I did want the test the actual result of the result is whether the Product with Id of 1 is because
            // in the Create API method, it returns the Ok result with the object is the Product got from the database
            // if i test it, it makes no sense because I just only test the function that get the product from the API.
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void UpdateProduct_ModelStateInvalid_ShouldReturnBadRequest()
        {
            var newProduct = new ProductResourceSave() {Id = 1};

            _controller.ModelState.AddModelError("Error", "error");

            var result = await _controller.CreateProduct(newProduct);

            result.Should().BeOfType<BadRequestObjectResult>();
        }


        [Fact]
        public async void UpdateProduct_NotExisted_ShouldReturnNotFoundResult()
        {
            var newProduct = new ProductResourceSave() {Id = 1};
            _mockRepository.Setup(g => g.GetProductId(1, true)).ReturnsAsync((Product)null);


            var result = await _controller.UpdateProduct(1, newProduct);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void UpdateProduct_ValidProduct_ShouldReturnOkObjectResult()
        {
            var newProductResource = new ProductResourceSave() { Id = 1 };
            var newProduct = new Product() {Id = 1};
            _mockRepository.Setup(g => g.GetProductId(1,true)).ReturnsAsync(newProduct);
            _mockMapper.Setup(m => m.Map<ProductResourceSave, Product>(It.IsAny<ProductResourceSave>()))
                .Returns(newProduct);

            var result = await _controller.UpdateProduct(1, newProductResource);
            
            
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async void DeleteProduct_NotExisted_ShouldReturnNotFoundResult()
        {

            var result = await _controller.DeleteProduct(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void DeleteProduct_ValidProduct_ShouldReturnOkObjectResult()
        {
            var newProductResource = new ProductResource() { Id = 1 };
            var newProduct = new Product() { Id = 1 };
            _mockRepository.Setup(g => g.GetProductId(1, true)).ReturnsAsync(newProduct);
            _mockMapper.Setup(m => m.Map<Product, ProductResource>(It.IsAny<Product>()))
                .Returns(newProductResource);

            var result = await _controller.DeleteProduct(1);


            result.Should().BeOfType<OkObjectResult>();
        }


        [Fact]
        public async void GetProduct_NotExisted_ShouldReturnNotFoundResult()
        {

            var result = await _controller.GetProduct(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async void GetProduct_ValidProduct_ShouldReturnOkObjectResult()
        {
            var newProductResource = new ProductResource() { Id = 1 };
            var newProduct = new Product() { Id = 1 };
            _mockRepository.Setup(g => g.GetProductId(1, true)).ReturnsAsync(newProduct);

            _mockMapper.Setup(m => m.Map<Product, ProductResource>(It.IsAny<Product>()))
                .Returns(newProductResource);

            var result = await _controller.GetProduct(1);


            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var productResult = okResult.Value.Should().BeAssignableTo<ProductResource>().Subject;

            productResult.Id.Should().Be(1);
        }

       

        [Fact]
        public async void GetProducts_Valid_ShouldReturnOkObjectResultWithListOfProductResource()
        {
            var newProductResources = new List<ProductResource>() {new ProductResource() {Id = 1}};
            var newProducts = new List<Product>() {new Product() {Id = 1}};

            _mockRepository.Setup(g => g.GetProducts()).ReturnsAsync(newProducts);

            _mockMapper.Setup(m =>
                    m.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(It.IsAny<IEnumerable<Product>>()))
                .Returns(newProductResources);

            var result = await _controller.GetProducts();


            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;
            var productResult = okResult.Value.Should().BeAssignableTo<IEnumerable<ProductResource>>().Subject;

            productResult.ElementAt(0).Id.Should().Be(1);
        }


    }
}
