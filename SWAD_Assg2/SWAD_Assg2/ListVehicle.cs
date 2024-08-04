using System;
using System.Collections.Generic;

namespace SWAD_Assg2
{
    public class ListVehicle
    {
        public int CarId { get; set; }
        public bool Availability { get; set; }
        public decimal RentalRate { get; set; }
        public string InsuranceCoverage { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Mileage { get; set; }
        public List<string> Photos { get; set; }
        public string Description { get; set; }
        public string LicensePlateNumber { get; set; }
        public string VIN { get; set; }
        public string Color { get; set; }

        // Constructor
        public ListVehicle(int carId, bool availability, decimal rentalRate, string insuranceCoverage, string make, string model, int year, int mileage, List<string> photos, string description, string licensePlateNumber, string vin, string color)
        {
            CarId = carId;
            Availability = availability;
            RentalRate = rentalRate;
            InsuranceCoverage = insuranceCoverage;
            Make = make;
            Model = model;
            Year = year;
            Mileage = mileage;
            Photos = photos ?? new List<string>();
            Description = description;
            LicensePlateNumber = licensePlateNumber;
            VIN = vin;
            Color = color;
        }

        // Method to add a photo
        public void AddPhoto(string photo)
        {
            Photos.Add(photo);
        }

        // Method to update mileage
        public void UpdateMileage(int mileage)
        {
            Mileage = mileage;
        }

        // Method to update availability
        public void UpdateAvailability(bool availability)
        {
            Availability = availability;
        }

        // Method to update rental rate
        public void UpdateRentalRate(decimal rentalRate)
        {
            RentalRate = rentalRate;
        }
    }
}
 