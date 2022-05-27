using HomeChoice_Core.Data;
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
    public class OderController:ControllerBase
    {
        private readonly ILogger<OderController> _logger;
        private readonly ShopDBContext _context;

        protected OderController(ILogger<OderController> logger, ShopDBContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAllOder()
        {
            return Ok(_context.oders.AsEnumerable().ToList());
        }
    }
}
