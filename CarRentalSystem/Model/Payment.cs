using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Model
{
    internal class Payment
    {
        private int paymentID;
        private int leaseID;
        private DateTime paymentDate;
        private double amount;

        public Payment() { }

        public Payment(int paymentID, int leaseID, DateTime paymentDate, double amount)
        {
            PaymentID = paymentID;
            LeaseID = leaseID;
            PaymentDate = paymentDate;
            Amount = amount;
        }

        public int PaymentID
        {
            get { return paymentID; }
            set { paymentID = value; }
        }
        public int LeaseID
        {
            get { return leaseID; }
            set { leaseID = value; }
        }
        public DateTime PaymentDate
        {
            get { return paymentDate; }
            set { paymentDate = value; }
        }
        public double Amount
        {
            get { return amount; }
            set { amount = value; }
        }
    }
}
