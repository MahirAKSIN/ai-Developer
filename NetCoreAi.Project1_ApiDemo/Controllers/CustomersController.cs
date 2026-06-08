using Microsoft.AspNetCore.Mvc;
using NetCoreAi.Project1_ApiDemo.Context;
using NetCoreAi.Project1_ApiDemo.Entities;

namespace NetCoreAi.Project1_ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApiContext _apiContext;
        public CustomersController(ApiContext apiContext)
        {
            _apiContext = apiContext;
        }
        [HttpGet]
        public IActionResult Customerlist()
        {
            var value = _apiContext.Customers.ToList();
            return Ok(value);
        }
        [HttpPost]
        public IActionResult CustomerAdd(Customer customer)
        {
            var newCustomer = new Customer
            {
                CustomerName = customer.CustomerName,
                CustomerSurName = customer.CustomerSurName,
                CustomerBalance = customer.CustomerBalance
            };

            _apiContext.Customers.Add(newCustomer);
            _apiContext.SaveChanges();
            return Ok(newCustomer);
        }
        [HttpDelete("{DeleteCustomerId}")]
        public IActionResult DeleteCustomer(int DeleteCustomerId)
        {
            var deleteValue = _apiContext.Customers.Find(DeleteCustomerId);

            if (deleteValue == null)
            {
                return Ok("Böyle musteri yok");
            }
            _apiContext.RemoveRange(deleteValue);
            _apiContext.SaveChanges();
            return Ok(deleteValue);
        }
        [HttpPut]
        public IActionResult PutCustomer(Customer customer)
        {
            var existingCustomer = _apiContext.Customers.Find(customer.CustomerId);

            if (existingCustomer == null)
            {
                return NotFound("Böyle musteri yok");
            }

            existingCustomer.CustomerName = customer.CustomerName;
            existingCustomer.CustomerSurName = customer.CustomerSurName;
            existingCustomer.CustomerBalance = customer.CustomerBalance;
            _apiContext.SaveChanges();
            return Ok(existingCustomer);
        }
        [HttpGet("{Id}")]
        public IActionResult CustomerFind(int Id)
        {

            var findValue = _apiContext.Customers.Find(Id);

            if (findValue == null)
            {
                return Ok("Böyle musteri yok");
            }
            return Ok(findValue);
        }
    }
}
