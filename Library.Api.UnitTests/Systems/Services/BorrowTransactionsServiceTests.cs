using FluentAssertions;
using Library.Api.Infrastructure.Interfaces;
using Library.Api.Infrastructure.Models;
using Library.Api.Services;
using Library.Api.Services.Interfaces;
using Moq;

namespace Library.Api.UnitTests.Systems.Services
{
    public class BorrowTransactionsServiceTests
    {
        #region GetByMemberId

        [Fact]
        public async Task GetByMemberId_OnSuccess_ReturnsBorrowTransactionList()
        {
            //Arrange
            var booksService = new Mock<IBooksService>();
            var membersService = new Mock<IMembersService>();
            var repository = new Mock<IBorrowTransactionsRepository>();
            repository.Setup(s => s.GetByMemberId(It.IsAny<int>()))
                .ReturnsAsync(new List<BorrowTransaction>() { new BorrowTransaction() });
            var sut = new BorrowTransactionsService(repository.Object, booksService.Object, membersService.Object);

            //Act
            var result = await sut.GetByMemberId(It.IsAny<int>());

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<List<BorrowTransaction>>();
        }

        [Fact]
        public async Task GetByMemberId_OnNotFound_ReturnsEmpty()
        {
            //Arrange
            var booksService = new Mock<IBooksService>();
            var membersService = new Mock<IMembersService>();
            var repository = new Mock<IBorrowTransactionsRepository>();
            var sut = new BorrowTransactionsService(repository.Object, booksService.Object, membersService.Object);

            //Act
            var result = await sut.GetByMemberId(It.IsAny<int>());

            //Assert
            result.Should().BeEmpty();
        }

        #endregion

        #region Add

        [Fact]
        public async Task Add_OnSuccess_ReturnsTrue()
        {
            //Arrange
            var booksService = new Mock<IBooksService>();
            booksService.Setup(s => s.Get(It.IsAny<int>()))
                .ReturnsAsync(new Book() { IsAvailable = true });
            var membersService = new Mock<IMembersService>();
            membersService.Setup(s => s.Get(It.IsAny<int>()))
                .ReturnsAsync(new Member());
            var repository = new Mock<IBorrowTransactionsRepository>();
            repository.Setup(s => s.Add(It.IsAny<BorrowTransaction>()))
                .ReturnsAsync(true);
            var sut = new BorrowTransactionsService(repository.Object, booksService.Object, membersService.Object);
            var mockTransaction = new Mock<BorrowTransaction>();

            //Act
            var result = await sut.Add(mockTransaction.Object);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Add_WhenBookIsNotAvailable_ReturnsFalse()
        {
            //Arrange
            var booksService = new Mock<IBooksService>();
            booksService.Setup(s => s.Get(It.IsAny<int>()))
                .ReturnsAsync(new Book() { IsAvailable = false });
            var membersService = new Mock<IMembersService>();
            membersService.Setup(s => s.Get(It.IsAny<int>()))
                .ReturnsAsync(new Member());
            var repository = new Mock<IBorrowTransactionsRepository>();
            repository.Setup(s => s.Add(It.IsAny<BorrowTransaction>()))
                .ReturnsAsync(true);
            var sut = new BorrowTransactionsService(repository.Object, booksService.Object, membersService.Object);
            var mockTransaction = new Mock<BorrowTransaction>();

            //Act
            var result = await sut.Add(mockTransaction.Object);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Add_WhenBookDoesNotExist_ReturnsFalse()
        {
            //Arrange
            var booksService = new Mock<IBooksService>();
            var membersService = new Mock<IMembersService>();
            membersService.Setup(s => s.Get(It.IsAny<int>()))
                .ReturnsAsync(new Member());
            var repository = new Mock<IBorrowTransactionsRepository>();
            repository.Setup(s => s.Add(It.IsAny<BorrowTransaction>()))
                .ReturnsAsync(true);
            var sut = new BorrowTransactionsService(repository.Object, booksService.Object, membersService.Object);
            var mockTransaction = new Mock<BorrowTransaction>();

            //Act
            var result = await sut.Add(mockTransaction.Object);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Add_OnFail_ReturnsFalse()
        {
            //Arrange
            var booksService = new Mock<IBooksService>();
            booksService.Setup(s => s.Get(It.IsAny<int>()))
                .ReturnsAsync(new Book() { IsAvailable = true });
            var membersService = new Mock<IMembersService>();
            membersService.Setup(s => s.Get(It.IsAny<int>()))
                .ReturnsAsync(new Member());
            var repository = new Mock<IBorrowTransactionsRepository>();
            repository.Setup(s => s.Add(It.IsAny<BorrowTransaction>()))
                .ReturnsAsync(false);
            var sut = new BorrowTransactionsService(repository.Object, booksService.Object, membersService.Object);
            var mockTransaction = new Mock<BorrowTransaction>();

            //Act
            var result = await sut.Add(mockTransaction.Object);

            //Assert
            result.Should().BeFalse();
        }

        #endregion

        #region Update

        [Fact]
        public async Task Update_OnSuccess_ReturnsTrue()
        {
            //Arrange
            var booksService = new Mock<IBooksService>();
            var membersService = new Mock<IMembersService>();
            var repository = new Mock<IBorrowTransactionsRepository>();
            repository.Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(new BorrowTransaction());
            repository.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync(true);
            var sut = new BorrowTransactionsService(repository.Object, booksService.Object, membersService.Object);

            //Act
            var result = await sut.Update(It.IsAny<int>(), DateTime.UtcNow);

            //Assert
            result.Should().BeTrue();
        }

        [Fact]
        public async Task Update_OnFail_ReturnsFalse()
        {
            //Arrange
            var booksService = new Mock<IBooksService>();
            var membersService = new Mock<IMembersService>();
            var repository = new Mock<IBorrowTransactionsRepository>();
            repository.Setup(s => s.GetById(It.IsAny<int>()))
                .ReturnsAsync(new BorrowTransaction());
            repository.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync(false);
            var sut = new BorrowTransactionsService(repository.Object, booksService.Object, membersService.Object);

            //Act
            var result = await sut.Update(It.IsAny<int>(), DateTime.UtcNow);

            //Assert
            result.Should().BeFalse();
        }

        [Fact]
        public async Task Update_OnTransactionNotFound_ReturnsFalse()
        {
            //Arrange
            var booksService = new Mock<IBooksService>();
            var membersService = new Mock<IMembersService>();
            var repository = new Mock<IBorrowTransactionsRepository>();
            repository.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>()))
                .ReturnsAsync(true);
            var sut = new BorrowTransactionsService(repository.Object, booksService.Object, membersService.Object);

            //Act
            var result = await sut.Update(It.IsAny<int>(), DateTime.UtcNow);

            //Assert
            result.Should().BeFalse();
        }

        #endregion

    }
}
