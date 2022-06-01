using CarStore.Database.Entities;
using CarStore.DTO.Customer.Request;
using CarStore.DTO.Customer.Response;
using CarStore.Repository.CustomerRepository;
using CarStore.Repository.UserRepository;

namespace CarStore.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;

        public CustomerService(ICustomerRepository customerRepository, IUserRepository userRepository)
        {
            _customerRepository = customerRepository;
            _userRepository = userRepository;
        }

        // Retunere en liste med alle adresser
        public async Task<List<CustomerResponse>> GetAll()
        {
            // Henter alle customer fra database
            List<Customer> customer = await _customerRepository.SelectAllCustomer();

            // Retuner listen med customer
            return customer == null ? null : customer.Select(
            a => new CustomerResponse
            {
                Id = a.Id,
                AddressName = a.AddressName,
                ZipCode = a.ZipCode,
                CityName = a.CityName,
                User = new CustomerUserResponse
                {
                    UserId = a.User.Id,
                    Email = a.User.Email,
                    Role = a.User.Role
                }
            }).ToList();
        }

        public async Task<CustomerResponse> GetById(int customerId)
        {
            Customer customer = await _customerRepository.SelectCustomerById(customerId);

            return customer == null ? null : new CustomerResponse
            {
                Id = customer.Id,
                AddressName = customer.AddressName,
                ZipCode = customer.ZipCode,
                CityName = customer.CityName,
                User = null
            };
        }

        public async Task<CustomerResponse> Create(NewCustomer newCustomer)
        {
            Customer customer = new Customer
            {
                UserId = newCustomer.UserId,
                AddressName = newCustomer.AddressName,
                ZipCode = newCustomer.ZipCode,
                CityName = newCustomer.CityName,

            };

            customer = await _customerRepository.InsertNewCustomer(customer);
            await _userRepository.SelectUserById(customer.UserId);

            return customer == null ? null : new CustomerResponse
            {
                Id = customer.Id,
                AddressName = customer.AddressName,
                ZipCode = customer.ZipCode,
                CityName = customer.CityName,
                User = new CustomerUserResponse
                {
                    UserId = customer.User.Id,
                    Email = customer.User.Email,
                    Role = customer.User.Role
                }
            };
        }

        public async Task<CustomerResponse> Update(int input_customer_id, UpdateCustomer input_customer)
        {
            Customer customer = new Customer
            {
                // UserId = input_address.UserId,
                AddressName = input_customer.AddressName,
                ZipCode = input_customer.ZipCode,
                CityName = input_customer.CityName
            };

            customer = await _customerRepository.UpdateExistingCustomer(input_customer_id, customer);

            return customer == null ? null : new CustomerResponse
            {
                Id = customer.Id,
                AddressName = customer.AddressName,
                ZipCode = customer.ZipCode,
                CityName = customer.CityName,
                User = null
            };
        }

        public async Task<bool> Delete(int input_customer_id)
        {
            var result = await _customerRepository.DeleteCustomer(input_customer_id);
            return (result != null);

        }
    }
}
