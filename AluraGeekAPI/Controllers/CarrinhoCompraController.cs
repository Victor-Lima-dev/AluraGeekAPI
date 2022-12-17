using AluraGeekAPI.Context;
using AluraGeekAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AluraGeekAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarrinhoCompraController : ControllerBase
    {
        //banco de dados
        private readonly AppDbContext _context;
        private readonly CarrinhoCompra _carrinhoCompra;
        //construtor
        public CarrinhoCompraController(AppDbContext context, CarrinhoCompra carrinhoCompra)
        {
            _context = context;
            _carrinhoCompra = carrinhoCompra;
        }
      

        
        
    }
}
