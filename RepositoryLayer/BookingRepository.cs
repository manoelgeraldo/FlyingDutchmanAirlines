using FlyingDutchmanAirlines.DatabaseLayer;
using FlyingDutchmanAirlines.DatabaseLayer.Models;
using FlyingDutchmanAirlines.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class BookingRepository
    {
        private readonly FlyingDutchmanAirlinesContext _context;

        public BookingRepository(FlyingDutchmanAirlinesContext _context)
        {
            this._context = _context;
        }

        public async Task CreateBooking(int customerId, int flightNumber)
        {
            if (customerId < 0 || flightNumber < 0)
            {
                Console.WriteLine($"Argument Exception in CreateBooking! " +
                                  $"CustomerId = {customerId}," +
                                  $"flightNumber = {flightNumber}");
                throw new ArgumentException("invalid arguments provided");
            }

            Booking newBooking = new()
            {
                CustomerId = customerId,
                FlightNumber = flightNumber
            };

            try
            {
                _context.Bookings.Add(newBooking);
                await _context.SaveChangesAsync();
            }
            catch (Exception error)
            {
                Console.WriteLine($"Exception during database query: {error.Message}");
                throw new CouldNotAddBookingToDatabaseException();
            }
        }
    }
}
