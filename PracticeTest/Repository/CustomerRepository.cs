//using Microsoft.Data.SqlClient;
using Dapper;
using PracticeTest.Controllers;
using PracticeTest.Interface;
using PracticeTest.Models;
using System.Data.SqlClient;
using System.Net;

namespace PracticeTest.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<RegistrationController> _logger;

        public CustomerRepository(IConfiguration configuration, ILogger<RegistrationController> logger)
        {
            _connectionString = configuration.GetConnectionString("DBConnection");
            _logger = logger;
        }
        public async Task<int> Create(CustomerRegistration customerRegistration)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    CustomerRegistration customer1 = new CustomerRegistration();

                    conn.Open();

                    var query = @"INSERT INTO CustomerRegistration (Id, Name, Email, PhoneNumber, Address,Password)
                          VALUES (@Id, @Name, @Email, @PhoneNumber, @Address,@Password)";

                    var parameters = new
                    {
                        Id = Guid.NewGuid(),
                        Name = customerRegistration.Name,
                        Email = customerRegistration.Email,
                        PhoneNumber = customerRegistration.PhoneNumber,
                        Address = customerRegistration.Address,
                        Password = customerRegistration.Password,
                    };
                    var i = await conn.ExecuteAsync(query, parameters);
                    return i;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw;
            }
        }
        public Task Delete(string id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    CustomerRegistration customer1 = new CustomerRegistration();
                    conn.Open();
                    var query = @"delete FROM CustomerRegistration WHERE Id = @Id";
                    var parameters = new { Id = id };

                    return conn.QuerySingleOrDefaultAsync<CustomerRegistration>(query, parameters);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw;
            }
        }
        public async Task<IEnumerable<CustomerRegistration>> GetAll()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    CustomerRegistration customer1 = new CustomerRegistration();
                    conn.Open();
                    var query = @"select * from CustomerRegistration";
                    return await conn.QueryAsync<CustomerRegistration>(query);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw;
            }
        }
        public async Task<CustomerRegistration> GetById(string id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    CustomerRegistration customer1 = new CustomerRegistration();
                    conn.Open();
                    var query = @"SELECT * FROM CustomerRegistration WHERE Id = @Id";
                    var parameters = new { Id = id };
                    return await conn.QuerySingleOrDefaultAsync<CustomerRegistration>(query, parameters);                   
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw;
            }
        }
        public async Task Update(CustomerRegistration customer)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    var query = @"
                    UPDATE CustomerRegistration 
                    SET Name=@Name, Email = @Email,PhoneNumber=@PhoneNumber,Address=@Address, Password = @Password 
                    WHERE Id = @Id";
                    var parameters = new
                    {
                        Name = customer.Name,
                        Email = customer.Email,
                        PhoneNumber = customer.PhoneNumber,
                        Address = customer.Address,
                        Password = customer.Password,
                        Id = customer.Id
                    };
                    await conn.ExecuteAsync(query, parameters);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw;
            }
        }
        public async Task<CustomerRegistration> LoginCustomer(string email, string password)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    CustomerRegistration customer1 = new CustomerRegistration();
                    conn.Open();
                    var query = @"SELECT * FROM CustomerRegistration WHERE Email=@email and password=@password";
                    var parameters = new { Email = email, Password = password };
                    var q = await conn.QuerySingleOrDefaultAsync<CustomerRegistration>(query, parameters);
                    conn.Close();
                    return q;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw;
            }
        }
    }
}
