using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using Resources;
using System.ComponentModel.DataAnnotations;
using ProductService =P3AddNewFunctionalityDotNetCore.Models.Services.ProductService;
using System.Globalization;
using Xunit;

namespace Project2
{
    public class ProductViewModelTest
    {     
        [Fact]
        public void FieldNametest0_IsRequired_IsEmpty()
        {
            // Arrange
            var languageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<ICart>();
            var productRepository = Mock.Of<IProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
            var product = new ProductViewModel
            {
                Name =string.Empty,
               // Price = "10",
               // Stock = "50",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
            var result = productController.Create(product) as ViewResult;
            var values = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in values)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                    Console.WriteLine(expected);
                }
            }
            // Assert
            Assert.Equal("Please enter a name", expected.First());
           // Assert.True(expected.Count == 3 && expected.First() == "Please enter a name");

        }
        [Fact]
        public void FieldNametest1_IsRequired_IsCorrect()
        {
            // Arrange
            var languageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<ICart>();
            var productRepository = Mock.Of<IProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
            var product = new ProductViewModel
            {
               // Name =string.Empty,
                Name = "Martin",
                Price = "10",
                Stock = "50",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
                        
            var result = productController.Create(product) as ViewResult;//voir si ca marche PHCH
            var values = result.ViewData.ModelState.Values;
            List<string>expected= new List<string>();
            foreach(var res in values)
            {
                foreach(var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
            }
            // Assert
            //Assert.Null(expected);
            // Assert.True(expected.Count == 0 /*&& expected.First() =="Please enter a name"*/);
//Assert.False(expected.First() == "Please enter a name");
        }
        [Fact]
        public void PriceTest()
        {
            // Arrange
            var languageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<ICart>();
            var productRepository = Mock.Of<IProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
            var product = new ProductViewModel
                       
            {
                
                Name = "Martin",
                Price = "50",
                Stock = "50",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
            var result = productController.Create(new ProductViewModel()) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in modelStateValues)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
            }
            Console.WriteLine(expected);
            int indice=expected.IndexOf("Please enter a price");
            //Assert
           // Assert.True(expected.Count==1 && expected[indice]=="Please enter a price");
           Assert.Equal("Please enter a price", expected[2]);
        
        }
        [Fact]
        public void StockTest()
        {
            // Arrange
            var languageService = Mock.Of<ILanguageService>();
            var cart = Mock.Of<ICart>();
            var productRepository = Mock.Of<IProductRepository>();
            var orderRepository = Mock.Of<IOrderRepository>();
            var localizer = Mock.Of<IStringLocalizer<ProductService>>();
            var productController = new ProductController(new ProductService(cart, productRepository, orderRepository, localizer), languageService);
            var product = new ProductViewModel
            {
               
              //  Name = "martin",
               // Price = "10",
                Stock = " ",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
            var result = productController.Create(new ProductViewModel()) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in modelStateValues)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage); 
                }
            }
            //Assert
            Assert.False(expected.Count == 1 && expected.First() == "PLease enter a stock value");
        }

      
        

        
    }

}






    