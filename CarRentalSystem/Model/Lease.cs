using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Model
{
    internal class Lease
    {
        private int leaseID;
        private int vehicleID;
        private int customerID;
        private DateTime startDate;
        private DateTime endDate;
        private string type;

        public Lease() { }

        public Lease(int leaseID, int vehicleID, int customerID, DateTime startDate, DateTime endDate, string type)
        {
            LeaseID = leaseID;
            VehicleID = vehicleID;
            CustomerID = customerID;
            StartDate = startDate;
            EndDate = endDate;
            Type = type;
        }

        public int LeaseID
        {
            get { return leaseID; }
            set { leaseID = value; }
        }
        public int VehicleID
        {
            get { return vehicleID; }
            set { vehicleID = value; }
        }
        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }
        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
