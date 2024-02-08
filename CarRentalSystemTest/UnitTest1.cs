using CarRentalSystem;
using CarRentalSystem.Service;
using CarRentalSystem.Repository;
using CarRentalSystem.Exceptions;
using CarRentalSystem.Model;
using Microsoft.Data.SqlClient;
using System.Reflection.Metadata;
namespace CarRentalSystemTest
{
    public class Tests
    {
        ICarLeaseRepositoryImpl _carRentalService;

        [SetUp]
        public void Setup()
        {
            _carRentalService = new ICarLeaseRepositoryImpl();
        }
        
        [Test]

        public void IfCarAddedSuccessElseFail()
        {
            Vehicle testVehicle = new Vehicle
            {
                Make = "Suzuki",
                Model = "Ertiga",
                Year = 2022,
                DailyRate = 3000.0m,
                Status = "available",
                PassengerCapacity = 7,
                EngineCapacity = 2000
            };

         
            int a = _carRentalService.AddCar(testVehicle);
   
           
            Assert.IsTrue(a>0);
        }
        
        [Test]
        public void IfLeaseAddedSuccessElseFail()
        {
            Lease testLease = new Lease()
            {
                VehicleID = 10 ,
                CustomerID = 6,
                Type = "MonthlyLease",
                StartDate = DateTime.Parse("2024-02-05"),
                EndDate = DateTime.Parse("2024-04-05")
            };


            int a = _carRentalService.CreateLease(testLease.CustomerID, testLease.VehicleID,testLease.StartDate,testLease.EndDate,testLease.Type);

            Assert.IsTrue(a > 0);
        }
        
        [Test]
        public void FindCustomerById_ThrowsCustomerNotFoundException()
        {
        
            int nonExistingCustomerId = 100; 

            Assert.Throws<CustomerNotFoundException>(() => _carRentalService.FindCustomerById(nonExistingCustomerId));

        }
        
        [Test]
        public void FindCarById_ThrowsCarNotFoundException()
        {
            
            int nonExistingVehicleId = 100; 

            Assert.Throws<CarNotFoundException>(() => _carRentalService.FindCarById(nonExistingVehicleId));

        }

        [Test]
        public void FindLeaseById_ThrowsCarNotFoundException()
        {

            int nonExistingLeaseId = 100;

            Assert.Throws<LeaseNotFoundException>(() => _carRentalService.FindLeaseById(nonExistingLeaseId));

        }

    }
}