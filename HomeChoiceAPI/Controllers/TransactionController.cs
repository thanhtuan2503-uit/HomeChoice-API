using HomeChoice_Core.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeChoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController:ControllerBase
    {
        private readonly ShopDBContext _context;

        protected TransactionController(ShopDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllTransaction()
        {
            return Ok(_context.transactions.AsEnumerable().ToList());
        }
    }
}
