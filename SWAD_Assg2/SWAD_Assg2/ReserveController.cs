using swad_assg2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace swad_assg2
{
    internal class ReserveController
    {
        private List<Car> cars = new List<Car>();
        private List<Renter> renters = new List<Renter>();
        private List<Booking> bookings = new List<Booking>();
        private List<ListRental> rentalDetailsList = new List<ListRental>();

        public void LoadData()
        {
            LoadCarDetails();
            LoadRenterDetails();
            LoadRentalDetails();
            LoadBookingDetails();
        }

        private void LoadCarDetails()
        {
            using (StreamReader sr = new StreamReader("Car_Details.csv"))
            {
                string s = sr.ReadLine();
                string[] header = s.Split(',');

                while ((s = sr.ReadLine()) != null)
                {
                    string[] items = s.Split(',');

                    Car car = new Car(
                        int.Parse(items[0]), // carId
                        true, // assuming availability is true
                        decimal.Parse(items[11]), // rentalRate
                        items[8], // insuranceCoverage
                        items[1], // make
                        items[2], // model
                        int.Parse(items[3]), // year
                        int.Parse(items[4]), // mileage
                        new List<string> { items[9] }, // photos
                        items[10], // description
                        new List<string>(), // reviews
                        items[6], // licensePlateNumber
                        items[7], // vin
                        items[5]  // color
                    );


                    cars.Add(car);
                }
            }
        }

        private void LoadRenterDetails()
        {
            using (StreamReader sr = new StreamReader("Renter_Details.csv"))
            {
                string s = sr.ReadLine();
                string[] header = s.Split(',');

                while ((s = sr.ReadLine()) != null)
                {
                    string[] items = s.Split(',');

                    Renter renter = new Renter
                    {
                        RenterId = items[0],
                        FullName = items[1],
                        ContactDetails = items[2],
                        DateOfBirth = DateTime.Parse(items[3]),
                        DriversLicence = new DriversLicence
                        {
                            LicenceNumber = items[4],
                            ExpiryDate = DateTime.Parse(items[5]),
                            IssuingCountry = items[6]
                        },
                        BookingHistory = items[7],
                        PaymentDetails = items[8],
                        VerificationStatus = items[9],
                        IsPrime = Convert.ToBoolean(items[10]),
                        IsPenalised = Convert.ToBoolean(items[12])
                    };

                    renters.Add(renter);
                }
            }
        }

        private void LoadRentalDetails()
        {
            using (StreamReader sr = new StreamReader("Rental_Details.csv"))
            {
                string s = sr.ReadLine();

                while ((s = sr.ReadLine()) != null)
                {
                    string[] items = s.Split(',');

                    ListRental rental = new ListRental
                    {
                        DailyRate = double.Parse(items[0]),
                        AvailabilityStartDateTime = DateTime.Parse(items[1]),
                        AvailabilityEndDateTime = DateTime.Parse(items[2]),
                        PickupLocation = items[3],
                        ReturnLocation = items[4],
                        Insurance = bool.Parse(items[5])
                    };

                    rentalDetailsList.Add(rental);
                }
            }
        }

        private void LoadBookingDetails()
        {
            if (!File.Exists("Booking_Details.csv")) return;

            using (StreamReader sr = new StreamReader("Booking_Details.csv"))
            {
                string s = sr.ReadLine();

                while ((s = sr.ReadLine()) != null)
                {
                    string[] items = s.Split(',');

                    Booking booking = new Booking(
                        int.Parse(items[0]),
                        DateTime.Parse(items[1]),
                        DateTime.Parse(items[2]),
                        float.Parse(items[3]),
                        items[4],
                        items[5],
                        bool.Parse(items[6])
                    );

                    foreach (var car in
                        cars)
                    {
                        if (car.LicensePlateNumber == items[7])
                        {
                            booking.Car = car;
                            break;
                        }
                    }

                    foreach (var renter in renters)
                    {
                        if (renter.RenterId == items[8])
                        {
                            booking.Renter = renter;
                            renter.UpcomingRentals.Add(booking);
                            break;
                        }
                    }

                    bookings.Add(booking);
                }
            }
        }

        public bool ValidateDetails(string startInput, string endInput, out DateTime startDateTime, out DateTime endDateTime)
        {
            if (DateTime.TryParse(startInput, out startDateTime) && DateTime.TryParse(endInput, out endDateTime))
            {
                return startDateTime < endDateTime;
            }
            startDateTime = default;
            endDateTime = default;
            return false;
        }

        public List<(Car car, ListRental rental)> GetAvailableCars(DateTime startDateTime, DateTime endDateTime)
        {
            var availableCars = new List<(Car car, ListRental rental)>();

            foreach (var car in cars)
            {
                if (car.IsAvailable(startDateTime, endDateTime, bookings))
                {
                    foreach (var rental in rentalDetailsList)
                    {
                        if (startDateTime >= rental.AvailabilityStartDateTime && endDateTime <= rental.AvailabilityEndDateTime)
                        {
                            availableCars.Add((car, rental));
                            break;
                        }
                    }
                }
            }

            return availableCars;
        }

        public Booking CreateBooking(int bookingId, DateTime startDateTime, DateTime endDateTime, string pickUpLocation, string returnLocation, Car car)
        {
            var renter = renters.First();

            Booking newBooking = renter.CreateBooking(bookingId, startDateTime, endDateTime, 0, pickUpLocation, returnLocation, car);

            bookings.Add(newBooking);
            WriteBookingDetailsToFile(newBooking);
            return newBooking;
        }

        private void WriteBookingDetailsToFile(Booking booking)
        {
            using (StreamWriter sw = new StreamWriter("Booking_Details.csv", true))
            {
                sw.WriteLine($"{booking.BookingId},{booking.StartDateTime},{booking.EndDateTime},{booking.TotalCost},{booking.PickUpLocation},{booking.ReturnLocation},{booking.BookingStatus},{booking.Car.LicensePlateNumber},{booking.Renter.RenterId}");
            }
        }

        public int GetNextBookingId()
        {
            return bookings.Count + 1;
        }
    }
}
