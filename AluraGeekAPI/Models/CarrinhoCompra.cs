using AluraGeekAPI.Context;
using Microsoft.EntityFrameworkCore;

namespace AluraGeekAPI.Models
{
    public class CarrinhoCompra
    {
        //banco de dados
        private readonly AppDbContext _context;
        //construtor
        public CarrinhoCompra(AppDbContext context)
        {
            _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoItem> CarrinhoItens { get; set; }
        
        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            //criar sessão
            ISession session = 
                services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;
           //criar contexto
             var context = services.GetService<AppDbContext>();
            //pegar id da sessão
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();
            //salvar id da sessão
            session.SetString("CarrinhoId", carrinhoId);
            //retornar carrinho
            return new CarrinhoCompra(context) { CarrinhoCompraId = carrinhoId };
        }
        
        public void AdicionarAoCarrinho(Produto produto)
        {

            var carrinhoItem = _context.CarrinhoItens.FirstOrDefault
                (s => s.Produto.ProdutoId == produto.ProdutoId 
            && s.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoItem == null)
            {
                carrinhoItem = new CarrinhoItem
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Produto = produto,
                    Quantidade = 1
                };

                _context.CarrinhoItens.Add(carrinhoItem);
            }
            else
            {
                carrinhoItem.Quantidade++;
            }
            _context.SaveChanges();
        }

        public void RemoverDoCarrinho(Produto produto)
        {
            var carrinhoItem = _context.CarrinhoItens.FirstOrDefault
                (s => s.Produto.ProdutoId == produto.ProdutoId
            && s.CarrinhoCompraId == CarrinhoCompraId);

            if (carrinhoItem != null)
            {
                if (carrinhoItem.Quantidade > 1)
                {
                    carrinhoItem.Quantidade--;
                    _context.CarrinhoItens.Update(carrinhoItem);
                }
                else
                {
                    _context.CarrinhoItens.Remove(carrinhoItem);
                }
            }
            _context.SaveChanges();

        }

        public List<CarrinhoItem> GetCarrinhoItens()
        {
            return CarrinhoItens ?? (CarrinhoItens = _context.CarrinhoItens.Where
                (c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Include(s => s.Produto)
                .ToList());
        }

        public void LimparCarrinho()
        {
            var carrinhoItens = _context.CarrinhoItens.Where
                (cart => cart.CarrinhoCompraId == CarrinhoCompraId);

            _context.CarrinhoItens.RemoveRange(carrinhoItens);

            _context.SaveChanges();
        }

        public decimal GetCarrinhoCompraTotal()
        {
            var total = _context.CarrinhoItens.Where
                (c => c.CarrinhoCompraId == CarrinhoCompraId)
                .Select(c => c.Produto.Preco * c.Quantidade).Sum();
            return (decimal)total;
        }

    }
}
