using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAD_Assg2
{
    public class CarOwner
    {
        public int OwnerId { get; set; }
        public List<string> Cars { get; set; }
        public decimal Earnings { get; set; }

        // Constructor
        public CarOwner(int ownerId, List<string> cars, decimal earnings)
        {
            OwnerId = ownerId;
            Cars = cars;
            Earnings = earnings;
        }

        // Method to add a car
        public void AddNewCar(string car) // same name as seq diag
        {
            Cars.Add(car);
        }

        // Method to remove a car
        public void RemoveCar(string car)
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