using CarStore.DTO.Customer.Request;
using CarStore.DTO.Customer.Response;

namespace CarStore.Services.CustomerService
{
    public interface ICustomerService
    {
        Task<List<CustomerResponse>> GetAll();
        Task<CustomerResponse> GetById(int customerId);
        Task<CustomerResponse> Create(NewCustomer customer);
        Task<CustomerResponse> Update(int customerId, UpdateCustomer customer);
        Task<bool> Delete(int customerId);
    }
}
