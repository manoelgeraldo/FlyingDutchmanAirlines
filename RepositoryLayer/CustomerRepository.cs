using FlyingDutchmanAirlines.DatabaseLayer.Models;

namespace FlyingDutchmanAirlines.RepositoryLayer
{
    public class CustomerRepository
    {
        public bool CreateCustomer(string name){
            
            if (IsInvalidCustomerName(name)) { return false; }
            
            Customer newCustomer = new (name) {
                Name = name
            };
            
            return true;
        }

        private bool IsInvalidCustomerName(string name){
            
            char[] forbiddenCharacters = {'!','@','#','$','%','&','8'};
            
            return string.IsNullOrEmpty(name) || name.Any(x => forbiddenCharacters.Contains(x));
        }
    }
}