using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Topmoveis.Data;
using Topmoveis.Model;

namespace LojaTopMoveis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController2 : ControllerBase
    {

        /*private readonly LojaContext _context;

        public ProductController2(LojaContext repo)
        {
            _context = repo;
        }

        [HttpGet]
        [Route("/produtos")]
        public async Task<IActionResult> GetProducts()
        {
            try
            {
                var produtos = await _context.Products.ToListAsync();
                return Ok(produtos);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpPost]
        [Route("/products")]
        public async Task<IActionResult> CreateProducts(Product product)
        {
            try
            {
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpPut]
        [Route("/products")]
        public async Task<IActionResult> UpdateProducts(Product product)
        {
            try
            {
                var dbProduct = await _context.Products.FindAsync(product.Id);
                if(dbProduct == null)
                    return NotFound();
                
                dbProduct.Name = product.Name;
                dbProduct.Description = product.Description;
                dbProduct.FeaturedProduct = product.FeaturedProduct;
                dbProduct.Category = product.Category;
                dbProduct.Value = product.Value;

                await _context.SaveChangesAsync();
                return Ok(product);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        [HttpDelete]
        [Route("/products")]
        public async Task<IActionResult> DeleteProducts(Guid id)
        {
            try
            {
                var dbProduct = await _context.Products.FindAsync(id);
                if (dbProduct == null)
                    return NotFound();

                _context.Products.Remove(dbProduct);
                await _context.SaveChangesAsync();
                return Ok("ok");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }*/
    }

    

}
