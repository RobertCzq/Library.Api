using FluentAssertions;
using Library.Api.Infrastructure.Interfaces;
using Library.Api.Infrastructure.Models;
using Library.Api.Services;
using Moq;

namespace Library.Api.UnitTests.Systems.Services
{
    public class BooksServiceTests
    {
        #region Get

        [Fact]
        public async Task Get_OnSuccess_ReturnsBook()
        {
            //Arrange
            var repository = new Mock<IGenericRepository<Book>>();
            repository.Setup(s => s.GetById(It.IsAny<int>())).ReturnsAsync(new Book());
            var sut = new BooksService(repository.Object);

            //Act
            var result = await sut.Get(It.IsAny<int>());

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<Book>();
        }

        [Fact]
        public async Task Get_OnNotFound_ReturnsNull()
        {
            //Arrange
            var repository = new Mock<IGenericRepository<Book>>();
            var sut = new BooksService(repository.Object);

            //Act
            var result = await sut.Get(It.IsAny<int>());

            //Assert
            result.Should().BeNull();
        }

        #endregion

        #region Add

        [Fact]
        public async Task Add_OnSuccess_ReturnsTrue()
        {
            //Arrange
            var repository = new Mock<IGenericRepository<Book>>();
            repository.Setup(s => s.Add(It.IsAny<Book>()))
                .ReturnsAsync(true);
            var sut = new BooksService(repository.Object);

            //Act
            var result = await sut.Add(It.IsAny<Book>());

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Add_OnFail_ReturnsFalse()
        {
            //Arrange
            var repository = new Mock<IGenericRepository<Book>>();
            repository.Setup(s => s.Add(It.IsAny<Book>()))
                .ReturnsAsync(false);
            var sut = new BooksService(repository.Object);

            //Act
            var result = await sut.Add(It.IsAny<Book>());

            //Assert
            result.Should().BeFalse();
        }

        #endregion

        #region Update

        [Fact]
        public async Task Update_OnSuccess_ReturnsTrue()
        {
            //Arrange
            var repository = new Mock<IGenericRepository<Book>>();
            repository.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<Book>()))
                .ReturnsAsync(true);
            var sut = new BooksService(repository.Object);

            //Act
            var result = await sut.Update(It.IsAny<int>(), It.IsAny<Book>());

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Update_OnFail_ReturnsFalse()
        {
            //Arrange
            var repository = new Mock<IGenericRepository<Book>>();
            repository.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<Book>()))
                .ReturnsAsync(false);
            var sut = new BooksService(repository.Object);

            //Act
            var result = await sut.Update(It.IsAny<int>(), It.IsAny<Book>());

            //Assert
            result.Should().BeFalse();
        }

        #endregion

        #region Delete

        [Fact]
        public async Task Delete_OnSuccess_ReturnsTrue()
        {
            //Arrange
            var repository = new Mock<IGenericRepository<Book>>();
            repository.Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(true);
            var sut = new BooksService(repository.Object);

            //Act
            var result = await sut.Delete(It.IsAny<int>());

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Delete_OnFail_ReturnsFalse()
        {
            //Arrange
            var repository = new Mock<IGenericRepository<Book>>();
            repository.Setup(s => s.Delete(It.IsAny<int>()))
                .ReturnsAsync(false);
            var sut = new BooksService(repository.Object);

            //Act
            var result = await sut.Delete(It.IsAny<int>());

            //Assert
            result.Should().BeFalse();
        }

        #endregion
    }
}
