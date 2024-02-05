using CarRentalSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Repository
{
    internal interface ICarLeaseRepository
    {

        void AddCar(Vehicle vehicle);
        
        void RemoveCar(int vehicleID);
       
        List<Vehicle> ListAvailableCars();
        
        List<Vehicle> ListRentedCars();
       
        Vehicle FindCarById(int vehicleID);
       
        void AddCustomer(Customer customer);

       void RemoveCustomer(int customerID);
      
        List<Customer> ListCustomers();
        Customer FindCustomerById(int customerID);

        
        Lease CreateLease(int customerID, int vehicleID, DateTime startDate, DateTime endDate,string type);
        
        void ReturnCar(int leaseID);
        
        List<Lease> ListActiveLeases();
       
        List<Lease> ListLeaseHistory();
 
        void RecordPayment(Lease lease, double amount);
       
    }

}

