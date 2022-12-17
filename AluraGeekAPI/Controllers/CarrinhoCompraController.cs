using AluraGeekAPI.Context;
using AluraGeekAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AluraGeekAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarrinhoCompraController : ControllerBase
    {
        //banco de dados
        private readonly AppDbContext _context;

        //construtor
        public CarrinhoCompraController(AppDbContext context)
        {
            _context = context;
        }

        //metodo para retornar um carrinhoCompra assincrono
        //GET /carrinhoCompra/{id}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<CarrinhoCompra>> GetCarrinhoCompra(int id)
        //{
        //    var carrinhoCompra = await _context.CarrinhoCompra.FindAsync(id);

        //    if (carrinhoCompra == null)
        //    {
        //        return NotFound();
        //    }

        //    return carrinhoCompra;
        //}
    }
}
