using System;
using System.Collections.Generic;

namespace SWAD_Assg2
{
    class Program
    {
        static CarController carController;

        static void Main(string[] args)
        {
            // Simulate a logged-in car owner
            CarOwner carOwner = new CarOwner(1, new List<Car>(), 0);
            carController = new CarController(carOwner);

            // Simulate user clicking on registerCar
            registerCar();
        }

        static void registerCar()
        {
            showCarDetailsPrompt();
            inputCarDetails(out string make, out string model, out int year, out int mileage, out string color, out string licensePlate, out string vin, out List<string> photos, out string insuranceCoverage, out string description, out decimal rentalRate);

            // Validate the details
            bool validateResult = carController.ValidateDetails(licensePlate, vin);
            if (validateResult)
            {
                string result = carController.SubmitCarDetails(make, model, year, mileage, color, licensePlate, vin, photos, insuranceCoverage, description, rentalRate);
                showValidatedResult(result);
            }
            else
            {
                showErrorMessage("Car already registered or validation failed.");
            }
        }

        static void showCarDetailsPrompt()
        {
            Console.WriteLine("Please enter the car details:");
        }

        static void inputCarDetails(out string make, out string model, out int year, out int mileage, out string color, out string licensePlate, out string vin, out List<string> photos, out string insuranceCoverage, out string description, out decimal rentalRate)
        {
            Console.Write("Make: ");
            make = Console.ReadLine();

            Console.Write("Model: ");
            model = Console.ReadLine();

            Console.Write("Year of Manufacture: ");
            year = int.Parse(Console.ReadLine());

            Console.Write("Mileage: ");
            mileage = int.Parse(Console.ReadLine());

            Console.Write("Color: ");
            color = Console.ReadLine();

            Console.Write("License Plate Number: ");
            licensePlate = Console.ReadLine();

            Console.Write("VIN: ");
            vin = Console.ReadLine();

            photos = new List<string>();
            Console.Write("Photo 1: ");
            photos.Add(Console.ReadLine());

            Console.Write("Insurance Coverage: ");
            insuranceCoverage = Console.ReadLine();

            Console.Write("Description: ");
            description = Console.ReadLine();

            Console.Write("Rental Rate: ");
            rentalRate = decimal.Parse(Console.ReadLine());
        }

        static void showValidatedResult(string result)
        {
            Console.WriteLine(result);
        }

        static void showErrorMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
