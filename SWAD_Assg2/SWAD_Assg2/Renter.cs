using SWAD_Assg2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace swad_assg2
{
    public class Renter : User
    {
        public string RenterId { get; set; }
        public string BookingHistory { get; set; }
        public string PaymentDetails { get; set; }
        public string VerificationStatus { get; set; }
        public bool IsPrime { get; set; }
        public List<Booking> UpcomingRentals { get; set; } = new List<Booking>();
        public bool IsPenalised { get; set; }

        public Booking CreateBooking(int bookingId, DateTime startDateTime, DateTime endDateTime, float totalCost, string pickUpLocation, string returnLocation, Car car) //for reserve
        {
            var booking = new Booking(bookingId, startDateTime, endDateTime, totalCost, pickUpLocation, returnLocation, true)
            {
                Car = car,
                Renter = this
            };
            UpcomingRentals.Add(booking);
            return booking;
        }

        public void DisplayUpcomingRentals() //For reserve
        {
            if (UpcomingRentals.Count == 0)
            {
                Console.WriteLine("No upcoming rentals found.");
            }
            else
            {
                Console.WriteLine("Upcoming Rentals:");
                foreach (var rental in UpcomingRentals)
                {
                    Console.WriteLine($"Booking ID: {rental.BookingId}, Car: {rental.Car.Make} {rental.Car.Model}, Pick-Up: {rental.PickUpLocation}, Return: {rental.ReturnLocation}, Start: {rental.StartDateTime}, End: {rental.EndDateTime}");
                }
            }
        }
    }
}