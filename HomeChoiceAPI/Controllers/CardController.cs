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
    public class CardController:ControllerBase
    {
        private readonly ShopDBContext _context;

        protected CardController(ShopDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCard()
        {
            return Ok(_context.cards.AsEnumerable().ToList());
        }
    }
}
