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

        //metodo para ver um carrinho
        //GET /carrinhoCompra/VerCarrinho
        [HttpGet("VerCarrinho")]
        public async Task<ActionResult<CarrinhoCompra>> GetCarrinhoItens()
        {
            var carrinho =  _carrinhoCompra.GetCarrinhoItens();
            if (carrinho == null)
            {
                return NotFound("Carrinho não encontrado");
            }
            return Ok(carrinho);
        }

        //metodo para adicionar um produto ao carrinho
        //Post
        [HttpPost("AdicionarAoCarrinho/{id}")]
        public async Task<ActionResult<CarrinhoCompra>> AdicionarCarrinho(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(c => c.ProdutoId == id);

            _carrinhoCompra.AdicionarAoCarrinho(produto);

            return Ok(_carrinhoCompra.GetCarrinhoItens());
        }

        //metodo para remover um produto do carrinho
        //Post
        [HttpPost("RemoverDoCarrinho/{id}")]
        public async Task<ActionResult<CarrinhoCompra>> RemoverCarrinho(int id)
        {
            var produto = await _context.Produtos.FirstOrDefaultAsync(c => c.ProdutoId == id);

            _carrinhoCompra.RemoverDoCarrinho(produto);

            return Ok(_carrinhoCompra.GetCarrinhoItens());
        }

        //metodo para limpar carrinho
        //Post
        [HttpPost("LimparCarrinho")]
        public async Task<ActionResult<CarrinhoCompra>> LimparCarrinho()
        {
            _carrinhoCompra.LimparCarrinho();

            return Ok(_carrinhoCompra.GetCarrinhoItens());
        }

        //metodo para somar carrinho
        //Post
        [HttpPost("SomarCarrinho")]
        public async Task<ActionResult<CarrinhoCompra>> SomarCarrinho()
        {
            var total = _carrinhoCompra.GetCarrinhoCompraTotal();

            return Ok(total);
        }

    }
}
