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
            _apiContext.Customers.Add(customer);
            _apiContext.SaveChanges();
            return Ok(customer);
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
            _apiContext.Customers.Update(customer);
            _apiContext.SaveChanges();
            return Ok(customer);

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
