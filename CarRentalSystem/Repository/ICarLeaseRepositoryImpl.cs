using CarRentalSystem.Model;
using Microsoft.Data.SqlClient;
using CarRentalSystem.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using CarRentalSystem.Exceptions;

namespace CarRentalSystem.Repository
{
    internal class ICarLeaseRepositoryImpl : ICarLeaseRepository
    {
        public string connectionString = DBConnUtil.GetConnectionString();
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;

        public ICarLeaseRepositoryImpl()
        {
            sqlconnection = new SqlConnection(connectionString);
            cmd = new SqlCommand();
        }

        public void AddCar(Vehicle vehicle)
        {
            try
            {
                sqlconnection.Open();

                cmd.CommandText = "INSERT INTO Vehicle VALUES (@make, @model, @year, @dailyRate, @status, @passengerCapacity, @engineCapacity)";
                cmd.Connection = sqlconnection;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@make", vehicle.Make);
                cmd.Parameters.AddWithValue("@model", vehicle.Model);
                cmd.Parameters.AddWithValue("@year", vehicle.Year);
                cmd.Parameters.AddWithValue("@dailyRate", vehicle.DailyRate);
                cmd.Parameters.AddWithValue("@status", vehicle.Status);
                cmd.Parameters.AddWithValue("@passengerCapacity", vehicle.PassengerCapacity);
                cmd.Parameters.AddWithValue("@engineCapacity", vehicle.EngineCapacity);

                int addCarStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void RemoveCar(int vehicleID)
        {
          
                cmd.CommandText = "DELETE FROM Vehicle WHERE vehicleID = @vehicleID)";
                sqlconnection.Open();

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@vehicleID", vehicleID);
                cmd.Connection = sqlconnection;
                int removeCarStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();
                if (removeCarStatus > 0)
                {
                    Console.WriteLine("Vehicle successfully Deleted");
                }
                else
                {
                    sqlconnection.Close();
                    throw new CarNotFoundException($"Vehicle with ID {vehicleID} not found");
                }
            
 
        }

        public List<Vehicle> ListAvailableCars()
        {
            List<Vehicle> vehicleList = new List<Vehicle>();
            try
            {
                cmd.CommandText = "select * from Vehicle where status = @available1;";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@available1", "available");
                cmd.Connection = sqlconnection;
                sqlconnection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Vehicle vehicle = new Vehicle();
                    vehicle.VehicleID = (int)reader["vehicleID"];
                    vehicle.Make = (string)reader["make"];
                    vehicle.Model = (string)reader["model"];
                    vehicle.Year = (int)reader["year"];
                    vehicle.DailyRate = (decimal)reader["dailyRate"];
                    vehicle.Status = (string)reader["status"];
                    vehicle.PassengerCapacity = (int)reader["passengerCapacity"];
                    vehicle.EngineCapacity = (int)reader["engineCapacity"];
                    vehicleList.Add(vehicle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error Occured:{ex.Message}");
            }
            sqlconnection.Close();
            return vehicleList;
        }

        public List<Vehicle> ListRentedCars()
        {
            List<Vehicle> vehicleList = new List<Vehicle>();
            cmd.CommandText = "select * from Vehicle where status = @notAvailable;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@notAvailable", "notAvailable");
            cmd.Connection = sqlconnection;
            sqlconnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Vehicle vehicle = new Vehicle();
                vehicle.VehicleID = (int)reader["vehicleID"];
                vehicle.Make = (string)reader["make"];
                vehicle.Model = (string)reader["model"];
                vehicle.Year = (int)reader["year"];
                vehicle.DailyRate = (decimal)reader["dailyRate"];
                vehicle.Status = (string)reader["status"];
                vehicle.PassengerCapacity = (int)reader["passengerCapacity"];
                vehicle.EngineCapacity = (int)reader["engineCapacity"];
                vehicleList.Add(vehicle);
            }
            sqlconnection.Close();
            return vehicleList;
        }

        public Vehicle FindCarById(int vehicleID)
        {
            Vehicle vehicle = new Vehicle();
            cmd.CommandText = "select * from Vehicle where vehicleID = @ID;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", vehicleID);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    vehicle.VehicleID = (int)reader["vehicleID"];
                    vehicle.Make = (string)reader["make"];
                    vehicle.Model = (string)reader["model"];
                    vehicle.Year = (int)reader["year"];
                    vehicle.DailyRate = (decimal)reader["dailyRate"];
                    vehicle.Status = (string)reader["status"];
                    vehicle.PassengerCapacity = (int)reader["passengerCapacity"];
                    vehicle.EngineCapacity = (int)reader["engineCapacity"];
                    sqlconnection.Close();

                }
                else
                {
                    sqlconnection.Close();
                    throw new CarNotFoundException($"Vehicle with ID {vehicleID} not found.");
                }
            }

            sqlconnection.Close();
            return vehicle;
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                cmd.CommandText = "INSERT INTO Customer VALUES (@firstName, @lastName, @email, @phoneNumber)";


                sqlconnection.Open();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@firstName", customer.FirstName);
                cmd.Parameters.AddWithValue("@lastName", customer.LastName);
                cmd.Parameters.AddWithValue("@email", customer.Email);
                cmd.Parameters.AddWithValue("@phoneNumber", customer.PhoneNumber);
                cmd.Connection = sqlconnection;

                int addCustomerStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();
                if (addCustomerStatus > 0)
                {
                    Console.WriteLine("Customer Successfully added");
                }
                else
                {
                    Console.WriteLine("Error.Customer Not Added.");
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        public void RemoveCustomer(int customerID)
        {

            
                cmd.CommandText = "delete from Customer where customerID=@C_ID and @C_ID not in (select CustomerID from Lease where endDate>=GetDate());";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@C_ID", customerID);
                sqlconnection.Open();
                int deleteCustomerStatus = cmd.ExecuteNonQuery();
            if (deleteCustomerStatus > 0)
            {
                Console.WriteLine("Customer Successfully removed");
            }
            else
            {
                sqlconnection.Close();
                throw new CustomerNotFoundException($"Customer with id {customerID} not found.");

            }
            sqlconnection.Close();
        }

        public List<Customer> ListCustomers()
        {
            List<Customer> customerList = new List<Customer>();
            cmd.CommandText = "select * from Customer;";
            cmd.Connection = sqlconnection;
            sqlconnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Customer customer = new Customer();
                customer.CustomerID = (int)reader["customerID"];
                customer.FirstName = (string)reader["firstName"];
                customer.LastName = (string)reader["lastName"];
                customer.PhoneNumber = (string)reader["phoneNumber"];
                customer.Email = (string)reader["email"];
                customerList.Add(customer);
            }
            sqlconnection.Close();
            return customerList;
        }

        public Customer FindCustomerById(int customerID)
        {
            Customer customer = new Customer();

            cmd.CommandText = "SELECT * FROM Customer WHERE customerID = @custID";
            sqlconnection.Open();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@custID", customerID);
            cmd.Connection = sqlconnection;
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                customer.CustomerID = (int)reader["customerID"];
                customer.FirstName = (string)reader["firstName"];
                customer.LastName = (string)reader["lastName"];
                customer.PhoneNumber = (string)reader["phoneNumber"];
                customer.Email = (string)reader["email"];
                sqlconnection.Close();
            }
            else
            {
                sqlconnection.Close();
                throw new CustomerNotFoundException($"Customer with ID {customerID} not found.");

                

            }
 
        
            return customer;
        }
        
        public Lease CreateLease(int customerID, int vehicleID, DateTime startDate, DateTime endDate,string type)
        {
           Lease lease = new Lease();

            cmd.CommandText = "insert into Lease values(@CID,@VID,@starttDate,@enddDate,@type)" + "Update Vehicle SET status = 'notAvailable' WHERE vehicleID = @vehicleID;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CID", customerID);
            cmd.Parameters.AddWithValue("@VID", vehicleID);
            cmd.Parameters.AddWithValue("@starttDate", startDate);
            cmd.Parameters.AddWithValue("@enddDate", endDate);
            cmd.Parameters.AddWithValue("@type", type);
            sqlconnection.Open();
            int createLeaseStatus = cmd.ExecuteNonQuery();
            sqlconnection.Close();

            return lease;
        }
        
        public void ReturnCar(int leaseID)
        {
            try
            {
                cmd.CommandText = "UPDATE Vehicle SET status = 'available' WHERE vehicleID IN (SELECT vehicleID FROM Lease WHERE leaseID = @leaseID);" +
                                                       "UPDATE Lease SET endDate = GETDATE() WHERE leaseID = @leaseID;";
                    
                    sqlconnection.Open();
                    cmd.Connection = sqlconnection;

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@leaseID", leaseID);

                    int returnCarStatus = cmd.ExecuteNonQuery();
                      sqlconnection.Close();

                        if (returnCarStatus > 0)
                        {
                            Console.WriteLine("Car returned successfully.");
                        }
                        else
                        {
                            Console.WriteLine("Car not found or already returned.");
                        }
                   
         
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
        
        public List<Lease> ListActiveLeases()
        {
            List<Lease> leaseList = new List<Lease>();
            cmd.CommandText = "select * from Lease where endDate >= getDate();";
            cmd.Connection = sqlconnection;
            sqlconnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Lease lease = new Lease();
                lease.LeaseID = (int)reader["leaseID"];
                lease.VehicleID = (int)reader["vehicleID"];
                lease.CustomerID = (int)reader["customerID"];
                lease.StartDate = (DateTime)reader["startDate"];
                lease.EndDate = (DateTime)reader["endDate"];
                lease.Type = (string)reader["type"];
                leaseList.Add(lease);
            }
            sqlconnection.Close();
            return leaseList;

        }
        
        public List<Lease> ListLeaseHistory()
        {
            List<Lease> leaseList = new List<Lease>();
            cmd.CommandText = "select * from Lease where endDate<getDate();";
            cmd.Connection = sqlconnection;
            sqlconnection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Lease lease = new Lease();
                lease.LeaseID = (int)reader["leaseID"];
                lease.VehicleID = (int)reader["vehicleID"];
                lease.CustomerID = (int)reader["customerID"];
                lease.StartDate = (DateTime)reader["startDate"];
                lease.EndDate = (DateTime)reader["endDate"];
                lease.Type = (string)reader["type"];
                leaseList.Add(lease);
            }
            sqlconnection.Close();
            return leaseList;
        }
        
        public void RecordPayment(Lease lease, double amount)
        {
            cmd.CommandText = "insert into Payment values(@lID,@paymentDate,@amount);";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@lID", lease.LeaseID);
            cmd.Parameters.AddWithValue("@paymentDate",DateTime.Now);
            cmd.Parameters.AddWithValue("@amount", amount);
            sqlconnection.Open();
            try
            {
                int recordPaymentStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();
                if (recordPaymentStatus > 0)
                {
                    Console.WriteLine("Payment recorded successfully.");
                }
                else
                {
                    sqlconnection.Close();
                    throw new LeaseNotFoundException($"Lease with ID {lease.LeaseID} not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
