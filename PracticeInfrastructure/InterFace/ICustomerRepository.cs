using PracticeInfrastructure.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PracticeInfrastructure.InterFace
{
    public  interface ICustomerRepository
    {
        Task<IEnumerable<CustomerRegistration>> GetAll();
        Task<CustomerRegistration> GetById(int id);
        Task Add(CustomerRegistration customer);
        Task Update(CustomerRegistration customer);
        Task Delete(int id);
    }
}
