using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Configuration;
using P3AddNewFunctionalityDotNetCore.Data;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Entities;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests
{
    public class IntegrationTests
    {
        private readonly IStringLocalizer<ProductService> _localizer;
        private readonly IConfiguration _configuration;

        [Fact]
        public void SaveNewProductToDbTest()
        {
            var options = new DbContextOptionsBuilder<P3Referential>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

            var ctx = new P3Referential(options, _configuration);

            var cart = new Cart();
            var productRepository = new ProductRepository(ctx);
            var orderRepository = new OrderRepository(ctx);
            var languageService = new LanguageService();

            ProductController productController = null;

            productController = new ProductController(new ProductService(cart, productRepository, orderRepository, _localizer), languageService);

            Assert.NotNull(productController);
        }

        [Fact]
        public void GetProductByIdTest()
        {
            var cart = new Models.Cart();
            var options = new DbContextOptionsBuilder<P3Referential>()
                //.UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;
            ProductService productService = null;
            Product product = null;

            using (var ctx = new P3Referential(options, _configuration))
            {
                productService = new ProductService(cart,
                    new ProductRepository(ctx),
                    new OrderRepository(ctx), _localizer);
                product = productService.GetProductById(1);
            }

            Assert.NotNull(product);
            Assert.Equal(1, product.Id);
        }

        [Fact]
        public void DeleteProductFromDbAndRemoveFromCartTest()
        {
            var cart = new Models.Cart();
            var options = new DbContextOptionsBuilder<P3Referential>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=P3Referential-2f561d3b-493f-46fd-83c9-6e2643e7bd0a;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;
            ProductService productService = null;
            ProductViewModel newProduct = new ProductViewModel
            {
                Name = "UnDroleDeNom",
                Price = "1000",
                Description = "UneDroleDeChose",
                Stock = "1",
                Details = "VraimentDrole"
            };

            var product = new Product();

            using (var ctx = new P3Referential(options, _configuration))
            {
                productService = new ProductService(cart,
                    new ProductRepository(ctx),
                    new OrderRepository(ctx), _localizer);
                productService.SaveProduct(newProduct);

                product = ctx.Product.FirstOrDefault(p => p.Name == "UnDroleDeNom");
                Assert.NotNull(product);

                cart.AddItem(product, 3);
                Assert.NotEmpty(cart.Lines);

                productService.DeleteProduct(product.Id);
                product = productService.GetProductById(product.Id);
            }

            Assert.Null(product);
            Assert.Empty(cart.Lines);
        }
    }
}

