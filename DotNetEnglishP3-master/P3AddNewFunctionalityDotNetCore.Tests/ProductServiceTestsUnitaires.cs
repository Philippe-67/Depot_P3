using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Moq;
using P3AddNewFunctionalityDotNetCore.Controllers;
using P3AddNewFunctionalityDotNetCore.Models;
using P3AddNewFunctionalityDotNetCore.Models.Repositories;
using P3AddNewFunctionalityDotNetCore.Models.Services;
using P3AddNewFunctionalityDotNetCore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xunit;

namespace P3AddNewFunctionalityDotNetCore.Tests //convention de nomage
{
    //public class ProductViewModelValidationTests //convention de nomage
    public class ProductServiceTestsUnitaires
    {
        /// <summary>
        /// Take this test method as a template to write your test method.
        /// A test method must check if a definite method does its job:
        /// returns an expected value from a particular set of parameters
        /// </summary>
     
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
                Name = string.Empty,
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
                Name = "Martin",
                Price = "10",
                Stock = "50",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act

            var result = productController.Create(product) as ViewResult;//voir si ca marche PHCH
            var values = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in values)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
            }
            // Assert
            Assert.Null(expected);
   
        }
        [Fact]
        public void Price_NotEmpty_Empty()
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
                Price = "",
                Stock = "50",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in modelStateValues)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
            }
            int indice = expected.IndexOf("Please enter a price");
          
            //Assert
            Assert.Contains("Please enter a price",expected);
        }
        [Fact]
        public void Price_NotEmpty_Integer()
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
                Price = "a",
                Stock = "50",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
            var result = productController.Create(product) as ViewResult;
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
            Assert.Contains("The value entered for the price must be a number",expected);
        }
        [Fact]
        public void Price_NotEmpty_IntegerGreaterThanZero()
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
                Price = "0",
                Stock = "50",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
            var result = productController.Create(product) as ViewResult;
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
            Assert.Contains("The price must be greater than zero",expected);
        }
        [Fact]
        public void Stock_NotEmpty_Empty()
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
                Name = "martin",
                Price = "10",
                Stock = "",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
            var result = productController.Create(product) as ViewResult;
            var modelStateValues = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in modelStateValues)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
            }
            int indice = expected.IndexOf("Please enter a stock value");
            //Assert
            Assert.Contains("Please enter a stock value",expected);

        }
        [Fact]
        public void Stock_NotEmpty_WithInteger()
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
                Name = "martin",
                Price = "10",
                Stock = "a",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
            var result = productController.Create(product) as ViewResult;
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
            Assert.Contains("The value entered for the stock must be an integer", expected);
        }
          [Fact]
        public void Stock_NotEmpty_TheStockMustGreaterThanZero()
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
                Name = "martin",
                Price = "10",
                Stock = "-1",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("en-GB");
            // Act
            var result = productController.Create(product) as ViewResult;
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
            Assert.Contains("The stock must greater than zero", expected);
        }
        [Fact]
        public void FieldNametest0fr_IsRequired_IsEmpty()
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
                Name = string.Empty,
                Price = "10",
                Stock = "50",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
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
            Assert.Contains("Veuillez saisir un nom", expected);
        }
        [Fact]
        public void FieldNametest1fr_IsRequired_IsCorrect()
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
                Price = "10",
                Stock = "50",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            // Act

            var result = productController.Create(product) as ViewResult;//voir si ca marche PHCH
            var values = result.ViewData.ModelState.Values;
            List<string> expected = new List<string>();
            foreach (var res in values)
            {
                foreach (var err in res.Errors)
                {
                    expected.Add(err.ErrorMessage);
                }
            }
            // Assert
            Assert.Null(expected);
          
        }
        [Fact]
        public void Pricefr_NotEmpty_Empty()
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
                Price = "",
                Stock = "50",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            // Act
            var result = productController.Create(product) as ViewResult;
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
            Assert.Contains("Veuillez saisir un prix", expected);
            }
        [Fact]
        public void Pricefr_NotEmpty_Integer()
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
                Price = "a",
                Stock = "50",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            // Act
            var result = productController.Create(product) as ViewResult;
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
            Assert.Contains("La valeur saisie pour le prix doit être un nombre", expected);
            }
        [Fact]
        public void Pricefr_NotEmpty_IntegerGreaterThanZero()
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
                Price = "0",
                Stock = "50",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            // Act
            var result = productController.Create(product) as ViewResult;
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
            Assert.Contains("Le prix doit être supérieur à zéro", expected);
            }
        [Fact]
        public void Stockfr_NotEmpty_Empty()
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
                Name = "martin",
                Price = "10",
                Stock = "",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            // Act
            var result = productController.Create(product) as ViewResult;
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
            Assert.Contains("Veuillez saisir un stock", expected);

        }
        [Fact]
        public void Stockfr_NotEmpty_WithInteger()
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
                Name = "martin",
                Price = "10",
                Stock = "a",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            // Act
            var result = productController.Create(product) as ViewResult;
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
            Assert.Contains("La valeur saisie pour le stock doit être un entier", expected);
        }
        [Fact]
        public void Stockfr_NotEmpty_TheStockMustGreaterThanZero()
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
                Name = "martin",
                Price = "10",
                Stock = "-1",
            };
            CultureInfo.CurrentUICulture = new CultureInfo("fr-FR");
            // Act
            var result = productController.Create(product) as ViewResult;
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
            Assert.Contains("Les stock doit être supérieure à zéro", expected);
        }
    }
}
    

