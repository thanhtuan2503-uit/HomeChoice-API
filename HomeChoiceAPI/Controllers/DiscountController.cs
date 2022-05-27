using HomeChoice_Core.Data;
using HomeChoice_Core.Model;
using Microsoft.AspNetCore.Http;
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
    public class DiscountController : ControllerBase
    {
        private readonly ShopDBContext _context;
        private readonly ILogger<DiscountController> _logger;

        public DiscountController(ShopDBContext context, ILogger<DiscountController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("getAllDiscount")]
        public IActionResult GetAllDiscount()
        {
            return Ok(_context.discounts.AsEnumerable().ToList());
        }

        [HttpGet("getDiscount")]
        public IActionResult GetDiscountById(int id)
        {
            try
            {
                var discount = _context.discounts.Find(id);
                if (discount == null)
                    return Ok(new Response { Status = "Fail", Message = "Discount is not found!" });
                return Ok(discount);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost("addDiscount")]
        public async Task<IActionResult> AddDiscount(Discount modeldiscount)
        {
            try
            {
                var discount = await _context.discounts.FindAsync(modeldiscount.discount_name);
                if (discount != null)
                {
                    _context.discounts.Add(modeldiscount);
                    _context.SaveChanges();
                    return Ok(new Response { Status = "Success", Message = "Add discount code successfully!" });
                }
                return Ok(new Response { Status = "Fail", Message = "Discount code already exists!" });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
        [HttpPatch("updateDiscount")]
        public async Task<IActionResult> UpdateDiscountCode(Discount discountModel)
        {
            try
            {
                var discount = await _context.discounts.FindAsync(discountModel.discount_id);
                if (discount != null)
                {
                    _context.discounts.Update(discountModel);
                    _context.SaveChanges();
                    return Ok(new Response { Status = "Success", Message = "Update discount successfully!" });
                }
                return Ok(new Response { Status = "Fail", Message = "Discount code is not found!" });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
