using FlyingDutchmanAirlines.Helpers;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace FlyingDutchmanAirlines.DatabaseLayer.Models
{
    public sealed class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public ICollection<Booking> Bookings { get; set; }

        public Customer(string name)
        {
            Bookings = new HashSet<Booking>();
            Name = name;
        }

        public static bool operator == (Customer x, Customer y)
        {
            CustomerEqualityComparer comparer = new();
            return comparer.Equals(x, y);
        }

        public static bool operator != (Customer x, Customer y) => !(x == y);
    }
}
