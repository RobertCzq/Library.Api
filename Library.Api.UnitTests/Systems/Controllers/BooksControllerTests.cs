using FluentAssertions;
using Library.Api.Infrastructure.Models;
using Library.Api.Models;
using Library.Api.Services.Interfaces;
using Library.Api.UnitTests.Fixtures;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Library.Api.UnitTests.Systems.Controllers
{
    public class BooksControllerTests
    {
        #region Get

        [Fact]
        public async Task Get_OnSuccess_ReturnsOk()
        {
            //Arrange
            var service = new Mock<IBooksService>();
            service.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(new Mock<Book>().Object);
            var sut = BooksControllerFixtures.SetupSut();

            //Act
            var result = (OkObjectResult)await sut.Get(service.Object, It.IsAny<int>());

            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task Get_WhenNoBooks_ReturnsNotFound()
        {
            //Arrange
            var service = new Mock<IBooksService>();
            var sut = BooksControllerFixtures.SetupSut();

            //Act
            var result = (NotFoundResult)await sut.Get(service.Object, It.IsAny<int>());

            //Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task Get_OnSuccess_ReturnBook()
        {
            var service = new Mock<IBooksService>();
            service.Setup(s => s.Get(It.IsAny<int>())).ReturnsAsync(new Book());
            var sut = BooksControllerFixtures.SetupSut();

            //Act
            var result = (OkObjectResult)await sut.Get(service.Object, It.IsAny<int>());

            //Assert
            result.Should().BeOfType<OkObjectResult>();
            result.Value.Should().BeOfType<Book>();
        }

        #endregion

        #region AddBook

        [Fact]
        public async Task AddBook_OnSuccess_ReturnsCreated()
        {
            //Arrange
            var service = new Mock<IBooksService>();
            var inputMock = new Mock<BookInputModel>();
            service.Setup(s => s.Add(It.IsAny<Book>())).ReturnsAsync(true);
            var sut = BooksControllerFixtures.SetupSut();

            //Act
            var result = (CreatedResult)await sut.AddBook(service.Object, inputMock.Object);

            //Assert
            result.StatusCode.Should().Be(201);
        }

        [Fact]
        public async Task AddBook_OnSuccess_ReturnsBook()
        {
            //Arrange
            var service = new Mock<IBooksService>();
            var inputMock = new BookInputModel();
            service.Setup(s => s.Add(It.IsAny<Book>())).ReturnsAsync(true);
            var sut = BooksControllerFixtures.SetupSut();

            //Act
            var result = (CreatedResult)await sut.AddBook(service.Object, inputMock);

            //Assert
            result.Should().BeOfType<CreatedResult>();
            result.Value.Should().BeOfType<BookInputModel>();
        }

        [Fact]
        public async Task AddBook_OnFail_ReturnsNoContent()
        {
            //Arrange
            var service = new Mock<IBooksService>();
            var inputMock = new Mock<BookInputModel>();
            service.Setup(s => s.Add(It.IsAny<Book>())).ReturnsAsync(false);
            var sut = BooksControllerFixtures.SetupSut();

            //Act
            var result = (NoContentResult)await sut.AddBook(service.Object, inputMock.Object);

            //Assert
            result.StatusCode.Should().Be(204);
        }

        #endregion

        #region Update



        #endregion
    }
}