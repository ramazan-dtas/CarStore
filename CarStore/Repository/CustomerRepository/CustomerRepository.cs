using CarStore.Database;
using CarStore.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarStore.Repository.CustomerRepository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AbContext _context;


        public CustomerRepository(AbContext context)
        {
            _context = context;
        }

        public async Task<Customer> DeleteCustomer(int customerId)
        {
            Customer customer = await _context.Customer.FirstOrDefaultAsync(
                customer => customer.Id == customerId);

            if (customer != null)
            {
                _context.Customer.Remove(customer);
                await _context.SaveChangesAsync();
                return customer;
            }
            return customer;
        }

        public async Task<Customer> InsertNewCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer> UpdateExistingCustomer(int updateTargetAddressId, Customer updateThisCustomer)
        {
            Customer updatedAddress = await _context.Customer
                .FirstOrDefaultAsync(address => address.Id == updateTargetAddressId);

            if (updatedAddress != null)
            {
                updatedAddress.UserId = updateThisCustomer.UserId;
                updatedAddress.AddressName = updateThisCustomer.AddressName;
                updatedAddress.ZipCode = updateThisCustomer.ZipCode;
                await _context.SaveChangesAsync();

                return updatedAddress;
            }

            return null;
        }

        public async Task<List<Customer>> SelectAllCustomer()
        {
            return await _context.Customer.Include(users => users.User).ToListAsync();
        }

        public async Task<Customer> SelectCustomerById(int customerId)
        {
            return await _context.Customer
                .FirstOrDefaultAsync(customer => customer.Id == customerId);
        }
    }
}
