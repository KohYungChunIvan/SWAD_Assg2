using System;
using System.Collections.Generic;

namespace SWAD_Assg2
{
    internal class CarOwner
    {
        public int OwnerId { get; set; }
        public List<Car> Cars { get; set; }
        public decimal Earnings { get; set; }

        // Constructor
        public CarOwner(int ownerId, List<Car> cars, decimal earnings)
        {
            OwnerId = ownerId;
            Cars = cars ?? new List<Car>();
            Earnings = earnings;
        }

        // Method to add a car
        public string AddNewCar(string make, string model, int year, int mileage, string color, string licensePlate, string vin, List<string> photos, string insuranceCoverage, string description, decimal rentalRate)
        {
            Car newCar = new Car(0, true, rentalRate, insuranceCoverage, make, model, year, mileage, photos, description, new List<string>(), licensePlate, vin, color);
            Cars.Add(newCar);
            return "Car added successfully to the owner.";
        }

        // Method to remove a car
        public void RemoveCar(Car car)
        {
            Cars.Remove(car);
        }

        // Method to update earnings
        public void UpdateEarnings(decimal amount)
        {
            Earnings += amount;
        }
    }
}
