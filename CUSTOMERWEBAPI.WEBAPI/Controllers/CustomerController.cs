using CUSTOMERWEBAPI.DataAccess.Entities;
using CUSTOMERWEBAPI.DataAccess.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CUSTOMERWEBAPI.WEBAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // GET: api/Customer/All
        [HttpGet("All")]
        public async Task<IActionResult> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllCustomersAsync();
            return Ok(customers);
        }

        // GET: api/Customer/5
        [HttpGet("{id:int}")]
          // Protect customer details
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
                return NotFound(new { Message = "Customer not found" });

            return Ok(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> AddCustomer([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newCustomerId = await _customerRepository.AddCustomerAsync(customer);

            if (newCustomerId <= 0)
                return BadRequest(new { Message = "Failed to create customer" });

            // Return CreatedAtAction (REST best practice)
            return CreatedAtAction(
                nameof(GetCustomerById),
                new { id = newCustomerId },
                new { CustomerId = newCustomerId }
            );
        }

        // PUT: api/Customer/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            customer.cust_id = id; // Ensure ID from URL is used

            var success = await _customerRepository.UpdateCustomerAsync(customer);

            if (!success)
                return NotFound(new { Message = "Update failed, customer not found" });

            return Ok(new { Message = "Customer updated successfully" });
        }

        // DELETE: api/Customer/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var success = await _customerRepository.DeleteCustomerAsync(id);

            if (!success)
                return NotFound(new { Message = "Customer not found" });

            return Ok(new { Message = "Customer deleted successfully" });
        }
    }
}
