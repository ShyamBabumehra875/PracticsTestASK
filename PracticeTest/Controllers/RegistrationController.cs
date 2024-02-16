using Microsoft.AspNetCore.Mvc;
using PracticeTest.Interface;
using PracticeTest.Models;
using Serilog;

namespace PracticeTest.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ILogger<RegistrationController> _logger;

        public RegistrationController(ICustomerRepository customerRepository, ILogger<RegistrationController> logger)

        {
            _customerRepository = customerRepository;
            _logger = logger;
        }


        [HttpGet]
        public async Task<IActionResult> LogingCustomer()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LoginCustomer(CustomerRegistration customerRegistration)
        {

            try
            {              
                string email = customerRegistration.Email;
                string pwd = customerRegistration.Password;
                var customer = await _customerRepository.LoginCustomer(email, pwd);
                if (customer == null) 
                {
                    return Redirect("Index");
                }
                return View(customer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                return Json(new { success = false, message = "Registration failed", error = ex.Message });
            }
        }
        public async Task<IActionResult> Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(CustomerRegistration customer)
        {
            try
            {
                var i = await _customerRepository.Create(customer);
                return RedirectToAction("GetCustomers");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                return Json(new { success = false, message = "Registration failed", error = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var customers = await _customerRepository.GetAll();
                return View(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                return BadRequest(new { message = "An error occurred while retrieving customer data.", error = ex.Message });
            }
        }

        public async Task<IActionResult> GetById(string id)
        {

            var customers = await _customerRepository.GetById(id);
            return View(customers);
        }
        [HttpPost]
        public async Task<IActionResult> GetById(CustomerRegistration registration)
        {

            try
            {
                var customers = _customerRepository.Update(registration);
                return Redirect("GetCustomers");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw;
            }
        }
        public async Task<IActionResult> DeleteById(string id)
        {

            try
            {
                var customers = _customerRepository.Delete(id);
                return RedirectToAction("GetCustomers");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                throw;
            }
        }

    }
}
