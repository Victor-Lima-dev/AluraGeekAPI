using AluraGeekAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AluraGeekAPI.Context
{
    public class AppDbContext : DbContext
    {
        //implentar dbcontext
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        //mapear classe produto
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CarrinhoItem> CarrinhoItens { get; set; }
    

    }
}
