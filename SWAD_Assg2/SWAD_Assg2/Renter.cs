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
        public string UpcomingRentals { get; set; }
        public bool IsPenalised { get; set; }

        public Booking MakeBooking(int bookingId, DateTime startDateTime, DateTime endDateTime, float totalCost, string pickUpLocation, string returnLocation, Car car)
        {
            return new Booking(bookingId, startDateTime, endDateTime, totalCost, pickUpLocation, returnLocation, true)
            {
                Car = car,
                Renter = this
            };
        }
    }
}
