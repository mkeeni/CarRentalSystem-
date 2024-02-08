using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentalSystem.Model
{
    public class Vehicle
    {
        private int vehicleID;
        private string make;
        private string model;
        private int year;
        private decimal dailyRate;
        private string status;
        private int passengerCapacity;
        private int engineCapacity;

        public Vehicle() { }

        public Vehicle(int vehicleID, string make, string model, int year, decimal dailyRate, string status, int passengerCapacity, int engineCapacity)
        {
            VehicleID = vehicleID;
            Make = make;
            Model = model;
            Year = year;
            DailyRate = dailyRate;
            Status = status;
            PassengerCapacity = passengerCapacity;
            EngineCapacity = engineCapacity;
        }
        public int VehicleID 
        { 
            get { return vehicleID; } 
            set { vehicleID = value; } 
        }
        public string Make 
        { get 
            { return make; } 
            set { make = value; } 
        }
        public string Model 
        { 
            get { return model; } 
            set { model = value; } 
        }
        public int Year 
        { 
            get { return year; } 
            set { year = value; } 
        }
        public decimal DailyRate 
        { 
            get { return dailyRate; } 
            set { dailyRate = value; } 
        }
        public string Status 
        { 
            get { return status; } 
            set { status = value; } 
        }
        public int PassengerCapacity 
        { 
            get { return passengerCapacity; } 
            set { passengerCapacity = value; } 
        }
        public int EngineCapacity 
        { 
            get { return engineCapacity; } 
            set { engineCapacity = value; } 
        }
    }
}
