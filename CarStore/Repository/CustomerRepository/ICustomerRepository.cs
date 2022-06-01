using CarStore.Database.Entities;

namespace CarStore.Repository.CustomerRepository
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> SelectAllCustomer();
        Task<Customer> SelectCustomerById(int customerId);
        Task<Customer> InsertNewCustomer(Customer customer);
        Task<Customer> UpdateExistingCustomer(int customerId, Customer customer);
        Task<Customer> DeleteCustomer(int customerId);
    }
}
