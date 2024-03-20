﻿using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        //Buscar objeto da classe Produto por id
        [HttpGet("{id:int}", Name = "ObterProduto")]
        public ActionResult<Produto> GetById(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto is null)
            {
                return NotFound("Produto não encontrado");
            }

            return produto;
        }

        //Buscar todos os objetos da classe Produto
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> GetAll()
        {
            return _context.Produtos.ToList();
        }

        //Inserir novo objeto da classe Produto
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
            if (produto is null)
            {
                return BadRequest();
            }

            _context.Produtos.Add(produto);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        //Atualizar Produto por id
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if (id != produto.ProdutoId)
            {
                return BadRequest();
            }

            _context.Entry(produto).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok(produto);
        }

        //Deletar Produto por id
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto is null)
            {
                return NotFound();
            }

            _context.Remove(produto);
            _context.SaveChanges();

            return Ok(produto);
        }
    }
}
