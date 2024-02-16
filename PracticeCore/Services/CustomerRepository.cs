using PracticeInfrastructure.Base;
using PracticeInfrastructure.InterFace;
namespace PracticeCore.Services
   
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task Add(CustomerRegistration customer)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerRegistration>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerRegistration> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(CustomerRegistration customer)
        {
            throw new NotImplementedException();
        }
    }
}
