using CarRentalSystem;
using CarRentalSystem.Service;
namespace CarRentalSystemTest
{
    public class Tests
    {
        CarRentalService _carRentalService;

        [SetUp]
        public void Setup()
        {
            _carRentalService = new CarRentalService();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}