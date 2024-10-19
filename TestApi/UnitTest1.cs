

using api.Constrollers;
using api.Data;
using api.Repository;
using NSubstitute;

namespace TestApi
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

            // Arange
            var mockDbContext = Substitute.For<IApplicationDbContext>();
            var mockRepo = Substitute.For<IStockRepository>();
            var controller = new StockController(mockDbContext, mockRepo);
            // Act


            // Assert


        }
    }
}