using BusinessLogic;
using BusinessLogic.Service;
using Microsoft.EntityFrameworkCore;
using Models;
using Moq;
using Lesson25.Models;

namespace BusinessLogicTests
{
    public class ProductServiceTests
    {
        [Fact]
        public void GetAll_WhenResultIsNotEmpty()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "First",
                    Price = 1
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Second",
                    Price = 2
                }
            };

            var mockSet = InitHelpers.GetQueryableMockDbSet(products);
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Products).Returns(mockSet);
            var validator = new ProductValidator();

            var service = new ProductService(mockContext.Object, (FluentValidation.IValidator<Product>)validator);

            // Act
            var result = service.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(products.Count(), result.Count());
        }

        [Fact]
        public void GetAll_WhenResultIsEmpty()
        {
            // Arrange
            var mockSet = InitHelpers.GetQueryableMockDbSet(new List<Product>());
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Products).Returns(mockSet);
            var validator = new ProductValidator();

            var service = new ProductService(mockContext.Object, (FluentValidation.IValidator<Product>)validator);

            // Act
            var result = service.GetAll();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(0, result.Count());
        }

        [Fact]
        public void Create_WhenModelIsValid()
        {
            // Arrange
            var mockSet = InitHelpers.GetQueryableMockDbSet(new List<Product>());
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Products).Returns(mockSet);
            var validator = new ProductValidator();

            var service = new ProductService(mockContext.Object, (FluentValidation.IValidator<Product>)validator);

            // Act
            var result = service.Create(new Product
            {
                Id = Guid.NewGuid(),
                Name = "First",
                Price = 1
            });

            //Assert
            Assert.NotEqual(Guid.Empty, result);
            Assert.Equal(1, mockContext.Object.Products.Count());
        }

        [Fact]
        public void Create_WhenModelIsValidAndListIsNotEmpty()
        {
            // Arrange
            var products = new List<Product>
            {
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "First",
                    Price = 1
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Second",
                    Price = 2
                }
            };

            var mockSet = InitHelpers.GetQueryableMockDbSet(products);
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Products).Returns(mockSet);
            var validator = new ProductValidator();

            var service = new ProductService(mockContext.Object, (FluentValidation.IValidator<Product>)validator);

            // Act
            var result = service.Create(new Product
            {
                Id = Guid.NewGuid(),
                Name = "First",
                Price = 1
            });

            //Assert
            Assert.NotEqual(Guid.Empty, result);
            Assert.Equal(3, mockContext.Object.Products.Count());
        }

        [Fact]
        public void Edit_WhenModelIsValid_ReturnsUpdatedMeeting()
        {
            var productId = Guid.NewGuid();

            var products = new List<Product>
            {
                new Product()
                {
                    Id = productId,
                    Name = "First",
                    Price = 1
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Second",
                    Price = 2
                }
            };

            var mockSet = InitHelpers.GetQueryableMockDbSet(products);
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(m => m.Products).Returns(mockSet);
            var validator = new ProductValidator();

            var service = new ProductService(mockContext.Object, (FluentValidation.IValidator<Product>)validator);

            // Act
            var updatedMeeting = new Product
            {
                Id = productId,
                Name = "First product updated",
                Price = 3
            };

            var result = service.Edit(updatedMeeting);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(result.Id, updatedMeeting.Id);
            Assert.Equal(result.Name, updatedMeeting.Name);
            Assert.Equal(result.Price, updatedMeeting.Price);

            Assert.Equal(2, mockContext.Object.Products.Count());
        }

        [Fact]
        public void Edit_ProductDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var updatedProduct = new Product 
            { 
                Id = productId, 
                Name = "UpdatedProduct", 
                Price = 15.0m 
            };

            var mockDbSet = InitHelpers.GetQueryableMockDbSet(new List<Product>());
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(db => db.Products).Returns(mockDbSet);
            var validator = new ProductValidator();

            var service = new ProductService(mockContext.Object, (FluentValidation.IValidator<Product>)validator);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => service.Edit(updatedProduct));
        }

        [Fact]
        public void DeleteById_ProductExists_RemovesProductAndSavesChanges()
        {
            // Arrange
            var productId = Guid.NewGuid();
            var existingProduct = new Product 
            { 
                Id = productId, 
                Name = "Product1", 
                Price = 1 
            };

            var products = new List<Product> { existingProduct };

            var mockDbSet = InitHelpers.GetQueryableMockDbSet(new List<Product>());
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(db => db.Products).Returns(mockDbSet);
            var validator = new ProductValidator();

            var service = new ProductService(mockContext.Object, (FluentValidation.IValidator<Product>)validator);

            // Act
            service.DeleteById(productId);

            // Assert
            mockContext.Verify(db => db.SaveChanges(), Times.Once);
        }

        [Fact]
        public void DeleteById_ProductDoesNotExist_ThrowsArgumentException()
        {
            // Arrange
            var productId = Guid.NewGuid();

            var mockDbSet = InitHelpers.GetQueryableMockDbSet(new List<Product>());
            var mockContext = InitHelpers.GetDbContext();
            mockContext.Setup(db => db.Products).Returns(mockDbSet);
            var validator = new ProductValidator();

            var service = new ProductService(mockContext.Object, (FluentValidation.IValidator<Product>)validator);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => service.DeleteById(productId));
            Assert.Equal("No such id exists", exception.Message);

            // Verify that SaveChanges was not called
            mockContext.Verify(db => db.SaveChanges(), Times.Never);
        }
    }
}
