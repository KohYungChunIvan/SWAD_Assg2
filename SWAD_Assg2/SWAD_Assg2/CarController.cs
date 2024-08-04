using System;
using System.Collections.Generic;
using System.IO;

namespace SWAD_Assg2
{
    internal class CarController
    {
        private CarOwner _carOwner;
        private List<Car> existingCars;

        public CarController(CarOwner carOwner)
        {
            _carOwner = carOwner;
            existingCars = LoadExistingCars();
        }

        private List<Car> LoadExistingCars()
        {
            var cars = new List<Car>();
            using (var reader = new StreamReader("Car_Details.csv"))
            {
                var header = reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    var car = new Car(
                        int.Parse(values[0]), // CarId
                        true,
                        decimal.Parse(values[11]), // RentalRate
                        values[8], // InsuranceCoverage
                        values[1], // Make
                        values[2], // Model
                        int.Parse(values[3]), // Year
                        int.Parse(values[4]), // Mileage
                        new List<string> { values[9] }, // Photos
                        values[10], // Description
                        new List<string>(),
                        values[6], // LicensePlateNumber
                        values[7], // VIN
                        values[5] // Color
                    );

                    cars.Add(car);
                }
            }
            return cars;
        }

        public string SubmitCarDetails(string make, string model, int year, int mileage, string color, string licensePlate, string vin, List<string> photos, string insuranceCoverage, string description, decimal rentalRate)
        {
            // Validate the details
            bool validateResult = ValidateDetails(licensePlate, vin);
            if (!validateResult)
            {
                return "Validation failed. Car not registered.";
            }

            // Add the new car to the owner's list
            return _carOwner.AddNewCar(make, model, year, mileage, color, licensePlate, vin, photos, insuranceCoverage, description, rentalRate);
        }

        public bool ValidateDetails(string licensePlate, string vin)
        {
            // Check if the license plate and VIN are already in use in the existing cars
            bool isLicensePlateValid = !existingCars.Exists(car => car.LicensePlateNumber == licensePlate);
            bool isVinValid = !existingCars.Exists(car => car.VIN == vin);

            // Both must be valid
            return isLicensePlateValid && isVinValid;
        }

        public string AddNewCar(string make, string model, int year, int mileage, string color, string licensePlate, string vin, List<string> photos, string insuranceCoverage, string description, decimal rentalRate)
        {
            // Add the new car to the owner's list
            return _carOwner.AddNewCar(make, model, year, mileage, color, licensePlate, vin, photos, insuranceCoverage, description, rentalRate);
        }
    }
}
