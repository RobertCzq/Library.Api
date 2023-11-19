using Library.Api.Controllers;

namespace Library.Api.UnitTests.Fixtures
{
    internal static class BooksControllerFixtures
    {
        public static BooksController SetupSut()
        {
            var sut = new BooksController();
            return sut;
        }
    }
}
