using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Data;
using FoodOrderWebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Persistence;
using Persistence.Services;
using Xunit;

namespace WebAPI.Tests
{
    public class FoodOrderTests
    {
        private readonly FoodOrderContext _context;
        private readonly FoodOrderService _service;

        public FoodOrderTests()
        {
            var options = new DbContextOptionsBuilder<FoodOrderContext>()
                .EnableSensitiveDataLogging()
                .UseInMemoryDatabase("TestDb")
                .Options;

            _context = new FoodOrderContext(options);
            TestDbInitializer.Initialize(_context);

            _context.ChangeTracker.Clear();
            _service = new FoodOrderService(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        #region CategoriesController

        [Fact]
        public void GetCategoryGood()
        {
            var controller = new CategoriesController(_service);
            var result = controller.GetCategory(1);

            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<CategoryDTO>(objectResult.Value);
            Assert.Equal(1, model.Id);
        }
        [Fact]
        public void GetCategoryNotFound()
        {
            var controller = new CategoriesController(_service);
            var result = controller.GetCategory(1000);
            
            var objectResult = Assert.IsType<NotFoundResult>(result);
        }

        #endregion

        #region ItemsController

        //GEtItems, PostItems

        [Fact]
        public void GetItemsOfOrder()
        {
            var controller = new ItemsController(_service);
            var result = controller.GetItems(1);

            var objectResult = Assert.IsType<ActionResult<IEnumerable<CartItemDTO>>>(result);
        }

        [Fact]
        public void PostItem()
        {
            var categoryDto = _service.GetCategories().First().ToDto();

            var newItem = new ItemDTO()
            {
                Name = "TesztÉtelÚj",
                Description = "Teszt Leírás Új, Teszt Leírás Új, Teszt Leírás Új.",
                Price = 2500,
                Spicy = true,
                Vegan = true,
                Category = categoryDto
            };

            var controller = new ItemsController(_service);
            var result = controller.PostItem(newItem);

            // Assert
            var objectResult = Assert.IsType<OkResult>(result);
        }

        #endregion

        #region OrdersController

        [Fact]
        public void GetOrders()
        {
            var controller = new OrdersController(_service);
            var result = controller.GetOrders();

            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<OrderDTO>>(objectResult.Value).ToList();
        }

        [Fact]
        public void GetOrder()
        {
            var controller = new OrdersController(_service);
            var result = controller.GetOrders(1);

            var objectResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<OrderDTO>(objectResult.Value);
            Assert.Equal(1, model.Id);
        }
        
        [Fact]
        public void ChangeOrder()
        {
            var orderDto = _context.Orders.First().ToDto();
            var newOrder = orderDto;
            newOrder.TimeOfCompletion = DateTime.Now;

            var controller = new OrdersController(_service);
            var result = controller.PutOrders(1,newOrder);

            
            var updateResult = controller.GetOrders(1);
            var objectResult = Assert.IsType<OkObjectResult>(updateResult);
            var model = Assert.IsAssignableFrom<OrderDTO>(objectResult.Value);
        }

        #endregion
    }
}