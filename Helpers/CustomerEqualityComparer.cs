using FlyingDutchmanAirlines.DatabaseLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace FlyingDutchmanAirlines.Helpers
{
    internal class CustomerEqualityComparer : EqualityComparer<Customer>
    {
        public override int GetHashCode(Customer obj)
        {
            int randomNumber = RandomNumberGenerator.GetInt32(int.MaxValue / 2);
            return (obj.CustomerId + obj.Name.Length + randomNumber).GetHashCode();
        }

        public override bool Equals(Customer x, Customer y) => x.CustomerId == y.CustomerId && x.Name == y.Name;

    }
}
