using PracticeTest.Models;

namespace PracticeTest.Interface
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<CustomerRegistration>> GetAll();
        Task<CustomerRegistration> GetById(string  id);
        Task<int> Create(CustomerRegistration customerRegistration);
        Task Update(CustomerRegistration customer);
        Task Delete(string id);

        Task<CustomerRegistration> LoginCustomer(string email, string password);
    }
}
