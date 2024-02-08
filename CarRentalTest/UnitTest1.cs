using CarRentalSystem.Repository;

namespace CarRentalTest
{
    public class Tests
    {
 
        ICarLeaseRepositoryImpl carlease = new ICarLeaseRepositoryImpl();
        [SetUp]
        public void Setup()
        {
            

        }

        [Test]

        public void Test1()
        {
            var a = carlease.ListAvailableCars();
            Assert.AreEqual(a.Count(), 5);
        }
    }
}