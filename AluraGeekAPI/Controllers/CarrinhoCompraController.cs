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

        //construtor
        public CarrinhoCompraController(AppDbContext context)
        {
            _context = context;
        }

        //metodo para retornar um carrinhoCompra assincrono
        //GET /carrinhoCompra/{id}
        [HttpGet("/Carrinho/{id}")]
        public async Task<ActionResult<CarrinhoCompra>> GetCarrinhoCompra(int id)
        {
            var carrinhoCompra = await _context.CarrinhoCompras.FindAsync(id);

            if (carrinhoCompra == null)
            {
                return NotFound();
            }

            return Ok(carrinhoCompra);
        }

        //metodo para criar um carrinhoCompra assincrono
        //POST /carrinhoCompra/Cadastrar
        [HttpPost("Cadastrar")]
        public async Task<ActionResult<CarrinhoCompra>> PostCarrinhoCompra(CarrinhoCompra carrinhoCompra)
        {
            _context.CarrinhoCompras.Add(carrinhoCompra);
            await _context.SaveChangesAsync();

            return Ok(carrinhoCompra);
        }

        //metodo para adicionar um carrinhoItem no carrinho
        //POST /carrinhoCompra/AdicionarItem
        [HttpPost("AdicionarItem")]
        public async Task<ActionResult<CarrinhoCompra>> PostCarrinhoItem(Produto produto, int carrinhoId)
        {

            //refatora esse metodo, pois estamos criando um CarrinhoItem e adicionando ele no carrinhoCompra

            //verifica se o item já existe no carrinho, se nao existir ele vai criar o item
            var carrinhoItem = await _context.CarrinhoItens.FirstOrDefaultAsync(p => p.ProdutoId == produto.ProdutoId && 
            p.CarrinhoItemId == carrinhoId);

            if (carrinhoItem == null)
            {
                  carrinhoItem = new CarrinhoItem
                {
                    CarrinhoItemId = carrinhoId,
                    ProdutoId = produto.ProdutoId,
                    Quantidade = 1
                };
                _context.CarrinhoItens.Add(carrinhoItem);
            }
            
            else
            {
                carrinhoItem.Quantidade++;
            }
            
            await _context.SaveChangesAsync();
            return Ok(carrinhoItem);
        }
    }
}
