using HomeChoice_Core.Data;
using HomeChoice_Core.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeChoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController:ControllerBase
    {
        private readonly ShopDBContext _context;

        public CustomerController( ShopDBContext context)
        {
            
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCustomer()
        {
            return Ok(_context.customers.AsEnumerable().ToList());
        }

        [HttpGet("getCustomer")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            try
            {
                var customer = await _context.customers.FindAsync(id);
                if(customer!=null)
                {
                    return Ok(customer);
                }
                return Ok(new Response { Status = "Fail", Message = "Customer is not found!" });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost("addCustomer")]
        public async Task<IActionResult> AddCustomer(Customer customerModel)
        {
            try
            {
                var customer = await _context.customers.FindAsync(customerModel.customer_name);
                if (customer != null)
                {
                    _context.customers.Add(customerModel);
                    _context.SaveChanges();
                    return Ok(new Response { Status = "Success", Message = "Add customer successfully!" });
                }
                return Ok(new Response { Status = "Fail", Message = "Add customer unsuccessfully!" });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPatch("updateCustomer")]
        public async Task<IActionResult> UpdateCustomer(Customer customerModel)
        {
            try
            {
                var customer = await _context.customers.FindAsync(customerModel.customer_id);
                if(customer != null)
                {
                    _context.customers.Update(customerModel);
                    _context.SaveChanges();
                    return Ok(new Response { Status = "Success", Message = "Update customer successfully!" });
                }
                return Ok(new Response { Status = "Fail", Message = "Customer is not found!" });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
