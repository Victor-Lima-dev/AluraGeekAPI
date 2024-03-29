﻿using AluraGeekAPI.Context;
using AluraGeekAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AluraGeekAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        //banco de dados
        private readonly AppDbContext _context;

        //construtor
        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        //metodo para retornar todos os produtos assincrono
        //GET /produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            var listaProdutos = await _context.Produtos.ToListAsync();
            return Ok(listaProdutos);
        }

        //metodo para criar um produto assincrono
        //POST /produtos/Cadastrar
        [HttpPost("Cadastrar")]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            var verificarCategoria = produto.Categoria.ToLower();
            var verificarNome = produto.Nome.ToLower();
            var verificarPreco = produto.Preco;

            string[] categorias = { "starwars", "consoles", "outros"};

            if (verificarCategoria == null || verificarNome == null || verificarPreco == 0)
            {
                return BadRequest("Os campos não podem ser nulos");
            }

            if (verificarPreco < 0)
            {
                return BadRequest("O preço não pode ser negativo");
            }

            if (Array.IndexOf(categorias, verificarCategoria) == -1)
            {
                return BadRequest("Categoria inválida");
            }

            //verificar se o produto ja existe
            var produtoExistente = await _context.Produtos.FirstOrDefaultAsync(p => p.Nome == produto.Nome && p.Categoria == produto.Categoria && p.Preco == produto.Preco);
            if (produtoExistente != null)
            {
                return BadRequest("Produto já cadastrado");
            }
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //metodo para deletar um produto assincrono
        //DELETE /produtos/Deletar
        [HttpDelete("Deletar/{id}")]
        public async Task<ActionResult<Produto>> DeleteProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            var produtoItem = await _context.CarrinhoItens.FirstOrDefaultAsync(p => p.Produto.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            if (produtoItem != null)
            {
            _context.CarrinhoItens.Remove(produtoItem);
               
            }

            _context.Produtos.Remove(produto);

            await _context.SaveChangesAsync();

            return Ok("Item excluido");
        }

        //metodo para atualizar um produto assincrono
        //PUT /produtos/Atualizar
        [HttpPut("Atualizar/{id}")]
        public async Task<ActionResult<Produto>> PutProduto(int id, Produto produto)
        {
            var verificarCategoria = produto.Categoria.ToLower();
            var verificarNome = produto.Nome.ToLower();
            var verificarPreco = produto.Preco;

            string[] categorias = { "starwars", "consoles", "outros" };

            if (verificarCategoria == null || verificarNome == null || verificarPreco == 0)
            {
                return BadRequest("Os campos não podem ser nulos");
            }

            if (verificarPreco < 0)
            {
                return BadRequest("O preço não pode ser negativo");
            }

            if (Array.IndexOf(categorias, verificarCategoria) == -1)
            {
                return BadRequest("Categoria inválida");
            }

            var produtoExistente = await _context.Produtos.FirstOrDefaultAsync(p => p.Nome == produto.Nome && p.Categoria == produto.Categoria && p.Preco == produto.Preco);
            if (produtoExistente != null)
            {
                return BadRequest("Produto já cadastrado");
            }


            //if (id != produto.ProdutoId)
            //{
            //    return BadRequest("erro de referencia");
            //}

            _context.Entry(produto).State = EntityState.Modified;
             await _context.SaveChangesAsync();
             return Ok();
        }

        //metodo para retornar um produto assincrono, por id
        //GET /produtos/Buscar/Id/{id}
        [HttpGet("Buscar/Id/{id}")]
        public async Task<ActionResult<Produto>> GetProdutoById(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

        //metodo para retornar um produto assincrono, pela categoria
        //GET /produtos/Buscar/Categoria/{categoria}
        [HttpGet("Buscar/Categoria/{categoria}")]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutoByCategoria(string categoria)
        {
            var produto = await _context.Produtos.Where(p => p.Categoria == categoria).ToListAsync();

            if (produto == null)
            {
                return NotFound();
            }

            return Ok(produto);
        }

    }
}
