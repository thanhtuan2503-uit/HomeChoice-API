using HomeChoice_Core.Data;
using HomeChoice_Core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HomeChoiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ShopDBContext _context;

        protected CategoryController(ShopDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            return Ok(_context.categories.AsEnumerable().ToList());
        }

        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var category = await _context.categories.FindAsync(id);
                if(category!=null)
                {
                    return Ok(category);
                }
                return Ok(new Response { Status = "Fail", Message = "Category is not found!" });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost("AddCategory")]
        public IActionResult AddCategory(Category categoryModel)
        {
            try
            {
                var category = _context.categories.FirstOrDefault(p => p.type == categoryModel.type);
                if (category == null)
                {
                    _context.categories.Add(categoryModel);
                    _context.SaveChanges();
                    return Ok(new Response { Status = "Success", Message = "Add category successfully!" });
                }
                return Ok(new Response { Status = "Fail", Message = "Add category unsuccessfully!" });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPatch("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(Category categoryModel)
        {
            try
            {
                var category = await _context.categories.FindAsync(categoryModel.category_id);
                if(category != null)
                {
                    _context.categories.Update(categoryModel);
                    _context.SaveChanges();
                    return Ok(new Response { Status = "Success", Message = "Update category successfully!" });
                }
                return Ok(new Response { Status = "Faild", Message = "Category is not found!" });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
