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
using System.Xml.Schema;

namespace CarRentalSystem.Repository
{
    public class ICarLeaseRepositoryImpl : ICarLeaseRepository
    {
        public string connectionString;
        SqlConnection sqlconnection = null;
        SqlCommand cmd = null;

        public ICarLeaseRepositoryImpl()
        {

            sqlconnection = new SqlConnection(DBConnUtil.GetConnectionString());
            cmd = new SqlCommand();
        }

        public int AddCar(Vehicle vehicle)
        {
            int addCarStatus = 0;
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

                addCarStatus = cmd.ExecuteNonQuery();
                sqlconnection.Close();
                return addCarStatus;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
            return addCarStatus;
        }

        public void RemoveCar(int vehicleID)
        {
          
                cmd.CommandText = "DELETE FROM Vehicle WHERE vehicleID = @vehicleID and vehicleID not in (select vehicleID from Lease where endDate >= getDate());";
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

            SqlDataReader reader = cmd.ExecuteReader();
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
        
        public int CreateLease(int customerID, int vehicleID, DateTime startDate, DateTime endDate,string type)
        {
            cmd.Connection = sqlconnection;
            cmd.CommandText = "insert into Lease values(@VID,@CID,@starttDate,@enddDate,@type)" + "Update Vehicle SET status = 'notAvailable' WHERE vehicleID = @VID;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@CID", customerID);
            cmd.Parameters.AddWithValue("@VID", vehicleID);
            cmd.Parameters.AddWithValue("@starttDate", startDate);
            cmd.Parameters.AddWithValue("@enddDate", endDate);
            cmd.Parameters.AddWithValue("@type", type);
            sqlconnection.Open();
            int createLeaseStatus = cmd.ExecuteNonQuery();
            sqlconnection.Close();

            return createLeaseStatus;
        }
        
        public void ReturnCar(int leaseID)
        {
            int check = checkDues(leaseID);
            if (check == 0)
            {
                Console.WriteLine("Not able to return car as the associated lease has dues. Please clear them before returning.");
            }
            else
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
                    throw new LeaseNotFoundException($"Lease with id {leaseID} not found");

                }
            }
        }

        public int checkDues(int leaseID)
        {
            cmd.CommandText = "select sum(amount) as total from payment where leaseID=@leaseID group by leaseID";
            sqlconnection.Open();
            cmd.Connection = sqlconnection;

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@leaseID", leaseID);
            SqlDataReader reader = cmd.ExecuteReader();

            double paid_amount = 0;

            // Check if there are rows before trying to read
            if (reader.Read())
            {
                // Check if the column exists before reading
                if (reader["total"] != DBNull.Value)
                {
                    paid_amount = Convert.ToDouble(reader["total"]);
                }
                else 
                {
                    paid_amount = 0.0d;
                }
            }

            reader.Close();
            sqlconnection.Close();

            cmd.CommandText = "select startDate,vehicleID from Lease where leaseID=@leaseID";

            sqlconnection.Open();
            cmd.Connection = sqlconnection;

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@leaseID", leaseID);
            SqlDataReader reader1 = cmd.ExecuteReader();

            DateTime sDate = DateTime.MinValue; // Initialize with a default value
            int vehicle_id = 0;

            // Check if there are rows before trying to read
            if (reader1.Read())
            {
                // Check if the columns exist before reading
                if (reader1["startDate"] != DBNull.Value)
                {
                    sDate = (DateTime)reader1["startDate"];
                }
                if (reader1["vehicleID"] != DBNull.Value)
                {
                    vehicle_id = (int)reader1["vehicleID"];
                }
            }

            reader1.Close();
            sqlconnection.Close();

            TimeSpan dateDiff = DateTime.Now - sDate;
            int days = dateDiff.Days;

            Vehicle vehicle = FindCarById(vehicle_id);

            double total = (double)(vehicle.DailyRate * days);
     
            if (total <= paid_amount)
            {
                return 1;
            }
            else if (total > paid_amount)
            {
                double dues = total - paid_amount;
                Console.WriteLine($"Please make payment of Rs.{dues} for {days} days(Rs.{vehicle.DailyRate} per day)");
                Console.WriteLine("Press 1 to proceed with payment or 0 complete payment and return car later.");
                int choice = int.Parse(Console.ReadLine());
                if (choice == 1)
                {
                    Lease lease = new Lease { LeaseID = leaseID };
                    RecordPayment(lease, dues);
                    return 1;
                }
                else
                {
                    return 0;
                }
            }
            return 1;
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

        public Lease FindLeaseById(int leaseID)
        {
            Lease lease = new Lease();
            cmd.CommandText = "select * from Lease where leaseID = @ID;";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ID", leaseID);
            cmd.Connection = sqlconnection;
            sqlconnection.Open();

            SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    lease.LeaseID = (int)reader["leaseID"];
                    lease.VehicleID = (int)reader["vehicleID"];
                    lease.CustomerID = (int)reader["customerID"];
                    lease.StartDate = (DateTime)reader["startDate"];
                    lease.EndDate = (DateTime)reader["endDate"];
                    lease.Type = (string)reader["type"];
                    sqlconnection.Close();

                }
                else
                {
                    sqlconnection.Close();
                    throw new LeaseNotFoundException($"Lease with ID {leaseID} not found.");
                }
            

            sqlconnection.Close();
            return lease;
        }


    }
}
