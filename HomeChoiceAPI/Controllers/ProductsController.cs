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
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ShopDBContext _context;

        public ProductsController(ILogger<ProductsController> logger, ShopDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            return Ok(_context.products.AsEnumerable().ToList());
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var product = _context.products.Find(id);
                if (product == null)
                    return Ok(new Response { Status="Failed",Message="Product is not find!" });
                return Ok(product);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPost("AddProduct")]
        public IActionResult AddProduct(Product productModel)
        {
            try
            {
                var product = _context.products.FirstOrDefault(p => p.product_name == productModel.product_name);
                if (product == null)
                {
                    _context.products.Add(productModel);
                    _context.SaveChanges();
                    return Ok(new Response { Status = "Success", Message = "Add product successfully" });
                }
                return Ok(new Response { Status="Fail",Message = "Add product failed" });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                var product = await _context.products.FindAsync(id);
                if(product != null)
                {
                    _context.products.Remove(product);
                    _context.SaveChanges();
                    return Ok(new Response { Status = "Success", Message = "Delete product successfully!" });
                }
                return Ok(new Response { Status = "Fail", Message = "Product is not found" });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpPatch("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Product productModel)
        {
            try
            {
                var product = await _context.products.FindAsync(productModel.product_id);
                if(product != null)
                {
                    _context.products.Update(productModel);
                    _context.SaveChanges();
                    return Ok(new Response { Status = "Success", Message = "Update product successfully!" });
                }
                return Ok(new Response { Status = "Fail", Message = "Product is not found!" });
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("GetProductCategory")]
        public IActionResult GetProductByProduct(int id)
        {
            try
            {
                var product =  _context.products.Where(p => p.category_id == id).ToList();
                if(product.Count==0)
                {
                    return Ok(new Response { Status = "Fail", Message = "Product is not found!" });
                }
                return Ok(product);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }

        [HttpGet("GetDetailProduct")]
        public IActionResult GetDetailProduct(int id)
        {
            try
            {
                var detailProduct = _context.detail_Products.Where(p => p.detail_id == id).FirstOrDefault();
                if(detailProduct == null)
                {
                    return Ok(new Response { Status = "Fail", Message = "Product is not found!" });
                }
                return Ok(detailProduct);
            }
            catch(Exception ex)
            {
                return Ok(ex.Message);
            }
        }
    }
}
