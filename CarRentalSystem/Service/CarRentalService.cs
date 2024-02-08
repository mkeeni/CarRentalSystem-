using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarRentalSystem.Exceptions;
using CarRentalSystem.Model;
using CarRentalSystem.Repository;

namespace CarRentalSystem.Service
{
    public class CarRentalService:ICarRentalService
    {
        readonly ICarLeaseRepository _carLeaseRepository;
        public CarRentalService()
        {
            _carLeaseRepository = new ICarLeaseRepositoryImpl();
        }
        public void GetAllCustomerDetailsS()
        {
            List<Customer> customerList = new List<Customer>();
            customerList = _carLeaseRepository.ListCustomers();
            foreach (var customer in customerList)
            {
                Console.WriteLine($"Customer ID: {customer.CustomerID}\t First Name: {customer.FirstName}\t Last Name: {customer.LastName}\t Email: {customer.Email}\t Phone: {customer.PhoneNumber}");
            }
        }

        public void ListActiveLeasesS()
        { 
            List<Lease> activeLeases = new List<Lease>();
            activeLeases = _carLeaseRepository.ListActiveLeases();
            foreach (var lease in activeLeases) 
            {
                Console.WriteLine($"Lease ID: {lease.LeaseID}\t Vehicle ID: {lease.VehicleID}\t Customer ID: {lease.CustomerID}\t Start Date: {lease.StartDate}\t End Date: {lease.EndDate}\t Type: {lease.Type}");
            }

        }

        public void ListLeaseHistoryS()
        {
            List<Lease> activeLeases = new List<Lease>();
            activeLeases = _carLeaseRepository.ListLeaseHistory();

            if (activeLeases.Count == 0)
            {
                Console.WriteLine("No lease History.");
            }
            else
            {
                foreach (var lease in activeLeases)
                {
                    Console.WriteLine($"Lease ID: {lease.LeaseID}\t Vehicle ID: {lease.VehicleID}\t Customer ID: {lease.CustomerID}\t Start Date: {lease.StartDate}\t End Date: {lease.EndDate}\t Type: {lease.Type}");
                }
            }
        }

        public void RecordPaymentS()
        {
            try
            {
                ListActiveLeasesS();
                Console.WriteLine("Enter lease ID for which payment is to be made: ");
                int id = int.Parse(Console.ReadLine());
                Lease lease = new Lease { LeaseID = id };
                Console.WriteLine("Enter Amount:");
                double amount = double.Parse(Console.ReadLine());
                _carLeaseRepository.RecordPayment(lease, amount);

            }
            catch (LeaseNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
        public void ListAvailableCarsS()
        {
         
                List<Vehicle> vehicles = new List<Vehicle>();
                vehicles = _carLeaseRepository.ListAvailableCars();
                foreach (var vehicle in vehicles)
                {
                    Console.WriteLine($"\nVehicle ID: {vehicle.VehicleID}\t Make: {vehicle.Make}\t Model: {vehicle.Model}\t Year: {vehicle.Year}\t Daily Rate: {vehicle.DailyRate}\t Status:{vehicle.Status}\t Passenger Capacity:{vehicle.PassengerCapacity}\t Engine Capacity:{vehicle.EngineCapacity}");
                }
        }

        public void ListRentedCarsS()
        {

            List<Vehicle> vehicles = new List<Vehicle>();
            vehicles = _carLeaseRepository.ListRentedCars();
            foreach (var vehicle in vehicles)
            {
                Console.WriteLine($"\nVehicle ID: {vehicle.VehicleID}\t Make: {vehicle.Make}\t Model: {vehicle.Model}\t Year: {vehicle.Year}\t Daily Rate: {vehicle.DailyRate}\t Status:{vehicle.Status}\t Passenger Capacity:{vehicle.PassengerCapacity}\t Engine Capacity:{vehicle.EngineCapacity}");
            }
        }

        public void FindCarByIdS()
        {
            try
            {
                Vehicle vehicle = new Vehicle();
                Console.WriteLine("Enter Vehicle ID: ");
                int id = int.Parse(Console.ReadLine());
                vehicle = _carLeaseRepository.FindCarById(id);

                Console.WriteLine($"\nVehicle ID: {vehicle.VehicleID}\t Make: {vehicle.Make}\t Model: {vehicle.Model}\t Year: {vehicle.Year}\t Daily Rate: {vehicle.DailyRate}\t Status:{vehicle.Status}\t Passenger Capacity:{vehicle.PassengerCapacity}\t Engine Capacity:{vehicle.EngineCapacity}");
            }
            catch (CarNotFoundException ex) 
            {
                Console.WriteLine($"{ex.Message}");

            }

        }

        public void AddCarS()
        {
            Console.WriteLine("Enter Make: ");
            string make = Console.ReadLine();
            Console.WriteLine("Enter Model: ");
            string model = Console.ReadLine();
            Console.WriteLine("Enter Year: ");
            int year = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Daily Rate: ");
            decimal dailyRate = decimal.Parse(Console.ReadLine());
            Console.WriteLine("Enter Status (available/notAvailable): ");
            string status = Console.ReadLine();
            Console.WriteLine("Enter Passenger Capacity: ");
            int passengerCapacity = int.Parse(Console.ReadLine());
            Console.WriteLine("Engine Capacity: ");
            int engineCapacity = int.Parse(Console.ReadLine());

            Vehicle newVehicle = new Vehicle
            {
                //VehicleID = vehicleID,
                Make = make,
                Model = model,
                Year = year,
                DailyRate = dailyRate,
                Status = status,
                PassengerCapacity = passengerCapacity,
                EngineCapacity = engineCapacity
            };

            int a = _carLeaseRepository.AddCar(newVehicle);
            Console.WriteLine("Vehicle added successfully");
        }

        public void RemoveCarS()
        {
            try
            {
                ListAvailableCarsS();
                Console.WriteLine("Enter Vehicle ID of Car to be removed: ");
                int id = int.Parse(Console.ReadLine());
                _carLeaseRepository.RemoveCar(id);
            }
            catch(CarNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }

        public void AddCustomerS()
        {
            Console.WriteLine("Enter First Name: ");
            string fname = Console.ReadLine();
            Console.WriteLine("Enter Last Name: ");
            string lname = Console.ReadLine();
            Console.WriteLine("Enter Email: ");
            string email = Console.ReadLine();
            Console.WriteLine("Enter Phone number: ");
            string phone = Console.ReadLine();

            Customer customer = new Customer 
            { 
                FirstName = fname,
                LastName = lname,
                Email = email,
                PhoneNumber = phone
            };

            _carLeaseRepository.AddCustomer(customer);
            Console.WriteLine("Customer added successfully");
        }

        public void RemoveCustomerS() 
        {
            try
            {
                GetAllCustomerDetailsS();
                Console.WriteLine("Enter Customer ID of Customer to be removed: ");
                int id = int.Parse(Console.ReadLine());
                _carLeaseRepository.RemoveCustomer(id);
                Console.WriteLine("Customer removed successfully");
            }
            catch(CustomerNotFoundException ex) 
            {
                Console.WriteLine($"{ex.Message}");

            }
        }

        public void FindCustomerByIdS() 
        {
            try
            {
                Customer customer = new Customer();
                Console.WriteLine("Enter Customer ID: ");
                int id = int.Parse(Console.ReadLine());
                customer = _carLeaseRepository.FindCustomerById(id);
                Console.WriteLine($"\nCustomer ID: {customer.CustomerID}\t First Name: {customer.FirstName}\t Last Name: {customer.LastName}\t Email: {customer.Email}\t Phone: {customer.PhoneNumber}");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine($"{ex.Message}");

            }
        }

        public void CreateLeaseS()
        {
            Console.WriteLine("Enter Customer ID: ");
            int cust_id = int.Parse(Console.ReadLine());
            Console.WriteLine("Choose From any of the below available vehicles: ");
            ListAvailableCarsS();
            Console.WriteLine("Enter Vehicle ID: ");
            int vehicle_id = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Start Date: ");
            DateTime startDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter End Date: ");
            DateTime endDate = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Enter Type (DailyLease/MonthlyLease): ");
            string types = Console.ReadLine();

            TimeSpan dateDiff = endDate - startDate;
            int days = dateDiff.Days;
            Vehicle vehicle = new Vehicle();
            vehicle = _carLeaseRepository.FindCarById(vehicle_id);
            decimal total = vehicle.DailyRate * days;
            int a = _carLeaseRepository.CreateLease(cust_id,vehicle_id,startDate,endDate,types) ;
            Console.WriteLine($"Lease created successfully. Lease amount for {days} days is Rs.{total}");

        }

        public void ReturnCarS()
        {
            Console.WriteLine("Enter Lease ID associated with car to be returned: ");
            int leaseID = int.Parse(Console.ReadLine());

            _carLeaseRepository.ReturnCar(leaseID);
            
        }

        public void FindLeaseByIdS()
        {
            try
            {
                Lease lease = new Lease();
                Console.WriteLine($"Enter Lease ID: ");
                int leaseID = int.Parse(Console.ReadLine());

                lease = _carLeaseRepository.FindLeaseById(leaseID);

                Console.WriteLine($"Lease ID:{lease.LeaseID}\tVehicle ID: {lease.VehicleID}\tCustomer ID: {lease.CustomerID}\tStart Date:{lease.StartDate}\tEnd Date:{lease.EndDate}\tType:{lease.Type}");
            }
            catch(LeaseNotFoundException ex) 
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
