using CarRentalSystem.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CarRentalSystem.Service
{
    public interface ICarRentalService
    {
        void GetAllCustomerDetailsS();
        void ListActiveLeasesS();
        void ListLeaseHistoryS();
        void RecordPaymentS();
        void ListAvailableCarsS();
        void ListRentedCarsS();
        void FindCarByIdS();
        void AddCarS();
        void RemoveCarS();
        void AddCustomerS();
        void RemoveCustomerS();
        void FindCustomerByIdS();
        void CreateLeaseS();
        void ReturnCarS();

    }
}
