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
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly ShopDBContext _context;

        public UsersController(ILogger<UsersController> logger, ShopDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllUser()
        {
            return Ok(_context.users.AsEnumerable().ToList());
        }

        [HttpGet("login/{username}/{password}")]
        public IActionResult Login(string username,string password)
        {
            try
            {
                var user = _context.users.FirstOrDefault(p => p.user_name == username && p.pass_word == password);
                if (user != null)
                {
                    return Ok(new Response {Status = "Success",Message ="Login succsessfully" });
                }
                return Ok(new Response { Status="Invalid",Message = "Invalid user" });
            }
           catch(Exception ex)
            {
                return Ok(ex.Message);
            }    
        }

        [HttpPost("addUser")]
        public IActionResult AddUser(User userModel)
        {
            try
            {
                var user = _context.users.FirstOrDefault(p => p.user_name == userModel.user_name && p.email == userModel.email);
                if (user == null)
                {
                    _context.users.Add(userModel);
                    _context.SaveChanges();
                    return Ok(new Response { Status="Success",Message = "Add user successed!" });
                }
                return Ok(new Response { Status="Fail", Message = "Add user failed!" });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _context.users.FindAsync(id);
                if (user == null)
                {
                    return Ok(new Response { Status = "Fail", Message = "User is not found!" });
                }
                _context.users.Remove(user);
                _context.SaveChanges();
                return Ok(new Response { Message = "Delete user successed!" });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPatch("RepairUser")]
        public async Task<IActionResult> RepairUserAsync(User userModel)
        {
            try
            {
                var user = await _context.users.FindAsync(userModel.user_id);
                if (user == null)
                {
                    return Ok(new Response { Status = "Fail", Message = "User is not found!" });
                }
                _context.users.Update(userModel);
                _context.SaveChanges();
                return Ok(new Response { Status = "Success", Message = "Update user successfully!" });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
