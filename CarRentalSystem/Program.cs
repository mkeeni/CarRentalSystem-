using CarRentalSystem.Model;
using CarRentalSystem.Repository;
using CarRentalSystem.Service;
using System.Diagnostics;

namespace CarRentalSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ICarRentalService carRentalService = new CarRentalService();

            int choice;


            do
            {
                Console.WriteLine("1. Add Car");
                Console.WriteLine("2. Remove Car");
                Console.WriteLine("3. List Available Cars");
                Console.WriteLine("4. List Rented Cars");
                Console.WriteLine("5. Find Car by ID");
                Console.WriteLine("6. Add Customer");
                Console.WriteLine("7. Remove Customer");
                Console.WriteLine("8. List Customers");
                Console.WriteLine("9. Find Customer by ID");
                Console.WriteLine("10. Create Lease");
                Console.WriteLine("11. Return Car");
                Console.WriteLine("12. List Active Leases");
                Console.WriteLine("13. List Lease History");
                Console.WriteLine("14. Record Payment");
                Console.WriteLine("15. Retrieve lease by ID");
                Console.WriteLine("0. Exit");

                Console.Write("Enter your choice: ");
                choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Exiting program.");

                        break;

                    case 1:
                        carRentalService.AddCarS();
                        break;
                    case 2:
                        carRentalService.RemoveCarS();
                        break;
                    case 3:
                        
                        carRentalService.ListAvailableCarsS();
                        break;
                     
                    case 4:
                        
                        carRentalService.ListRentedCarsS();
                        break;
                    
                    case 5:
                        carRentalService.FindCarByIdS();
                        break;
                    case 6:
                        carRentalService.AddCustomerS();
                        break;
                    
                    case 7:
                        carRentalService.RemoveCustomerS();
                        break;
        
                    case 8:
                        carRentalService.GetAllCustomerDetailsS();
                        break;
                    case 9:
                        carRentalService.FindCustomerByIdS();
                        break;
                    case 10:
                        carRentalService.CreateLeaseS();
                        break;
                     case 11:
                         carRentalService.ReturnCarS();
                         break;
                    case 12:
                        carRentalService.ListActiveLeasesS();
                        break;
                    
                    case 13:
                        carRentalService.ListLeaseHistoryS();
                        break;
                    
                   case 14:
                       carRentalService.RecordPaymentS();
                       break;
                    case 15:
                        carRentalService.FindLeaseByIdS();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a valid option.");
                        break;
                }

            } while (choice != 0);


        }
    }
}
